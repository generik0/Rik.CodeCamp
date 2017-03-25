using AutoMapper;

namespace GAIT.Utilities.Mapping
{
    public interface IMapperContainer
    {
        IMapper Mapper { get; set; }
    }
}
