using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Lokad.ILPack;

namespace Soft.Square.Reflection.AssemblyGenerator
{
    public class FieldDescriptor
    {
        public FieldDescriptor(string fieldName, Type fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
        }

        public string FieldName { get; }
        public Type FieldType { get; }
    }

    public static class TypeAssembelyBuilder
    {
        public static object CreateNewJournalBook(string typeName, Type type)
        {
            var myTypeInfo = CompileResultTypeInfo(typeName, type);
            var myType = myTypeInfo.AsType();
            return Activator.CreateInstance(myType);
        }

        public static TypeInfo CompileResultTypeInfo(string typeName, Type parent = null, Type[] collections = null)
        {
            TypeBuilder tb;
            if (parent != null)
            {
                typeName = parent.Namespace + "." + typeName;
                tb = GetTypeBuilder(typeName, parent);

                // tb.SetParent(parent);
                // tb.CopyPropertiesFrom(parent);
                // tb.AddInterfaceImplementation(typeof(ITest));
            }
            else
            {
                tb = GetTypeBuilder(typeName);
            }

            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // var yourListOfFields = new List<FieldDescriptor>()
            // {
            //     //new FieldDescriptor("YourProp1",typeof(string)),
            //     //new FieldDescriptor("YourProp2", typeof(int))
            // };
            var yourListOfFields = parent.GetProperties(
                BindingFlags.FlattenHierarchy |
                BindingFlags.Instance |

                // BindingFlags.NonPublic |
                BindingFlags.Public);

            // BindingFlags.Static
            foreach (var field in yourListOfFields)
            {
                if (field.ReflectedType.ToString() == parent.FullName)
                {

                    Console.WriteLine(field.Name);
                    Console.WriteLine(field.GetAccessors());
                    Console.WriteLine(field.ReflectedType);
                    Console.WriteLine(field.PropertyType);
                    Console.WriteLine("//////////////////////FieldType///////////////////////");
                    CreateProperty(tb, field.Name, field.PropertyType);
                }
            }

            foreach (Type typ in collections)
            {
                Console.WriteLine(typ.FullName);
                Console.WriteLine(typ.IsGenericType);
                Console.WriteLine("/////////////////////Property///////////////////////");

                CreateProperty(tb, "Posts", typ, false);
            }

            // var yourListOfProperties = parent.GetType().GetProperties(
            //     BindingFlags.Public | BindingFlags.NonPublic);
            // foreach (var field in yourListOfProperties)
            //     CreateProperty(tb, field.Name, field.PropertyType, field);

            TypeInfo objectTypeInfo = tb.CreateTypeInfo();
            var generator = new Lokad.ILPack.AssemblyGenerator();

            // for ad-hoc serialization
            // var bytes = generator.GenerateAssemblyBytes(tb.Assembly);

            // direct serialization to disk
            generator.GenerateAssembly(tb.Assembly, AppDomain.CurrentDomain.BaseDirectory + "AkhbarElYom.dll");
            return objectTypeInfo;
        }

        private static TypeBuilder GetTypeBuilder(string typeName, Type parent = null)
        {
            AssemblyBuilder assemblyBuilder;
            string typeSignature = typeName; // "MyDynamicType";

            // var an = new AssemblyName(typeSignature);

            if (parent != null)
            {
                assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(Assembly.GetAssembly(parent).GetName(), AssemblyBuilderAccess.RunAndCollect);
            }
            else
            {
                assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);
            }

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(
                    typeSignature,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);

            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType, bool entityMapped = true)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod(
                  "set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null,
                  new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

            if (!entityMapped)
            {
                AddNotMappedAttribute(propertyBuilder);
            }

        }

        private static void AddDataMemberAttribute(PropertyBuilder propertyBuilder)
        {
            Type attrType = typeof(DataMemberAttribute);
            var attr = new CustomAttributeBuilder(attrType.GetConstructor(Type.EmptyTypes), new object[] { });
            propertyBuilder.SetCustomAttribute(attr);
        }

        private static void AddNotMappedAttribute(PropertyBuilder propertyBuilder)
        {
            Type attrType = typeof(NotMappedAttribute);
            var attr = new CustomAttributeBuilder(attrType.GetConstructor(Type.EmptyTypes), new object[] { });
            propertyBuilder.SetCustomAttribute(attr);
        }

        // private static void AddTableAttribute(string name, TypeBuilder _typeBuilder)
        // {
        //     Type attrType = typeof(TableAttribute);
        //     _typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(attrType.GetConstructor(new[] { typeof(string) }),
        //         new object[] { name }));
        // }

        public static T DataContractSerialization<T>(T obj)
        {
            DataContractSerializer dcSer = new DataContractSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();

            dcSer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;

            T newObject = (T)dcSer.ReadObject(memoryStream);
            return newObject;
        }
    }
}