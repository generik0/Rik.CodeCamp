namespace GAIT.Utilities
{
    public interface IActivatorFactory
    {
        T Create<T>(params object[] parameters) where T : class;
    }
}