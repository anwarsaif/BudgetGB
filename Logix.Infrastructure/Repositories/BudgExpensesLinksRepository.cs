using Logix.Application.Interfaces.IRepositories.GB;
using Logix.Domain.GB;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.GB
{
    public class BudgExpensesLinksRepository : GenericRepository<BudgExpensesLinks>, IBudgExpensesLinksRepository
    {
        private readonly ApplicationDbContext context;

        public BudgExpensesLinksRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<BudgExpensesLinksVW>> GetAllVW(Expression<Func<BudgExpensesLinksVW, bool>> expression)
        {
            return await context.Set<BudgExpensesLinksVW>().Where(expression).AsNoTracking().ToListAsync();
        }

    }
    
}

