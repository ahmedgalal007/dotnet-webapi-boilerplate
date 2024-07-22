using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using Humanizer;

namespace Soft.Square.Reflection.AssemblyGenerator.EntityBuilder
{
    public class DynamicDbContextManager
    {
        public List<EntityTypeBuilder> _entityTypeBuilders;
        private readonly string _baseAsseblyName;
        private readonly string _contextName;
        private readonly ModuleBuilder _moduleBuilder;
        public DynamicDbContextManager(string contextName)
        {
            _baseAsseblyName = AppDomain.CurrentDomain.FriendlyName + ".DynamicEntities";
            _contextName = contextName;
            _entityTypeBuilders = new List<EntityTypeBuilder>();
            _moduleBuilder = CreateModuleBuilder();
        }

        public EntityTypeBuilder AddEntity(string typeName, List<(string, Type)> propertyList)
        {
            EntityTypeBuilder eTB = new EntityTypeBuilder(this, typeName);
            _entityTypeBuilders.Add(eTB);
            return eTB;
        }

        public EntityTypeBuilder AddEntity(string typeName, Type parentType)
        {
            EntityTypeBuilder eTB = new EntityTypeBuilder(this, typeName, parentType);
            _entityTypeBuilders.Add(eTB);
            return eTB;
        }

        public EntityTypeBuilder AddEntity(string typeName, EntityTypeBuilder parentType)
        {
            EntityTypeBuilder eTB = new EntityTypeBuilder(this, typeName, parentType);
            _entityTypeBuilders.Add(eTB);
            return eTB;
        }

        public void AddRelationShip(string parentTypeName, string childTypeName)
        {
            EntityTypeBuilder parentEntity = getEntityTypeBuilder(parentTypeName);
            EntityTypeBuilder childEntity = getEntityTypeBuilder(childTypeName);
            (string, Type) parentId = parentEntity.GetKeyProperty();
            parentEntity.AddVirtualICollectionProperty(childEntity.GetTypeName().Pluralize(), childEntity.GetTypeName(), childEntity);

            childEntity.AddProperty($"{parentEntity.GetTypeName()}Id", parentId.Item2);
            childEntity.AddVirtualProperty(parentEntity.GetTypeName(), parentEntity.CreateTypeInfo());
        }

        public EntityTypeBuilder getEntityTypeBuilder(string entityTypeName)
        {
            return _entityTypeBuilders.SingleOrDefault(tb => tb.GetTypeName() == entityTypeName);
        }

        private ModuleBuilder CreateModuleBuilder()
        {
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName($"{_baseAsseblyName}.{_contextName}"), AssemblyBuilderAccess.Run);
            return assemblyBuilder.DefineDynamicModule($"{_contextName}Module");
        }

        public void SaveToAssembly()
        {
            foreach (EntityTypeBuilder entityBuilder in _entityTypeBuilders)
            {
                entityBuilder.CreateTypeInfo();
            }

            var generator = new Lokad.ILPack.AssemblyGenerator();

            // for ad-hoc serialization
            // var bytes = generator.GenerateAssemblyBytes(tb.Assembly);
            string assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Entities");
            if(!Directory.Exists(assemblyPath))
                Directory.CreateDirectory(assemblyPath);

            // direct serialization to disk
            generator.GenerateAssembly(_moduleBuilder.Assembly, $"{assemblyPath}\\{_contextName}.dll"); // $"{assemblyPath}\\{ _contextName}.dll"
        }

        /// <summary>
        /// Load Assemblies from the given Directory.
        /// </summary>
        /// <param name="path"> the location to load and return it's assemblies.</param>
        /// <returns>Load and Return a List of Assemblies in that Directory.</returns>
        public static List<Assembly> loadAssemblies(string path)
        {
            List<Assembly> allAssemblies = new List<Assembly>();

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
                allAssemblies.Add(Assembly.LoadFile(dll));
            return allAssemblies;
        }

        /// <summary>
        /// Load Assemblies from the given Assembly Location.
        /// </summary>
        /// <param name="assembly"> the assembly to get it's location and load others assemblies.</param>
        /// <returns>Load and Return a List of Assemblies in that Assemblies location.</returns>
        public static List<Assembly> LoadAssemblies(Assembly assembly)
        {
            string path = Path.GetDirectoryName(assembly.Location);
            return loadAssemblies(path);
        }

        /// <summary>
        /// Load Assemblies from the current Execution Assembly Location.
        /// </summary>
        /// <returns>Load and Return a List of Assemblies in the Current Execution assebly location</returns>
        public static List<Assembly> LoadAssemblies()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return loadAssemblies(path);
        }
        #region Getters
        public string GetBaseAssemblyname()
        {
            return _baseAsseblyName;
        }

        public string GetContextName()
        {
            return _contextName;
        }

        public ModuleBuilder GetContextModuleBuilder()
        {
            return _moduleBuilder;
        }
        #endregion

    }

}
