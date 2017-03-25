using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using GAIT.Utilities.DI.Attributes;
using Rik.Codecamp.Entities;

namespace Rik.CodeCamp.Core.Contracts
{
    [ServiceContract, InverstionOfControlInstallAsWcfService]
    public interface IBarService
    {
        [OperationContract]
        bool IsConnected();

        [OperationContract]
        Task<IEnumerable<Brave>> GetAllBraves();

        [OperationContract]
        Task<Brave> GetBrave(int Id);
        
        [OperationContract]
        Task<int> SaveOrUpdateBrave(Brave brave);
    }
}
