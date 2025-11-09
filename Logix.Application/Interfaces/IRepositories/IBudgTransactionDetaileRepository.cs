using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.Gb;
using Logix.Domain.GB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IRepositories.GB
{
    public interface IBudgTransactionDetaileRepository : IGenericRepository<BudgTransactionDetaile>
    {
        Task<BudgTransactionDetailesVw> GetOneVW(Expression<Func<BudgTransactionDetailesVw, bool>> expression);
        Task<IEnumerable<BudgTransactionDetailesVw>> GetAllVW(Expression<Func<BudgTransactionDetailesVw, bool>> expression);

    }

}
