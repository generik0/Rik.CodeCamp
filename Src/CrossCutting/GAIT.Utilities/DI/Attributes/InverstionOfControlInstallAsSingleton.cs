using System;

namespace GAIT.Utilities.DI.Attributes
{
    [NoIoC]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class InverstionOfControlInstallAsSingleton : Attribute
    {
    }
}
