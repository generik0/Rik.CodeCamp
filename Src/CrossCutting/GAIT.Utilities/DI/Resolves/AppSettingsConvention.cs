using System;
using System.Configuration;
using System.Globalization;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using GAIT.Utilities.DI.Installers;

namespace GAIT.Utilities.DI.Resolves
{
    public class AppSettingsConvention : ISubDependencyResolver
    {
        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {


            return model.ComponentName.Name.EndsWith(AppSettingsInstaller.AppSettingsModelpostfix) && 
                DontInjectProperties(dependency) && (dependency.TargetType == typeof(int) || 
                dependency.TargetType == typeof(bool) || dependency.TargetType == typeof(string) || dependency.TargetType == typeof(double));
        }

        private static bool DontInjectProperties(DependencyModel dependency)
        {
            return !dependency.IsOptional;
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            var appSettingsKey = dependency.DependencyKey;
            var appSetting = ConfigurationManager.AppSettings[appSettingsKey];
            try
            {
                if (dependency.TargetType == typeof(string))
                {
                    return appSetting ?? string.Empty;
                }
                return appSetting != null ? Convert.ChangeType(appSetting, dependency.TargetType, CultureInfo.InvariantCulture) : Activator.CreateInstance(dependency.TargetType);
            }
            catch (FormatException)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Unable to convert {0} setting to {1}", appSettingsKey, dependency.TargetType));
            }
        }
    }
}