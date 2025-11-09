using Logix.Application.Interfaces.IRepositories.GB;
using Logix.Domain.ACC;
using Logix.Domain.Gb;
using Logix.Domain.GB;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Repositories.GB
{
    public class BudgTransactionDetaileRepository : GenericRepository<BudgTransactionDetaile>, IBudgTransactionDetaileRepository
    {
        private readonly ApplicationDbContext context;

        public BudgTransactionDetaileRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<BudgTransactionDetailesVw> GetOneVW(Expression<Func<BudgTransactionDetailesVw, bool>> expression)
        {
            return await context.Set<BudgTransactionDetailesVw>().AsNoTracking().FirstOrDefaultAsync(expression);
        }
        public async Task<IEnumerable<BudgTransactionDetailesVw>> GetAllVW(Expression<Func<BudgTransactionDetailesVw, bool>> expression)
        {
            return await context.Set<BudgTransactionDetailesVw>().Where(expression).AsNoTracking().ToListAsync();
        }
    }
}

