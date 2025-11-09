using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.ACC;
using Logix.Application.Interfaces.IRepositories.GB;
using Logix.Application.Interfaces.IRepositories.Main;
using Logix.Infrastructure.Repositories.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Repositories
{
    public class GbRepositoryManager : IGbRepositoryManager
    {
        
        private readonly IUnitOfWork unitOfWork;
        private readonly ISubitemsRepository subitemsRepository;
        private readonly IBudgTransactionsRepository budgTransactionsRepository;
        private readonly IBudgTransactionDetaileRepository budgTransactionDetaileRepository;
        private readonly IBudgExpensesLinksRepository budgExpensesLinksRepository;
        private readonly IBudgDocTypeRepository budgDocTypeRepository;
        private readonly IBudgAccountExpensesRepository budgAccountExpensesRepository;

        public GbRepositoryManager(
             ISubitemsRepository subitemsRepository,
             IBudgTransactionsRepository budgTransactionsRepository,
                 IBudgTransactionDetaileRepository budgTransactionDetaileRepository,
                 IBudgExpensesLinksRepository budgExpensesLinksRepository,
                 IBudgDocTypeRepository budgDocTypeRepository,
                 IBudgAccountExpensesRepository budgAccountExpensesRepository,
            IUnitOfWork unitOfWork
           
          
            )
        {
            this.subitemsRepository = subitemsRepository;
            this.budgTransactionsRepository = budgTransactionsRepository;
            this.budgTransactionDetaileRepository = budgTransactionDetaileRepository;
            this.budgExpensesLinksRepository = budgExpensesLinksRepository;
            this.budgDocTypeRepository = budgDocTypeRepository;
            this.budgAccountExpensesRepository = budgAccountExpensesRepository;
            this.unitOfWork = unitOfWork;
           
            
        }
      

        public IUnitOfWork UnitOfWork => unitOfWork;

        public ISubitemsRepository SubitemsRepository => subitemsRepository;
        public IBudgTransactionsRepository BudgTransactionsRepository => budgTransactionsRepository;
        public IBudgTransactionDetaileRepository BudgTransactionDetaileRepository => budgTransactionDetaileRepository;
        public IBudgExpensesLinksRepository BudgExpensesLinksRepository => budgExpensesLinksRepository;
        public IBudgDocTypeRepository BudgDocTypeRepository=> budgDocTypeRepository;
        public IBudgAccountExpensesRepository BudgAccountExpensesRepository=> budgAccountExpensesRepository;
    }
}
