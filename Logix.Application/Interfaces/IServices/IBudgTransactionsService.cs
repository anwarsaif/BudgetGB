using Logix.Application.DTOs.GB;
using Logix.Application.Wrapper;
using Logix.Domain.GB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IServices.GB
{
    public interface IBudgTransactionsService :  IGenericQueryService<BudgTransactionDto , BudgTransactionVw>, IGenericWriteService<BudgTransactionDto, BudgTransactionEditDto>

    {

        Task<IResult> UpdateStatuseId(long Id,int StatuseId, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionDto>> AddDel(BudgTransactionVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionInitialCreditsDto>> AddInitialCredits(BudgTransactionInitialCreditsVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionInitialCreditsEditDto>> UpdateInitialCredits(BudgTransactionInitialCreditsEditVM entity, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionReinforcementsDto>> AddReinforcements(BudgTransactionReinforcementsVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionReinforcementsEditDto>> UpdateReinforcements(BudgTransactionReinforcementsEditVM entity, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionCostsitemsDto>> AddCostsitems(BudgTransactionCostsitemsVM entity, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionEditDto>> UpdateDel(BudgTransactionEditVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionCostsitemsEditDto>> UpdateCostsitems(BudgTransactionCostsitemsEditVM entity, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionTransfersDto>> AddTransfers(BudgTransactionTransfersVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionTransfersEditDto>> UpdateTransfers(BudgTransactionTransfersEditVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionInitialYearDto>> AddDetaileYear(BudgTransactionInitialYearVM entity, CancellationToken cancellationToken = default);

        Task<IResult<IEnumerable<BudgTransactionVw>>> GetAllVW(CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionLinksDto>> AddLinks(BudgTransactionLinksVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionLinksInitialDto>> AddLinks(BudgTransactionLinksInitialVM entity, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionLinksEditDto>> UpdateLinks(BudgTransactionLinksEditVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionLinksInitialDto>> AddLinksInitial(BudgTransactionLinksInitialVM entity, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionLinksInitialEditDto>> UpdateLinksInitial(BudgTransactionLinksInitialEditVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionDiscountsDto>> AddDiscounts(BudgTransactionDiscountsVM entity, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionDiscountsEditDto>> UpdateDiscounts(BudgTransactionDiscountsEditVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionLinksInitialmultipleDto>> AddLinksMultiple(BudgTransactionLinksInitialmultipleVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionLinksInitialmultipleEditDto>> UpdateLinksMultiple(BudgTransactionLinksInitialmultipleEditVM entity, CancellationToken cancellationToken = default);

        Task<IResult<BudgTransactionlinkFinalMultiDto>> AddlinkFinalMultiple(BudgTransactionlinkFinalMultiVM entity, CancellationToken cancellationToken = default);
        Task<IResult<BudgTransactionlinkFinalMultiEditDto>> UpdatelinkFinalMultiple(BudgTransactionlinkFinalMultieEditVM entity, CancellationToken cancellationToken = default);
        Task<int?> GetBudgTransactionsStatuse(long Id, int DocTypeID);
    }
}
