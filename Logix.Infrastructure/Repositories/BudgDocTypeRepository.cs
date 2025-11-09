using Logix.Application.Interfaces.IRepositories.GB;
using Logix.Domain.GB;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.GB
{
    public class BudgDocTypeRepository : GenericRepository<BudgDocType>, IBudgDocTypeRepository
    {
        private readonly ApplicationDbContext context;

        public BudgDocTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
       
    }
}

