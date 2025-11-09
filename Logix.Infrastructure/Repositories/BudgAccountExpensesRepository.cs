using Logix.Application.Interfaces.IRepositories.GB;
using Logix.Domain.GB;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.GB
{
    public class BudgAccountExpensesRepository : GenericRepository<BudgAccountExpenses>, IBudgAccountExpensesRepository
    {
        private readonly ApplicationDbContext context;

        public BudgAccountExpensesRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}

