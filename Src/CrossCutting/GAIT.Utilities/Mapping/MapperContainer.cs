using AutoMapper;

namespace GAIT.Utilities.Mapping
{
    public class MapperContainer : IMapperContainer
    {
        public IMapper Mapper { get; set; }
    }
}
