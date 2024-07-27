using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Persistence.DynamicSchemas._obsolete.AI;

using System;
using System.Reflection;
using System.Reflection.Emit;

public class RuntimeTypeGenerator
{
    public static void Main(string[] args)
    {
        // Define the assembly name
        var assemblyName = new AssemblyName("DynamicAssembly");

        // Create a dynamic assembly
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
            assemblyName,
            AssemblyBuilderAccess.Run
        );

        // Define a dynamic module in the assembly
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");

        // Define a new type in the module
        var typeBuilder = moduleBuilder.DefineType(
            "DynamicType",
            TypeAttributes.Public | TypeAttributes.Class,
            typeof(object)
        );

        // Define a field in the new type
        var fieldBuilder = typeBuilder.DefineField(
            "MyField",
            typeof(string),
            FieldAttributes.Public
        );

        // Define a constructor
        var constructorBuilder = typeBuilder.DefineConstructor(
            MethodAttributes.Public,
            CallingConventions.Standard,
            Type.EmptyTypes
        );

        var ctorIL = constructorBuilder.GetILGenerator();
        ctorIL.Emit(OpCodes.Ldarg_0);
        ctorIL.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
        ctorIL.Emit(OpCodes.Ret);

        // Define a method
        var methodBuilder = typeBuilder.DefineMethod(
            "MyMethod",
            MethodAttributes.Public,
            typeof(void),
            Type.EmptyTypes
        );

        var methodIL = methodBuilder.GetILGenerator();
        methodIL.Emit(OpCodes.Ret);

        // Create the type
        var dynamicType = typeBuilder.CreateType();

        // Use the newly created type
        object instance = Activator.CreateInstance(dynamicType);
        Console.WriteLine("Dynamic type created: " + dynamicType.Name);

        // Check the type and fields
        Console.WriteLine("Fields:");
        foreach (var fld in dynamicType.GetFields())
        {
            Console.WriteLine(" - " + fld.Name + ": " + fld.FieldType.Name);
        }

        // Example of setting and getting the field value
        var field = dynamicType.GetField("MyField");
        field.SetValue(instance, "Hello, World!");
        Console.WriteLine("Field value: " + field.GetValue(instance));
    }
}

