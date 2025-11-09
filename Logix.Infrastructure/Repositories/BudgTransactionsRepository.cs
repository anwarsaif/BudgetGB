using Logix.Application.Interfaces.IRepositories.GB;
using Logix.Domain.ACC;
using Logix.Domain.Gb;
using Logix.Domain.GB;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Repositories.GB
{
    public class BudgTransactionsRepository : GenericRepository<BudgTransaction>, IBudgTransactionsRepository
    {
        private readonly ApplicationDbContext context;

        public BudgTransactionsRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<BudgTransactionVw>> GetAllVW()
        {
            return await context.BudgTransactionVws.ToListAsync();
        }
        public decimal GetSum(Expression<Func<BudgTransactionDetailesVw, bool>> expression, Expression<Func<BudgTransactionDetailesVw, decimal>> sumExpression)
        {
            return context.BudgTransactionDetailesVws.Where(expression).Sum(sumExpression);
        }
        public async Task<string> GetBudgTransactionsCode(long facilityId, string currentDate, long FinYear, int DocTypeId)
        {
            try
            {
                var items = await context.BudgTransactions
                    .Where(x => x.FacilityId == facilityId)
                    .ToListAsync();


                long codeAut = items
                    .Where(x => x.FinYear == FinYear
                             && x.DocTypeId == DocTypeId
                             && x.FacilityId == facilityId
                             && !string.IsNullOrEmpty(x.DateGregorian)
                             && x.DateGregorian.Length >= 4
                             && x.DateGregorian.Substring(0, 4) == currentDate.Substring(0, 4))
                    .Select(t => !string.IsNullOrEmpty(t.Code) ? Convert.ToInt64(t.Code) : 0)
                    .DefaultIfEmpty()
                    .Max() + 1;

                return codeAut.ToString().PadLeft(5, '0');
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return "00001";
            }
        }
        public async Task<int?> GetBudgTransactionsStatuse(long Id, int DocTypeID)
        {
            try
            {
                int StatusId = 0;
              
                 StatusId= await context.BudgTransactions.Where(X => X.Id == Id && X.DocTypeId == DocTypeID && X.IsDeleted == false).Select(x => x.StatusId).SingleOrDefaultAsync()??0;
              
                    return StatusId;
               
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
