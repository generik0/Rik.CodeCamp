using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;
using Castle.Core.Internal;
using Castle.Facilities.TypedFactory;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GAIT.Utilities;
using GAIT.Utilities.DI.Attributes;
using GAIT.Utilities.DI.Installers;
using GAIT.Utilities.Logging;
using NLog;

namespace DSB.Kcit.Crm.Utilities.DI.Installers
{
    [NoIoC]
    public class GeneralInstallerWcfServer : IWindsorInstaller
    {
        private readonly IEnumerable<string> _paths = ProjectMetadata.ProjectAssembiliesPaths;

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var logger = LoggingFactory.Create(GetType());

            try
            {

                if (FacilityHelper.DoesKernelNotAlreadyContainFacility<TypedFactoryFacility>(container))
                {
                    container.Kernel.AddFacility<TypedFactoryFacility>();
                }
                if (FacilityHelper.DoesKernelNotAlreadyContainFacility<WcfFacility>(container))
                {
                    container.Kernel.AddFacility<WcfFacility>();
                }
                var returnFaults = new ServiceDebugBehavior
                {
                    IncludeExceptionDetailInFaults = true,
                    HttpHelpPageEnabled = true,
                };
                container.Register(Component.For<IServiceBehavior>().Instance(returnFaults).IsFallback());

                var assemblies = new List<Assembly>();
                foreach (var path in _paths)
                {
                    foreach (var sufix in ProjectMetadata.ProjectPrefixName)
                    {
                        assemblies.AddRange(Directory.GetFiles(path, $"{sufix}*.dll").Select(Assembly.LoadFile));
                    }
                }
                var types = assemblies.SelectMany(s => s.GetTypes()).OrderBy(x => x.Name).ToArray();
                RegisterBasicHttpBinding(container, types, logger);
            }
            catch (Exception exception)
            {
                logger.Error(exception, $"{GetType()} failed to install");
                throw;
            }
        }

        private static void RegisterBasicHttpBinding(IWindsorContainer container, Type[] types, ILogger logger)
        {
            var serviceContracts =
                types.Where(
                    x => x.IsInterface && x.HasAttribute<InverstionOfControlInstallAsWcfService>());
            var protocol = ConfigurationManager.AppSettings["soapProtocol"]?.ToLowerInvariant() ?? "http";

            foreach (var serviceContract in serviceContracts)
            {
                var concrete =
                    types.FirstOrDefault(x => x.IsClass && x.GetInterfaces().Any(y => y == serviceContract));

                if (concrete == null)
                {
                    logger.Error("Unable to find concrete type for interface: {0}.", serviceContract);
                    continue;
                }

                container.Register(Component.For(serviceContract).ImplementedBy(concrete)
                    .AsWcfService(new DefaultServiceModel()
                        .AddBaseAddresses(Addresses(concrete, protocol))
                        .PublishMetadata(x => x.EnableHttpGet())
                        .AddEndpoints(WcfEndpoint.ForContract(serviceContract).BoundTo(new BasicHttpBinding
                        {
                            Security = new BasicHttpSecurity
                            {
                                Mode = string.Equals(protocol, "https") ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None,
                                Transport = new HttpTransportSecurity
                                {
                                    ClientCredentialType = HttpClientCredentialType.None
                                },
                                Message = new BasicHttpMessageSecurity
                                {
                                    AlgorithmSuite = SecurityAlgorithmSuite.Default,
                                    ClientCredentialType = BasicHttpMessageCredentialType.Certificate
                                }
                            },
                            MaxBufferPoolSize = int.MaxValue,
                            MaxBufferSize = int.MaxValue,
                            MaxReceivedMessageSize = int.MaxValue,
                            ReaderQuotas =
                                new XmlDictionaryReaderQuotas()
                                {
                                    MaxArrayLength = int.MaxValue,
                                    MaxBytesPerRead = int.MaxValue,
                                    MaxDepth = 128,
                                    MaxNameTableCharCount = int.MaxValue,
                                    MaxStringContentLength = int.MaxValue
                                },
                            TransferMode = TransferMode.StreamedResponse
                        }))).LifestyleTransient());
            }
        }

        private static string[] Addresses(Type type, string protocol)
        {
            return new[] { $"{protocol}://{Environment.MachineName}/{type.Name}" };
        }
    }
}
