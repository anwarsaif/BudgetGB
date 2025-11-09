using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.GB;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Application.Wrapper;
using Logix.Domain.GB;
using System.Linq.Expressions;

namespace Logix.Application.Services.GB
{
    public class BudgExpensesLinksService : GenericQueryService<BudgExpensesLinks, BudgExpensesLinksDto, BudgExpensesLinksVW>, IBudgExpensesLinksService
    {
        private readonly IGbRepositoryManager gbpositoryManager;
        private readonly IMapper _mapper;
        private readonly ISessionHelper session;
        public BudgExpensesLinksService(IQueryRepository<BudgExpensesLinks> queryRepository, IGbRepositoryManager gbpositoryManager, IMapper mapper, ISessionHelper session) : base(queryRepository, mapper)
        {
            this.gbpositoryManager = gbpositoryManager;
            this._mapper = mapper;

            this.session = session;
        }
        public async Task<IResult<BudgExpensesLinksDto>> Add(BudgExpensesLinksDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgExpensesLinksDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                var item = _mapper.Map<BudgExpensesLinks>(entity);
                //item.ModifiedBy = 0;
                var newEntity = await gbpositoryManager.BudgExpensesLinksRepository.AddAndReturn(item);

                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgExpensesLinksDto>(newEntity);

                return await Result<BudgExpensesLinksDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgExpensesLinksDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgExpensesLinksRepository.GetById(Id);
            if (item == null) return Result<BudgExpensesLinksDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            gbpositoryManager.BudgExpensesLinksRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgExpensesLinksDto>.SuccessAsync(_mapper.Map<BudgExpensesLinksDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgExpensesLinksDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgExpensesLinksRepository.GetById(Id);
            if (item == null) return Result<BudgExpensesLinksDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            gbpositoryManager.BudgExpensesLinksRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgExpensesLinksDto>.SuccessAsync(_mapper.Map<BudgExpensesLinksDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgExpensesLinksDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<BudgExpensesLinksEditDto>> Update(BudgExpensesLinksEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgExpensesLinksEditDto>.FailAsync($"Error in {this.GetType()} : the passed entity IS NULL.");

            var item = await gbpositoryManager.BudgExpensesLinksRepository.GetById(entity.Id);

            if (item == null) return await Result<BudgExpensesLinksEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            gbpositoryManager.BudgExpensesLinksRepository.Update(item);

            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgExpensesLinksEditDto>.SuccessAsync(_mapper.Map<BudgExpensesLinksEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<BudgExpensesLinksEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }



    }

}