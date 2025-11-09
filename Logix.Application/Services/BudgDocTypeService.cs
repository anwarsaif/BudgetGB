using AutoMapper;
using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Domain.GB;

namespace Logix.Application.Services.GB
{
    public class BudgDocTypeService : GenericQueryService<BudgDocType, BudgDocType, BudgDocType>, IBudgDocTypeService
    {
        private readonly IAccRepositoryManager accRepositoryManager;

        private readonly IMapper _mapper;
        private readonly ISessionHelper session;

        public BudgDocTypeService(IQueryRepository<BudgDocType> queryRepository, IAccRepositoryManager AccRepositoryManager, IMapper mapper, ISessionHelper session) : base(queryRepository, mapper)
        {
            this.accRepositoryManager = AccRepositoryManager;
            this._mapper = mapper;

            this.session = session;
        }
    }
}