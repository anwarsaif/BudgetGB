using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories.ACC;
using Logix.Application.Interfaces.IRepositories.GB;
using Logix.Application.Interfaces.IRepositories.Main;
using Logix.Domain.GB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IRepositories
{
    public interface IGbRepositoryManager
    {
        ISubitemsRepository SubitemsRepository { get; }
        IBudgTransactionsRepository BudgTransactionsRepository { get; }
        IBudgTransactionDetaileRepository BudgTransactionDetaileRepository { get; }
       IBudgExpensesLinksRepository  BudgExpensesLinksRepository { get; }
        IBudgDocTypeRepository BudgDocTypeRepository { get; }
        IBudgAccountExpensesRepository BudgAccountExpensesRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }


}
