namespace GAIT.Utilities.Mapping
{
    public interface IMapBlockBase
    {
        T Map<T>(params object[] sources) where T : class;
    }
}