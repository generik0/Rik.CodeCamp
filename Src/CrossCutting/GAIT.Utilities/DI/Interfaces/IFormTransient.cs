using System;

namespace GAIT.Utilities.DI.Interfaces
{
    public interface IFormTransient : IDisposable
    {
        int parentId { get; set; }
        void Close();
        void Initialize();
    }
}
