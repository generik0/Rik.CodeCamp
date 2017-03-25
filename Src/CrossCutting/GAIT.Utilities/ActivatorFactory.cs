using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GAIT.Utilities
{
    public class ActivatorFactory : IActivatorFactory
    {
        private static IReadOnlyCollection<Type> _allAassembilyTypes;

        public T Create<T>(params object[] parameters) where T : class
        {
            var baseType = typeof(T);
            if (!baseType.IsInterface)
            {
                return CreateInstance<T>(baseType, parameters);
            }
            var classType = GetClassFromSameAssemblyAsInterfaceOtherwiseSearchAllAssembilies(baseType);
            if (classType != null)
            {
                return CreateInstance<T>(classType, parameters);
            }
            throw new ArgumentNullException();
        }

        private static T CreateInstance<T>(Type type, params object[] parameters) where T : class
        {
            var path = Assembly.GetAssembly(type).Location;
            var assembly = Assembly.LoadFrom(path);
            type = assembly.GetType(type.FullName);
            return (T)Activator.CreateInstance(type, parameters);
        }

        private static Type GetClassFromSameAssemblyAsInterfaceOtherwiseSearchAllAssembilies(Type interfaceType)
        {
            if (_allAassembilyTypes == null)
            {
                foreach (var path in ProjectMetadata.ProjectAssembiliesPaths)
                {
                    var assemblies = Directory.GetFiles(path, $"{ProjectMetadata.ProjectPrefixName}*.dll").Select(Assembly.LoadFile).ToList();
                    var types = assemblies?.SelectMany(s => s.GetTypes()).ToArray();
                    _allAassembilyTypes = types?.Where(x => !x.IsAbstract && x.IsClass && x.GetInterfaces().Length > 0).ToArray();
                }
            }
            if (_allAassembilyTypes == null) return interfaceType;
            var interfacesClasses = _allAassembilyTypes.Where(cls => cls.GetInterfaces().Any(inte => inte.GUID == interfaceType.GUID));
            var classType = interfacesClasses.FirstOrDefault(x => string.Equals(interfaceType.Name.Substring(1), x.Name));
            return classType;
        }

        public static T Resolve<T>(params object[] parameters) where T : class
        {
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }
    }
}
