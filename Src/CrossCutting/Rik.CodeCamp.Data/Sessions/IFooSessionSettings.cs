namespace Rik.CodeCamp.Data.Sessions
{
    public interface IFooSessionSettings
    {
        string ConnectionString { get; }
        string Name { get; }
        string ProviderName { get; }
    }
}