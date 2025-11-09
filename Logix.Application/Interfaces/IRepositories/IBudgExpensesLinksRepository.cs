using Logix.Domain.GB;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IRepositories.GB
{
    public interface IBudgExpensesLinksRepository : IGenericRepository<BudgExpensesLinks>
    {
        Task<IEnumerable<BudgExpensesLinksVW>> GetAllVW(Expression<Func<BudgExpensesLinksVW, bool>> expression);

    }


}
