
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IRepositories.GB
{
    
    public interface ISubitemsRepository : IGenericRepository<AccAccount> 
    {
        Task<IEnumerable<AccAccountsVw>> GetAllSubVW();

        Task<long?> GetAccGroupId(long AccAccountParentId, long facilityId);
        Task<long?> GetAccountLevel(long AccAccountParentId, long facilityId);

    }
}
