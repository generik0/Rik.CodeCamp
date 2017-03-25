using System;
using System.Collections.Generic;
using AutoMapper;

namespace GAIT.Utilities.Mapping
{
    public class MapperConfigurationWrapper : IMapperConfigurationWrapper
    {
        public IConfigurationProvider CreateMappings(IEnumerable<IMap> mappers)
        {
            
            return new MapperConfiguration(x=>CreateMaps(x, mappers));
        }

        public IMapper CreateMapper(IConfigurationProvider configuration)
        {
            return configuration.CreateMapper();
        }

        public void AssertConfigurationIsValid(IConfigurationProvider mapperConfiguration)
        {
            mapperConfiguration.AssertConfigurationIsValid();
        }

        private static void CreateMaps(IProfileExpression configuration, IEnumerable<IMap> mappers)
        {
            if (mappers == null) throw new ArgumentNullException(nameof(mappers));
            foreach (var map in mappers)
            {
                map.CreateMap(configuration);
            }
        }
    }
}
