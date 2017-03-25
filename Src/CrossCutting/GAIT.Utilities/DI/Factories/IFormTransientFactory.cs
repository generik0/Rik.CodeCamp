using GAIT.Utilities.DI.Attributes;
using GAIT.Utilities.DI.Interfaces;

namespace GAIT.Utilities.DI.Factories
{
    [InverstionOfControlInstallAsFactory]
    public interface IFormTransientFactory
    {
        T Create<T>() where T : IFormTransient;
        T Create<T>(object argumentsAsAnonymousType) where T : IFormTransient;
        void Release(IFormTransient instance);
    }
}

