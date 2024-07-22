using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using System.Reflection.Emit;

namespace Soft.Square.Reflection.AssemblyGenerator.EntityBuilder
{
    public class EntityTypeBuilder
    {
        private readonly string _typeName;
        private EntityAttributeBuilder _entityAttributeBuilder;
        private EntityPropertyBuilder _entityPropertyBuilder;
        private TypeBuilder _tb;
        private DynamicDbContextManager _manager;
        public bool IsHavingParent = false;
        public bool IsHavingParentBuilder = false;
        public Dictionary<string, Type> PropertyList;
        public EntityTypeBuilder(DynamicDbContextManager manager, string typeName)
        {
            _typeName = typeName;
            Intitialize(manager, _typeName);
        }

        public EntityTypeBuilder(DynamicDbContextManager manager, string typeName, Type parentType)
        {
            _typeName = typeName;
            Intitialize(manager, _typeName, parentType);
            PupolateProperitiesFromParent(parentType);
        }

        public EntityTypeBuilder(DynamicDbContextManager manager, string typeName, EntityTypeBuilder parentType)
        {
            _typeName = typeName;
            Intitialize(manager, _typeName, null, parentType);
            PupolateProperitiesFromParent(parentType);
        }

        private void Intitialize(DynamicDbContextManager manager, string typeName, Type parentType = null, EntityTypeBuilder entityTypeBuilder = null, EntityPropertyBuilder entityPropertyBuilder = null, EntityAttributeBuilder entityAttributeBuilder = null)
        {
            PropertyList = new Dictionary<string, Type>();
            _manager = manager;
            _entityAttributeBuilder = entityAttributeBuilder == null ? new EntityAttributeBuilder() : entityAttributeBuilder;
            _entityPropertyBuilder = entityPropertyBuilder == null ? new EntityPropertyBuilder() : entityPropertyBuilder;
            _tb = GetTypeBuilder(_manager.GetContextModuleBuilder(), typeName, parentType);
            IsHavingParent = parentType == null ? false : true;
            IsHavingParent = entityTypeBuilder == null ? false : true;
            ConstructorBuilder constructor = _tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
        }

        public string GetTypeName()
        {
            return _typeName;
        }

        public string GetTypeFullName()
        {
            return $"{_tb.Assembly.FullName}.{_typeName}";
        }

        public TypeBuilder GetTypeBuilder()
        {
            return _tb;
        }

        public TypeInfo CreateTypeInfo()
        {
            return _tb.CreateTypeInfo();
        }

        public void AddProperty(string propertyName, Type propertyType)
        {
            Type targetProperty;
            if (!PropertyList.TryGetValue(propertyName, out targetProperty))
            {
                _entityPropertyBuilder.CreateProperty(_tb, propertyName, propertyType);
                PropertyList.Add(propertyName, propertyType);
            }
            else
            {
                throw new Exception("Property Already exists!!");
            }
        }

        public void AddProperty(string propertyName, Type propertyType, Type childPropertyType)
        {
            Type dbSetCollection = propertyType.MakeGenericType(new Type[] { childPropertyType });
            AddProperty(propertyName, dbSetCollection);
        }

        public void AddVirtualICollectionProperty(string propertyName, Type childPropertyType)
        {
            Type postCollection = typeof(ICollection<>).MakeGenericType(new Type[] { childPropertyType.GetType() });

            // AddVirtualProperty(propertyName, PostCollection);
        }

        public void AddVirtualICollectionProperty(string propertyName, string childPropertyName, EntityTypeBuilder childTypeBuilder)
        {
            Type targetProperty;
            if (!PropertyList.TryGetValue(propertyName, out targetProperty))
            {
                _entityPropertyBuilder.CreateCollectionProperty(_tb, propertyName, childTypeBuilder._tb, true);

                // PropertyList.Add(propertyName, typeof(List<>).MakeGenericType(_tb.GetNestedType(propertyName)));
            }
            else
            {
                throw new Exception("Property Already exists!!");
            }

        }

        public void AddVirtualProperty(string propertyName, Type propertyType)
        {
            Type targetProperty;
            if (!PropertyList.TryGetValue(propertyName, out targetProperty))
            {
                _entityPropertyBuilder.CreateProperty(_tb, propertyName, propertyType, true);
                PropertyList.Add(propertyName, propertyType);
            }
            else
            {
                throw new Exception("Property Already exists!!");
            }
        }

        public (string, Type) GetKeyProperty()
        {
            // foreach(var property in _tb.GetProperties())
            // {
            //     if (property.GetCustomAttribute(typeof(KeyAttribute)) != null
            //         || property.Name.ToLower() == "id" )
            //     {
            //         return (property.Name, property.PropertyType);
            //     }
            // }
            foreach (var property in PropertyList)
            {
                if (property.Value.GetCustomAttribute(typeof(KeyAttribute)) != null
                    || property.Key.ToLower() == "id")
                {
                    return (property.Key, property.Value);
                }
            }

            return (string.Empty, null);
        }

        private static TypeBuilder GetTypeBuilder(ModuleBuilder moduleBuilder, string typeName, Type parent = null)
        {
            TypeBuilder tb = moduleBuilder.DefineType(
                    typeName,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    parent);

            return tb;
        }

        private void PupolateProperitiesFromParent(Type parentType)
        {
            var yourListOfFields = parentType.GetProperties(
                    BindingFlags.FlattenHierarchy |
                    BindingFlags.Instance |

                    // BindingFlags.NonPublic |
                    // BindingFlags.Static |
                    BindingFlags.Public);

            foreach (var property in yourListOfFields)
            {
                if (property.ReflectedType.ToString() == parentType.FullName)
                {
                    AddPropertyFromParent(property);
                }
            }
        }

        private void PupolateProperitiesFromParent(EntityTypeBuilder parentType)
        {
            foreach (PropertyInfo property in parentType.GetTypeBuilder().GetProperties())
            {
                AddPropertyFromParent(property);
            }
        }

        private void AddPropertyFromParent(PropertyInfo source)
        {

            if (source.GetGetMethod().IsVirtual)
            {
                if (source.PropertyType.BaseType != null && source.PropertyType.BaseType.IsGenericType)
                    AddVirtualProperty(source.Name, source.PropertyType);
            }
            else
            {
                AddProperty(source.Name, source.PropertyType);
            }
        }
    }
}
