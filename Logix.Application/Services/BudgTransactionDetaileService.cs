using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.GB;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Application.Wrapper;
using Logix.Domain.Gb;
using Logix.Domain.GB;
using Logix.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.GB
{
    public class BudgTransactionDetaileService : GenericQueryService<BudgTransactionDetaile, BudgTransactionDetaileDto, BudgTransactionDetailesVw>, IBudgTransactionDetaileService
    {
        private readonly IGbRepositoryManager gbpositoryManager;
        private readonly IMapper _mapper;
        private readonly ISessionHelper session;
        public BudgTransactionDetaileService(IQueryRepository<BudgTransactionDetaile> queryRepository, IGbRepositoryManager gbpositoryManager, IMapper mapper, ISessionHelper session) : base(queryRepository, mapper)
        {
            this.gbpositoryManager = gbpositoryManager;
            this._mapper = mapper;

            this.session = session;
        }
        public async Task<IResult<BudgTransactionDetaileDto>> Add(BudgTransactionDetaileDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionDetaileDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                var item = _mapper.Map<BudgTransactionDetaile>(entity);
                var newEntity = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item);

                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionDetaileDto>(newEntity);
             
                return await Result<BudgTransactionDetaileDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionDetaileDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(Id);
            if (item == null) return Result<BudgTransactionDetaileDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            gbpositoryManager.BudgTransactionDetaileRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgTransactionDetaileDto>.SuccessAsync(_mapper.Map<BudgTransactionDetaileDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgTransactionDetaileDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<BudgTransactionDetaileEditDto>>> GetAllDetaile(Expression<Func<BudgTransactionDetaileEditDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            try
            {
                var mapExpr = _mapper.Map<Expression<Func<BudgTransactionDetaile, bool>>>(expression);
                var items = await gbpositoryManager.BudgTransactionDetaileRepository.GetAll(mapExpr);
                if (items == null) return await Result<IEnumerable<BudgTransactionDetaileEditDto>>.FailAsync("No Data Found");
                var itemMap = _mapper.Map<IEnumerable<BudgTransactionDetaileEditDto>>(items);

                return await Result<IEnumerable<BudgTransactionDetaileEditDto>>.SuccessAsync(itemMap, "records retrieved");

            }
            catch (Exception exc)
            {
                return await Result<IEnumerable<BudgTransactionDetaileEditDto>>.FailAsync($"EXP in {GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(Id);
            if (item == null) return Result<BudgTransactionDetaileDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            gbpositoryManager.BudgTransactionDetaileRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgTransactionDetaileDto>.SuccessAsync(_mapper.Map<BudgTransactionDetaileDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgTransactionDetaileDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<BudgTransactionDetaileEditDto>> Update(BudgTransactionDetaileEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionDetaileEditDto>.FailAsync($"Error in {this.GetType()} : the passed entity IS NULL.");

            var item = await gbpositoryManager.BudgTransactionsRepository.GetById(entity.Id);

            if (item == null) return await Result<BudgTransactionDetaileEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            gbpositoryManager.BudgTransactionsRepository.Update(item);

            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgTransactionDetaileEditDto>.SuccessAsync(_mapper.Map<BudgTransactionDetaileEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<BudgTransactionDetaileEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<BudgTransactionDetaileEditDto>>> GetAllM(Expression<Func<BudgTransactionDetaileEditDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            try
            {
                var mapExpr = _mapper.Map<Expression<Func<BudgTransactionDetaile, bool>>>(expression);
                var items = await gbpositoryManager.BudgTransactionDetaileRepository.GetAll(mapExpr);
                if (items == null) return await Result<IEnumerable<BudgTransactionDetaileEditDto>>.FailAsync("No Data Found");
                var itemMap = _mapper.Map<IEnumerable<BudgTransactionDetaileEditDto>>(items);

                return await Result<IEnumerable<BudgTransactionDetaileEditDto>>.SuccessAsync(itemMap, "records retrieved");

            }
            catch (Exception exc)
            {
                return await Result<IEnumerable<BudgTransactionDetaileEditDto>>.FailAsync($"EXP in {GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<IEnumerable<BudgTransactionDetaileLinksEditDto>>> GetAllLinksM(Expression<Func<BudgTransactionDetaileLinksEditDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            try
            {
                var mapExpr = _mapper.Map<Expression<Func<BudgTransactionDetaile, bool>>>(expression);
                var items = await gbpositoryManager.BudgTransactionDetaileRepository.GetAll(mapExpr);
                if (items == null) return await Result<IEnumerable<BudgTransactionDetaileLinksEditDto>>.FailAsync("No Data Found");
                var itemMap = _mapper.Map<IEnumerable<BudgTransactionDetaileLinksEditDto>>(items);

                return await Result<IEnumerable<BudgTransactionDetaileLinksEditDto>>.SuccessAsync(itemMap, "records retrieved");

            }
            catch (Exception exc)
            {
                return await Result<IEnumerable<BudgTransactionDetaileLinksEditDto>>.FailAsync($"EXP in {GetType()}, Meesage: {exc.Message}");
            }
        }
        public async Task<IResult<IEnumerable<BudgTransactionDetaileLinksInitialEditDto>>> GetAllLinksInitialM(Expression<Func<BudgTransactionDetaileLinksInitialEditDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            try
            {
                var mapExpr = _mapper.Map<Expression<Func<BudgTransactionDetaile, bool>>>(expression);
                var items = await gbpositoryManager.BudgTransactionDetaileRepository.GetAll(mapExpr);
                if (items == null) return await Result<IEnumerable<BudgTransactionDetaileLinksInitialEditDto>>.FailAsync("No Data Found");
                var itemMap = _mapper.Map<IEnumerable<BudgTransactionDetaileLinksInitialEditDto>>(items);

                return await Result<IEnumerable<BudgTransactionDetaileLinksInitialEditDto>>.SuccessAsync(itemMap, "records retrieved");

            }
            catch (Exception exc)
            {
                return await Result<IEnumerable<BudgTransactionDetaileLinksInitialEditDto>>.FailAsync($"EXP in {GetType()}, Meesage: {exc.Message}");
            }
        }
        public async Task<IResult<IEnumerable<BudgTransactionDetaileDiscountsEditDto>>> GetAllDiscountsM(Expression<Func<BudgTransactionDetaileDiscountsEditDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            try
            {
                var mapExpr = _mapper.Map<Expression<Func<BudgTransactionDetaile, bool>>>(expression);
                var items = await gbpositoryManager.BudgTransactionDetaileRepository.GetAll(mapExpr);
                if (items == null) return await Result<IEnumerable<BudgTransactionDetaileDiscountsEditDto>>.FailAsync("No Data Found");
                var itemMap = _mapper.Map<IEnumerable<BudgTransactionDetaileDiscountsEditDto>>(items);

                return await Result<IEnumerable<BudgTransactionDetaileDiscountsEditDto>>.SuccessAsync(itemMap, "records retrieved");

            }
            catch (Exception exc)
            {
                return await Result<IEnumerable<BudgTransactionDetaileDiscountsEditDto>>.FailAsync($"EXP in {GetType()}, Meesage: {exc.Message}");
            }
        }

    }
}