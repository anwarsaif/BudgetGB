using Logix.Application.Interfaces.IServices.ACC;
using Logix.Application.Interfaces.IServices.Gb;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Application.Interfaces.IServices.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IServices
{
    public interface IGbServiceManager
    {

        ISubitemsService SubitemsService { get; }
        IBudgTransactionsService BudgTransactionsService { get; }
        
        IBudgTransactionDetaileService BudgTransactionDetaileService { get; }
        IBudgExpensesLinksService BudgExpensesLinksService { get; }
        IGBDashboardService GBDashboardService { get; }
        IBudgAccountExpensesService BudgAccountExpensesService { get; }
        IBudgTransactionBalanceVWService BudgTransactionBalanceVWService { get; }
        IBudgDocTypeService BudgDocTypeService { get; }



    }
}
