using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;

namespace Soft.Square.Reflection.AssemblyGenerator.EntityBuilder
{
    public class EntityAttributeBuilder
    {
        public static void AddDataMemberAttribute(ref PropertyBuilder propertyBuilder)
        {
            Type attrType = typeof(DataMemberAttribute);
            var attr = new CustomAttributeBuilder(attrType.GetConstructor(Type.EmptyTypes), new object[] { });
            propertyBuilder.SetCustomAttribute(attr);
        }

        public static void AddNotMappedAttribute(ref PropertyBuilder propertyBuilder)
        {
            Type attrType = typeof(NotMappedAttribute);
            var attr = new CustomAttributeBuilder(attrType.GetConstructor(Type.EmptyTypes), new object[] { });
            propertyBuilder.SetCustomAttribute(attr);
        }
        public static void AddTableAttribute(string name,ref TypeBuilder _typeBuilder)
        {
            Type attrType = typeof(TableAttribute);
            _typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(attrType.GetConstructor(new[] { typeof(string) }),
                new object[] { name }));
        }

    }
}
