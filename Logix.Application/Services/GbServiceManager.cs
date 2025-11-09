using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.ACC;
using Logix.Application.Interfaces.IServices.Gb;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Application.Interfaces.IServices.Main;
using Logix.Application.Services.ACC;
using Logix.Application.Services.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services
{
    public class GbServiceManager : IGbServiceManager
    {
      
        private readonly ISubitemsService subitemsService;
        private readonly IBudgTransactionsService budgTransactionsService;
        private readonly IBudgTransactionDetaileService budgTransactionDetaileService;
        private readonly IBudgExpensesLinksService budgExpensesLinksService;
        private readonly IGBDashboardService gBDashboardService;
        private readonly IBudgAccountExpensesService budgAccountExpensesService;
        private readonly IBudgTransactionBalanceVWService budgTransactionBalanceVWService;
        private readonly IBudgDocTypeService budgDocTypeService;

        public GbServiceManager(
           
           ISubitemsService subitemsService,
           IBudgTransactionsService budgTransactionsService,
           IBudgTransactionDetaileService budgTransactionDetaileService,
           IBudgExpensesLinksService budgExpensesLinksService,
           IGBDashboardService gBDashboardService,
           IBudgAccountExpensesService budgAccountExpensesService,
           IBudgTransactionBalanceVWService budgTransactionBalanceVWService,
           IBudgDocTypeService budgDocTypeService


            )
        {
           
            this.subitemsService = subitemsService;
            this.budgTransactionsService = budgTransactionsService;
            this.budgTransactionDetaileService = budgTransactionDetaileService;
            this.budgExpensesLinksService = budgExpensesLinksService;
            this.gBDashboardService = gBDashboardService;
            this.budgAccountExpensesService = budgAccountExpensesService;
            this.budgTransactionBalanceVWService = budgTransactionBalanceVWService;
            this.budgDocTypeService = budgDocTypeService;
        }
       
        public ISubitemsService SubitemsService => subitemsService;
        public IBudgTransactionsService BudgTransactionsService => budgTransactionsService;
        public IBudgTransactionDetaileService BudgTransactionDetaileService => budgTransactionDetaileService;
        public IBudgExpensesLinksService BudgExpensesLinksService => budgExpensesLinksService;
        public IGBDashboardService GBDashboardService => gBDashboardService;

        public IBudgAccountExpensesService BudgAccountExpensesService => budgAccountExpensesService;
        public IBudgTransactionBalanceVWService BudgTransactionBalanceVWService => budgTransactionBalanceVWService;

        public IBudgDocTypeService BudgDocTypeService => budgDocTypeService;

    }
}
