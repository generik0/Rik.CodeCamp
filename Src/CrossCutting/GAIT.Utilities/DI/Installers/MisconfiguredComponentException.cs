using System;
using GAIT.Utilities.DI.Attributes;

namespace GAIT.Utilities.DI.Installers
{
    [NoIoC]
    public class MisconfiguredComponentException : Exception
    {
        public MisconfiguredComponentException(string message) : base(message)
        {
        }

        public MisconfiguredComponentException(Exception exception) : base(exception.Message)
        {
        }
    }
}
