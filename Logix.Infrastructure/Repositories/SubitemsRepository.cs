using Logix.Application.Interfaces.IRepositories.ACC;
using Logix.Application.Interfaces.IRepositories.GB;
using Logix.Application.Interfaces.IRepositories.Main;
using Logix.Domain.ACC;
using Logix.Domain.Main;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Repositories.Gb
{
    public class SubitemsRepository : GenericRepository<AccAccount>, ISubitemsRepository
    {
        private readonly ApplicationDbContext context;

        public SubitemsRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<AccAccountsVw>> GetAllSubVW()
        {
            return await context.AccAccountsVw.ToListAsync();
        }

             public async Task<long?> GetAccGroupId(long AccAccountParentId, long facilityId)
        {
            try
            {
                if (facilityId > 0)
                {
                    return await context.AccAccounts.Where(X => X.AccAccountId == AccAccountParentId && X.FacilityId == facilityId && X.IsDeleted == false).Select(x => x.AccGroupId).SingleOrDefaultAsync();
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<long?> GetAccountLevel(long AccAccountParentId, long facilityId)
        {
            try
            {
                if (facilityId > 0)
                {
                    return await context.AccAccounts.Where(X => X.AccAccountId == AccAccountParentId && X.FacilityId == facilityId && X.IsDeleted == false).Select(x => x.AccountLevel).SingleOrDefaultAsync();
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

    }
}
