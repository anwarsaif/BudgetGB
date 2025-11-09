using Logix.Application.DTOs.GB;
using Logix.Domain.ACC;
using Logix.Domain.Gb;
using Logix.Domain.GB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IRepositories.GB
{
    public interface IBudgTransactionsRepository : IGenericRepository<BudgTransaction>
    {
        decimal GetSum(Expression<Func<BudgTransactionDetailesVw, bool>> expression, Expression<Func<BudgTransactionDetailesVw, decimal>> sumExpression);

        Task<string> GetBudgTransactionsCode(long facilityId, string currentDate, long FinYear, int DocTypeId);
        Task<int?> GetBudgTransactionsStatuse(long Id, int DocTypeID);


    }
}
