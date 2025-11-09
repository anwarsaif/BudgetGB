using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.GB;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.ACC;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.GB;
using System.Linq.Expressions;

namespace Logix.Application.Services.GB
{
    public class BudgAccountExpensesService : GenericQueryService<BudgAccountExpenses, BudgAccountExpensesDto, BudgAccountExpensesVW>, IBudgAccountExpensesService
    {
        private readonly IGbRepositoryManager gbpositoryManager;
        private readonly IMapper _mapper;
        private readonly ISessionHelper session;
        public BudgAccountExpensesService(IQueryRepository<BudgAccountExpenses> queryRepository, IGbRepositoryManager gbpositoryManager, IMapper mapper, ISessionHelper session) : base(queryRepository, mapper)
        {
            this.gbpositoryManager = gbpositoryManager;
            this._mapper = mapper;

            this.session = session;
        }
        public async Task<IResult<BudgAccountExpensesDto>> Add(BudgAccountExpensesDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgAccountExpensesDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                var item = _mapper.Map<BudgAccountExpenses>(entity);
                var newEntity = await gbpositoryManager.BudgAccountExpensesRepository.AddAndReturn(item);

                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgAccountExpensesDto>(newEntity);

                return await Result<BudgAccountExpensesDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgAccountExpensesDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgAccountExpensesRepository.GetById(Id);
            if (item == null) return Result<BudgAccountExpensesDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            gbpositoryManager.BudgAccountExpensesRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgAccountExpensesDto>.SuccessAsync(_mapper.Map<BudgAccountExpensesDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgAccountExpensesDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<BudgAccountExpensesEditDto>>> GetAllDetaile(Expression<Func<BudgAccountExpensesEditDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            try
            {
                var mapExpr = _mapper.Map<Expression<Func<BudgAccountExpenses, bool>>>(expression);
                var items = await gbpositoryManager.BudgAccountExpensesRepository.GetAll(mapExpr);
                if (items == null) return await Result<IEnumerable<BudgAccountExpensesEditDto>>.FailAsync("No Data Found");
                var itemMap = _mapper.Map<IEnumerable<BudgAccountExpensesEditDto>>(items);

                return await Result<IEnumerable<BudgAccountExpensesEditDto>>.SuccessAsync(itemMap, "records retrieved");

            }
            catch (Exception exc)
            {
                return await Result<IEnumerable<BudgAccountExpensesEditDto>>.FailAsync($"EXP in {GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgAccountExpensesRepository.GetById(Id);
            if (item == null) return Result<BudgAccountExpensesDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            gbpositoryManager.BudgAccountExpensesRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgAccountExpensesDto>.SuccessAsync(_mapper.Map<BudgAccountExpensesDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgAccountExpensesDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<BudgAccountExpensesEditDto>> Update(BudgAccountExpensesEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgAccountExpensesEditDto>.FailAsync($"Error in {this.GetType()} : the passed entity IS NULL.");

            var item = await gbpositoryManager.BudgTransactionsRepository.GetById(entity.Id);

            if (item == null) return await Result<BudgAccountExpensesEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            gbpositoryManager.BudgTransactionsRepository.Update(item);

            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgAccountExpensesEditDto>.SuccessAsync(_mapper.Map<BudgAccountExpensesEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<BudgAccountExpensesEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<BudgAccountExpensesEditDto>>> GetAllM(Expression<Func<BudgAccountExpensesEditDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            try
            {
                var mapExpr = _mapper.Map<Expression<Func<BudgAccountExpenses, bool>>>(expression);
                var items = await gbpositoryManager.BudgAccountExpensesRepository.GetAll(mapExpr);
                if (items == null) return await Result<IEnumerable<BudgAccountExpensesEditDto>>.FailAsync("No Data Found");
                var itemMap = _mapper.Map<IEnumerable<BudgAccountExpensesEditDto>>(items);

                return await Result<IEnumerable<BudgAccountExpensesEditDto>>.SuccessAsync(itemMap, "records retrieved");

            }
            catch (Exception exc)
            {
                return await Result<IEnumerable<BudgAccountExpensesEditDto>>.FailAsync($"EXP in {GetType()}, Meesage: {exc.Message}");
            }
        }



    }
}