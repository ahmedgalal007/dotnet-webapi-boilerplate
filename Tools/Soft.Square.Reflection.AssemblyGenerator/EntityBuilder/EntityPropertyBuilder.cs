using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Soft.Square.Reflection.AssemblyGenerator.EntityBuilder
{
    public class EntityPropertyBuilder
    {
        public void CreateProperty2(TypeBuilder tb, string propertyName, Type propertyType, bool IsVirtual = false)
        {
            var getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            if (IsVirtual)
                getSetAttr = getSetAttr | MethodAttributes.Virtual;

            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);
            
            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, getSetAttr, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                getSetAttr,
                  null, new[] { propertyType });

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

        }
        public void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType, bool IsVirtual = false)
        {
            var getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            if (IsVirtual)
                getSetAttr = getSetAttr | MethodAttributes.Virtual;

            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.None, propertyType, Type.EmptyTypes);

            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, getSetAttr, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                getSetAttr,
                  null, new[] { propertyType });

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

        }
        public void CreateCollectionProperty(TypeBuilder myType, string propertyName, TypeBuilder childType, bool IsVirtual = true)
        {
            Type listOf = typeof(List<>);
            Type selfContained = listOf.MakeGenericType(childType);

            //define a backingfield
            FieldBuilder field = myType.DefineField("<Items>_" + propertyName, selfContained, FieldAttributes.Private);

            //define a parameterless constructor to initialize the field.
            ConstructorBuilder constructor = myType.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, Type.EmptyTypes);
            ILGenerator constructorBody = constructor.GetILGenerator();
            constructorBody.Emit(OpCodes.Ldarg_0);
            constructorBody.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
            constructorBody.Emit(OpCodes.Ldarg_0);
            constructorBody.Emit(OpCodes.Newobj, TypeBuilder.GetConstructor(selfContained, listOf.GetConstructor(Type.EmptyTypes)));
            constructorBody.Emit(OpCodes.Stfld, field);
            constructorBody.Emit(OpCodes.Ret);

            //define the getter
            MethodBuilder getter = myType.DefineMethod("get_Items", MethodAttributes.Public | MethodAttributes.HideBySig, selfContained, Type.EmptyTypes);
            ILGenerator getterBody = getter.GetILGenerator();
            getterBody.Emit(OpCodes.Ldarg_0);
            getterBody.Emit(OpCodes.Ldfld, field);
            getterBody.Emit(OpCodes.Ret);

            //define the setter
            MethodBuilder setter = myType.DefineMethod("set_Items", MethodAttributes.Public | MethodAttributes.HideBySig, typeof(void), new Type[] { selfContained });
            ILGenerator setterBody = setter.GetILGenerator();
            setterBody.Emit(OpCodes.Ldarg_0);
            setterBody.Emit(OpCodes.Ldarg_1);
            setterBody.Emit(OpCodes.Stfld, field);
            setterBody.Emit(OpCodes.Ret);

            PropertyBuilder property = myType.DefineProperty(propertyName, PropertyAttributes.None, selfContained, null);

            //Bind getter and setter
            property.SetGetMethod(getter);
            property.SetSetMethod(setter);
        }
    }
}
