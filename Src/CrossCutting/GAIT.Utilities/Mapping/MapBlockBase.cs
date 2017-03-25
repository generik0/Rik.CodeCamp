using System;
using GAIT.Utilities.DI.Attributes;

namespace GAIT.Utilities.Mapping
{
    [NoIoC]
    public abstract class MapBlockBase : IMapBlockBase
    {
        private readonly IMapperContainer _container;

        protected MapBlockBase(IMapperContainer container)
        {
            _container = container;
        }

        

        protected bool CanMap(Type type, Type toBemappedType)
        {
            var result = type == toBemappedType || type.BaseType == toBemappedType;
            return result;
        }

        public T Map<T>(params object[] sources) where T : class
        {
            if (sources == null || sources.Length == 0)
                return default(T);

            var destinationType = typeof(T);
            var result = _container.Mapper.Map(sources[0], sources[0].GetType(), destinationType) as T;
            for (var i = 1; i < sources.Length; i++)
            {
                _container.Mapper.Map(sources[i], result, sources[i].GetType(), destinationType);
            }

            return result;
        }
    }
}