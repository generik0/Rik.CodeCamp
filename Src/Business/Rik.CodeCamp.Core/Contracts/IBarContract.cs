using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using GAIT.Utilities.DI.Attributes;

namespace Rik.CodeCamp.Core.Contracts
{
    [ServiceContract, InverstionOfControlInstallAsWcfService]
    public interface IBarContract
    {
        [OperationContract]
        bool IsConnected();

        [OperationContract]
        Task<IEnumerable<BraveService>> GetAllBraves();

        [OperationContract]
        Task<BraveService> GetBrave(int Id);
        
        [OperationContract]
        Task<int> SaveOrUpdateBrave(BraveService brave);
    }
}
