using System.Collections.Generic;
using AutoMapper;

namespace GAIT.Utilities.Mapping
{
    public interface IMapperConfigurationWrapper
    {
        IConfigurationProvider CreateMappings(IEnumerable<IMap> mappers);
        IMapper CreateMapper(IConfigurationProvider configuration);
        void AssertConfigurationIsValid(IConfigurationProvider mapperConfiguration);
    }
}