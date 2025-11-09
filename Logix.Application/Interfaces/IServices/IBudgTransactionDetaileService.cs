using Logix.Application.DTOs.GB;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.GB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IServices.GB
{
    public interface IBudgTransactionDetaileService : IGenericQueryService<BudgTransactionDetaileDto, BudgTransactionDetailesVw>, IGenericWriteService<BudgTransactionDetaileDto, BudgTransactionDetaileEditDto>

    {
        Task<IResult<IEnumerable<BudgTransactionDetaileEditDto>>> GetAllDetaile(Expression<Func<BudgTransactionDetaileEditDto, bool>> expression, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<BudgTransactionDetaileEditDto>>> GetAllM(Expression<Func<BudgTransactionDetaileEditDto, bool>> expression, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<BudgTransactionDetaileLinksInitialEditDto>>> GetAllLinksInitialM(Expression<Func<BudgTransactionDetaileLinksInitialEditDto, bool>> expression, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<BudgTransactionDetaileDiscountsEditDto>>> GetAllDiscountsM(Expression<Func<BudgTransactionDetaileDiscountsEditDto, bool>> expression, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<BudgTransactionDetaileLinksEditDto>>> GetAllLinksM(Expression<Func<BudgTransactionDetaileLinksEditDto, bool>> expression, CancellationToken cancellationToken = default);




    }

}

