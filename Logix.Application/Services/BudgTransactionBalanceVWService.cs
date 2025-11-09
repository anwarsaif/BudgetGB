using AutoMapper;
using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Domain.GB;

namespace Logix.Application.Services.GB
{
    public class BudgTransactionBalanceVWService : GenericQueryService<BudgTransactionBalanceVW, BudgTransactionBalanceVW, BudgTransactionBalanceVW>, IBudgTransactionBalanceVWService
    {
        private readonly IAccRepositoryManager accRepositoryManager;

        private readonly IMapper _mapper;
        private readonly ISessionHelper session;

        public BudgTransactionBalanceVWService(IQueryRepository<BudgTransactionBalanceVW> queryRepository, IAccRepositoryManager AccRepositoryManager, IMapper mapper, ISessionHelper session) : base(queryRepository, mapper)
        {
            this.accRepositoryManager = AccRepositoryManager;
            this._mapper = mapper;

            this.session = session;
        }
    }

}