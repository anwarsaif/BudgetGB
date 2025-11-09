using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.GB;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IServices.Gb
{
    
   /* public interface ISubitemsService: IGenericService<SubitemsDto>, IUpdateService<SubitemsEditDto>

    {
        Task<IResult<IEnumerable<AccAccountsVw>>> GetAllSubVW(CancellationToken cancellationToken = default);

        Task<IResult<R>> Find<R>(Expression<Func<SubitemsDto, R>> selector, Expression<Func<SubitemsDto, bool>> expression, CancellationToken cancellationToken = default);

    }*/

    public interface ISubitemsService : IGenericQueryService<SubitemsDto, AccAccountsVw>, IGenericWriteService<SubitemsDto, SubitemsEditDto>
    {
        Task<long> GetAccountLevel(long AccAccountParentId);
        Task<IResult> UpdateParentId(long Id,long AccGroupId, CancellationToken cancellationToken = default);
        Task<IResult<SubitemsDto>> AddExcel(SubitemsDto entity, CancellationToken cancellationToken = default);

    }
}
