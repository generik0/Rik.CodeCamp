using System;
using AutoMapper;

namespace GAIT.Utilities.Mapping
{
    // ReSharper disable once InconsistentNaming
    public interface IMap : IMapBlockBase
    {
        void CreateMap(IProfileExpression mapperConfiguration);
        bool CanMap(Type type );
    }
}
