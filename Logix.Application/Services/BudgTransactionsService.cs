using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.GB;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Application.Wrapper;
using Logix.Domain.Gb;
using Logix.Domain.GB;

namespace Logix.Application.Services.GB
{


    public class BudgTransactionsService : GenericQueryService<BudgTransaction, BudgTransactionDto, BudgTransactionVw>, IBudgTransactionsService
    {
        private readonly IGbRepositoryManager gbpositoryManager;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;

        public BudgTransactionsService(IQueryRepository<BudgTransaction> queryRepository, IGbRepositoryManager gbpositoryManager, IAccRepositoryManager accRepositoryManager, IMapper mapper, ISessionHelper session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.gbpositoryManager = gbpositoryManager;
            this.accRepositoryManager = accRepositoryManager;
            this._mapper = mapper;

            this.session = session;
            this.localization = localization;
        }
        public async Task<IResult> UpdateStatuseId(long Id, int StatuseId, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgTransactionsRepository.GetById(Id);
            if (item == null) return Result<BudgTransactionDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.StatusId = StatuseId;
            gbpositoryManager.BudgTransactionsRepository.Update(item);
            try
            {
                await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgTransactionDto>.SuccessAsync(_mapper.Map<BudgTransactionDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgTransactionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<BudgTransactionDto>> Add(BudgTransactionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                var item = _mapper.Map<BudgTransaction>(entity);
                item.BudgTypeId = 0;
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);

                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionDto>(newEntity);


                return await Result<BudgTransactionDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }
        public async Task<IResult<BudgTransactionDto>> AddDel(BudgTransactionVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, BudgTransactions.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                item.BudgTypeId = 1;
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {
                    //var NatureAccount = await accRepositoryManager.AccGroupRepository.GetOne(x => x.NatureAccount, s => s.AccGroupId == child.AccGroupId);
                    //if (NatureAccount != null)
                    //{
                    //    if (NatureAccount == 1)
                    //    {

                    //        child.Credit = child.Value;
                    //        child.Debit = 0;

                    //    }
                    //    else if (NatureAccount == -1)

                    //    {
                    //        child.Debit = child.Value;
                    //        child.Credit = 0;

                    //    }
                    //}
                    child.Credit = child.Value;
                    if (child.Credit == null)
                    {
                        child.Credit = 0;
                    }
                    if (child.Debit == null)
                    {
                        child.Debit = 0;
                    }
                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;

                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    if (item2 == null) return await Result<BudgTransactionDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionDto>(newEntity);


                return await Result<BudgTransactionDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult<BudgTransactionCostsitemsDto>> AddCostsitems(BudgTransactionCostsitemsVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionCostsitemsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionCostsitemsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionCostsitemsDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, BudgTransactions.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                BudgTransactions.DeptID = 0;
                BudgTransactions.AppId = 0;
                BudgTransactions.ReferenceId = 0;
                BudgTransactions.ReferenceType = 0;
                BudgTransactions.ProjectId = 0;
                BudgTransactions.CollectionEmpId = 0;
                BudgTransactions.BankId = 0;
                BudgTransactions.PaymentTypeId = 0;
                BudgTransactions.CcId = 0;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                item.BudgTypeId = 2;
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionCostsitemsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionCostsitemsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    child.Credit = child.Value;
                    if (child.Credit == null)
                    {
                        child.Credit = 0;
                    }
                    if (child.Debit == null)
                    {
                        child.Debit = 0;
                    }
                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;
                    child.DateGregorian = BudgTransactions.DateGregorian;
                    child.TypeID = 2;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;

                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    if (item2 == null) return await Result<BudgTransactionCostsitemsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionCostsitemsDto>(newEntity);


                return await Result<BudgTransactionCostsitemsDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionCostsitemsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult<BudgTransactionCostsitemsEditDto>> UpdateCostsitems(BudgTransactionCostsitemsEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionCostsitemsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;
                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 7);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionCostsitemsEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }

                if (BudgTransactions == null) return await Result<BudgTransactionCostsitemsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionCostsitemsEditDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionCostsitemsEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                BudgTransactions.DeptID = 0;
                BudgTransactions.AppId = 0;
                BudgTransactions.ReferenceId = 0;
                BudgTransactions.ReferenceType = 0;
                BudgTransactions.ProjectId = 0;
                BudgTransactions.CollectionEmpId = 0;
                BudgTransactions.BankId = 0;
                BudgTransactions.PaymentTypeId = 0;
                BudgTransactions.CcId = 0;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionCostsitemsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {
                    child.DateGregorian = BudgTransactions.DateGregorian;
                    child.TypeID = 2;
                    child.Credit = child.Value;
                    if (child.Credit == null)
                    {
                        child.Credit = 0;
                    }
                    if (child.Debit == null)
                    {
                        child.Debit = 0;
                    }

                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;

                        if (item2 == null) return await Result<BudgTransactionCostsitemsEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionCostsitemsEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");


                        child.Credit = child.Value;
                        if (child.Credit == null)
                        {
                            child.Credit = 0;
                        }
                        if (child.Debit == null)
                        {
                            child.Debit = 0;
                        }
                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;


                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionCostsitemsEditDto>.SuccessAsync(_mapper.Map<BudgTransactionCostsitemsEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionCostsitemsEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgTransactionsRepository.GetById(Id);
            if (item == null) return Result<BudgTransactionDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
            gbpositoryManager.BudgTransactionsRepository.Update(item);
            var epdRes = await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

            if (epdRes > 0)
            {
                var itemsD = await gbpositoryManager.BudgTransactionDetaileRepository.GetAll(x => x.TId == item.Id);
                if (itemsD == null) return Result<BudgTransactionDto>.Fail($"--- there is no Data with this id: {item.Id}---");

                foreach (var items in itemsD)
                {
                    items.IsDeleted = true;
                    gbpositoryManager.BudgTransactionDetaileRepository.Update(items);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }

            }
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionDto>.SuccessAsync(_mapper.Map<BudgTransactionDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgTransactionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.BudgTransactionsRepository.GetById(Id);
            if (item == null) return Result<BudgTransactionDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
            gbpositoryManager.BudgTransactionsRepository.Update(item);
            var epdRes = await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

            if (epdRes > 0)
            {
                var itemsD = await gbpositoryManager.BudgTransactionDetaileRepository.GetAll(x => x.TId == item.Id);
                if (itemsD == null) return Result<BudgTransactionDto>.Fail($"--- there is no Data with this id: {item.Id}---");

                foreach (var items in itemsD)
                {
                    items.IsDeleted = true;
                    gbpositoryManager.BudgTransactionDetaileRepository.Update(items);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }

            }
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionDto>.SuccessAsync(_mapper.Map<BudgTransactionDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<BudgTransactionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<BudgTransactionEditDto>> Update(BudgTransactionEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionEditDto>.FailAsync($"Error in {this.GetType()} : the passed entity IS NULL.");

            var item = await gbpositoryManager.BudgTransactionsRepository.GetById(entity.Id);

            if (item == null) return await Result<BudgTransactionEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            gbpositoryManager.BudgTransactionsRepository.Update(item);

            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<BudgTransactionEditDto>.SuccessAsync(_mapper.Map<BudgTransactionEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<BudgTransactionEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult<BudgTransactionEditDto>> UpdateDel(BudgTransactionEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;
                if (BudgTransactions == null) return await Result<BudgTransactionEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    child.Credit = child.Value;
                    if (child.Credit == null)
                    {
                        child.Credit = 0;
                    }
                    if (child.Debit == null)
                    {
                        child.Debit = 0;
                    }

                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;

                        if (item2 == null) return await Result<BudgTransactionEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");


                        child.Credit = child.Value;
                        if (child.Credit == null)
                        {
                            child.Credit = 0;
                        }
                        if (child.Debit == null)
                        {
                            child.Debit = 0;
                        }
                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;



                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionEditDto>.SuccessAsync(_mapper.Map<BudgTransactionEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }



        public async Task<IResult<BudgTransactionTransfersDto>> AddTransfers(BudgTransactionTransfersVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionTransfersDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionTransfersDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;

                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionTransfersDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, BudgTransactions.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                BudgTransactions.DeptID = 0;
                BudgTransactions.AppId = 0;
                BudgTransactions.ReferenceId = 0;
                BudgTransactions.ReferenceType = 0;
                BudgTransactions.ProjectId = 0;
                BudgTransactions.CollectionEmpId = 0;
                BudgTransactions.BankId = 0;
                BudgTransactions.PaymentTypeId = 0;
                BudgTransactions.CcId = 0;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionTransfersDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionTransfersDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {
                    child.TypeID = BudgTransactions.BudgTypeId;
                    child.DateGregorian = BudgTransactions.DateGregorian;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;
                    if (item2.Credit == null)
                    {
                        item2.Credit = 0;
                    }
                    if (item2.Debit == null)
                    {
                        item2.Debit = 0;
                    }
                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    if (item2 == null) return await Result<BudgTransactionTransfersDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionTransfersDto>(newEntity);


                return await Result<BudgTransactionTransfersDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionTransfersDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult<BudgTransactionTransfersEditDto>> UpdateTransfers(BudgTransactionTransfersEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionTransfersEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;

                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 2);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionTransfersEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }

                if (BudgTransactions == null) return await Result<BudgTransactionTransfersEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionTransfersEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                BudgTransactions.DeptID = 0;
                BudgTransactions.AppId = 0;
                BudgTransactions.ReferenceId = 0;
                BudgTransactions.ReferenceType = 0;
                BudgTransactions.ProjectId = 0;
                BudgTransactions.CollectionEmpId = 0;
                BudgTransactions.BankId = 0;
                BudgTransactions.PaymentTypeId = 0;
                BudgTransactions.CcId = 0;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionTransfersEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {
                    child.TypeID = BudgTransactions.BudgTypeId;
                    child.DateGregorian = BudgTransactions.DateGregorian;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;
                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }
                        if (item2 == null) return await Result<BudgTransactionTransfersEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionTransfersEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");
                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;
                        item3.TypeID = BudgTransactions.BudgTypeId;
                        item3.DateGregorian = BudgTransactions.DateGregorian;
                        if (item3.Credit == null)
                        {
                            item3.Credit = 0;
                        }
                        if (item3.Debit == null)
                        {
                            item3.Debit = 0;
                        }


                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionTransfersEditDto>.SuccessAsync(_mapper.Map<BudgTransactionTransfersEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionTransfersEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }


        public async Task<IResult<BudgTransactionLinksDto>> AddLinks(BudgTransactionLinksVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionLinksDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionLinksDto;
                if (BudgTransactions == null) return await Result<BudgTransactionLinksDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionLinksDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, BudgTransactions.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionLinksDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionLinksDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {
                    child.CcId ??= 0;
                    child.Cc2Id ??= 0;
                    child.Cc3Id ??= 0;
                    child.Rate ??= 0;
                    child.ExchangeRate ??= 0;
                    child.CurrencyId ??= 0;
                    child.ReferenceTypeId ??= 0;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;
                    item2.ExpenseId = BudgTransactions.ExpenseId;

                    if (item2.Credit == null)
                    {
                        item2.Credit = 0;
                    }
                    if (item2.Debit == null)
                    {
                        item2.Debit = 0;
                    }
                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    if (item2 == null) return await Result<BudgTransactionLinksDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }

                /// UpdateStatus
                //if (newEntity.Id>0)
                //{
                //    var itemStatus = await gbpositoryManager.BudgTransactionsRepository.GetById(item.ReferenceNo ?? 0);
                //    if (itemStatus == null) return Result<BudgTransactionLinksDto>.Fail($"--- there is no Data with this id: {newEntity.Id}---");
                //  // تم تحويلة الى ارتباط نهائي
                //    itemStatus.StatusId = 3;
                //    gbpositoryManager.BudgTransactionsRepository.Update(itemStatus);
                //    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                //}


                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionLinksDto>(newEntity);


                return await Result<BudgTransactionLinksDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionLinksDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }
        public async Task<IResult<BudgTransactionLinksInitialDto>> AddLinks(BudgTransactionLinksInitialVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;

                    if (item2.Credit == null)
                    {
                        item2.Credit = 0;
                    }
                    if (item2.Debit == null)
                    {
                        item2.Debit = 0;
                    }
                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    if (item2 == null) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }

                /// UpdateStatus
                if (newEntity.Id > 0)
                {
                    var itemStatus = await gbpositoryManager.BudgTransactionsRepository.GetById(item.ReferenceNo ?? 0);
                    if (itemStatus == null) return Result<BudgTransactionLinksInitialDto>.Fail($"--- there is no Data with this id: {newEntity.Id}---");
                    itemStatus.StatusId = 1;
                    gbpositoryManager.BudgTransactionsRepository.Update(itemStatus);
                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }


                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionLinksInitialDto>(newEntity);


                return await Result<BudgTransactionLinksInitialDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionLinksInitialDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }
        public async Task<IResult<BudgTransactionLinksEditDto>> UpdateLinks(BudgTransactionLinksEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionLinksEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionLinksEditDto;

                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 5);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionLinksEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }
                if (BudgTransactions == null) return await Result<BudgTransactionLinksEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionLinksEditDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionLinksEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionLinksEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {
                    child.CcId ??= 0;
                    child.Cc2Id ??= 0;
                    child.Cc3Id ??= 0;
                    child.Rate ??= 0;
                    child.ExchangeRate ??= 0;
                    child.CurrencyId ??= 0;
                    child.ReferenceTypeId ??= 0;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;
                        item2.ExpenseId = BudgTransactions.ExpenseId;

                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }
                        if (item2 == null) return await Result<BudgTransactionLinksEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionLinksEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");
                        child.CcId ??= 0;
                        child.Cc2Id ??= 0;
                        child.Cc3Id ??= 0;
                        child.Rate ??= 0;
                        child.ExchangeRate ??= 0;
                        child.CurrencyId ??= 0;
                        child.ReferenceTypeId ??= 0;
                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;
                        item3.ExpenseId = BudgTransactions.ExpenseId;

                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }
                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionLinksEditDto>.SuccessAsync(_mapper.Map<BudgTransactionLinksEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionLinksEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }
        public async Task<IResult<BudgTransactionLinksInitialDto>> AddLinksInitial(BudgTransactionLinksInitialVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {


                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionLinksInitialDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, entity.BudgTransactionDto.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                BudgTransactions.CustomerId = 0;
                BudgTransactions.CcId = 0;

                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    child.CcId ??= 0;
                    child.Cc2Id ??= 0;
                    child.Cc3Id ??= 0;
                    child.Rate ??= 0;
                    child.ExchangeRate ??= 0;
                    child.CurrencyId ??= 0;
                    child.ReferenceTypeId ??= 0;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;

                    if (item2.Credit == null)
                    {
                        item2.Credit = 0;
                    }
                    if (item2.Debit == null)
                    {
                        item2.Debit = 0;
                    }
                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    item2.ExpenseId = BudgTransactions.ExpenseId;
                    if (item2 == null) return await Result<BudgTransactionLinksInitialDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionLinksInitialDto>(newEntity);


                return await Result<BudgTransactionLinksInitialDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionLinksInitialDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }
        public async Task<IResult<BudgTransactionLinksInitialEditDto>> UpdateLinksInitial(BudgTransactionLinksInitialEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionLinksInitialEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;
                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 8);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionLinksInitialEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }
                if (BudgTransactions == null) return await Result<BudgTransactionLinksInitialEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionLinksInitialEditDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionLinksInitialEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionLinksInitialEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {
                    child.CcId ??= 0;
                    child.Cc2Id ??= 0;
                    child.Cc3Id ??= 0;
                    child.Rate ??= 0;
                    child.ExchangeRate ??= 0;
                    child.CurrencyId ??= 0;
                    child.ReferenceTypeId ??= 0;
                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;
                        item2.ExpenseId = BudgTransactions.ExpenseId;


                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }
                        if (item2 == null) return await Result<BudgTransactionLinksInitialEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionLinksInitialEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");
                        child.CcId ??= 0;
                        child.Cc2Id ??= 0;
                        child.Cc3Id ??= 0;
                        child.Rate ??= 0;
                        child.ExchangeRate ??= 0;
                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;
                        item3.ExpenseId = BudgTransactions.ExpenseId;


                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }

                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionLinksInitialEditDto>.SuccessAsync(_mapper.Map<BudgTransactionLinksInitialEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionLinksInitialEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }


        public async Task<IResult<BudgTransactionDiscountsDto>> AddDiscounts(BudgTransactionDiscountsVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionDiscountsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionDiscountsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionDiscountsDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, entity.BudgTransactionDto.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                BudgTransactions.CustomerId = 0;
                BudgTransactions.CcId = 0;
                BudgTransactions.DeptID = 0;
                BudgTransactions.AppId = 0;
                BudgTransactions.ReferenceId = 0;
                BudgTransactions.ReferenceType = 0;
                BudgTransactions.ProjectId = 0;
                BudgTransactions.CollectionEmpId = 0;
                BudgTransactions.BankId = 0;
                BudgTransactions.PaymentTypeId = 0;
                BudgTransactions.CcId = 0;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionDiscountsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionDiscountsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    child.CcId ??= 0;
                    child.Cc2Id ??= 0;
                    child.Cc3Id ??= 0;
                    child.Rate ??= 0;
                    child.ExchangeRate ??= 0;
                    child.CurrencyId ??= 0;
                    child.ReferenceTypeId ??= 0;
                    child.TypeID = BudgTransactions.BudgTypeId;
                    child.DateGregorian = BudgTransactions.DateGregorian;

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;

                    item2.IsDeleted = false;
                    if (item2.Credit == null)
                    {
                        item2.Credit = 0;
                    }
                    if (item2.Debit == null)
                    {
                        item2.Debit = 0;
                    }

                    item2.CreatedOn = DateTime.UtcNow;
                    item2.ExpenseId = 0;
                    item2.CreatedBy = (int)session.UserId;
                    if (item2 == null) return await Result<BudgTransactionDiscountsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionDiscountsDto>(newEntity);


                return await Result<BudgTransactionDiscountsDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionDiscountsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }
        public async Task<IResult<BudgTransactionDiscountsEditDto>> UpdateDiscounts(BudgTransactionDiscountsEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionDiscountsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;

                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 4);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionDiscountsEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }
                if (BudgTransactions == null) return await Result<BudgTransactionDiscountsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionDiscountsEditDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }

                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionDiscountsEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                BudgTransactions.DeptID = 0;
                BudgTransactions.AppId = 0;
                BudgTransactions.ReferenceId = 0;
                BudgTransactions.ReferenceType = 0;
                BudgTransactions.ProjectId = 0;
                BudgTransactions.CollectionEmpId = 0;
                BudgTransactions.BankId = 0;
                BudgTransactions.PaymentTypeId = 0;
                BudgTransactions.CcId = 0;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionDiscountsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {
                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;
                    child.ReferenceTypeId = 0;
                    child.TypeID = BudgTransactions.BudgTypeId;
                    child.DateGregorian = BudgTransactions.DateGregorian;

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        //item2.UpdateUserId = 0;
                        //item2.DeleteUserId = 0;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;
                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }
                        if (item2 == null) return await Result<BudgTransactionDiscountsEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionDiscountsEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");


                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.CcId ??= 0;
                        item3.Cc2Id ??= 0;
                        item3.Cc3Id ??= 0;
                        item3.Rate ??= 0;
                        item3.ExchangeRate ??= 0;
                        item3.CurrencyId ??= 0;
                        item3.ReferenceTypeId = 0;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;
                        if (item3.Credit == null)
                        {
                            item3.Credit = 0;
                        }
                        if (item3.Debit == null)
                        {
                            item3.Debit = 0;
                        }


                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionDiscountsEditDto>.SuccessAsync(_mapper.Map<BudgTransactionDiscountsEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionDiscountsEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }
        public async Task<IResult<BudgTransactionInitialYearDto>> AddDetaileYear(BudgTransactionInitialYearVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null || entity.BudgTransactionDto == null || entity.Childrenyear == null || entity.Childrenyear.Count == 0)
                return await Result<BudgTransactionInitialYearDto>.FailAsync($"Error: Invalid entity.");

            try
            {
                bool exitupdate = false;  // تصحيح هنا: يتم استخدام = وليس ==
                string masseg = "";
                string massegAdd = "";
                entity.BudgTransactionDto.FacilityId = session.FacilityId;
                entity.BudgTransactionDto.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(entity.BudgTransactionDto.DateGregorian))
                {
                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(entity.BudgTransactionDto.FinYear ?? 0, entity.BudgTransactionDto.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionInitialYearDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                bool isAdded = false;
                BudgTransaction newEntityCosts = null;
                BudgTransaction newEntityMoney = null;

                foreach (var child in entity.Childrenyear)
                {
                    var existingCostDetail = await gbpositoryManager.BudgTransactionDetaileRepository
                        .GetOneVW(x => x.AccAccountId == child.AccAccountId && x.Finyear == session.FinYear && x.TypeID == 2);

                    if (existingCostDetail != null && existingCostDetail.AccAccountId > 0)
                    {
                        var costDetail = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(existingCostDetail.Id);
                        if (costDetail != null)
                        {
                            var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(costDetail.TId ?? 0, 7);
                            if (Statuse == 2)
                            {
                                exitupdate = true;  // التصحيح هنا
                            }
                            if (exitupdate == false)
                            {
                                UpdateDetailValues(costDetail, child, 2);
                                gbpositoryManager.BudgTransactionDetaileRepository.Update(costDetail);
                                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                            else
                            {
                                masseg = masseg + ',' + child.AccAccountCode;
                            }
                        }
                    }
                    else if (child.CostsValue > 0)
                    {
                        if (newEntityCosts == null)
                        {
                            var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, entity.BudgTransactionDto.DateGregorian, session.FinYear, 7);
                            entity.BudgTransactionDto.DocTypeId = 7;
                            entity.BudgTransactionDto.StatusId = 1;
                            entity.BudgTransactionDto.Code = code;
                            var item = _mapper.Map<BudgTransaction>(entity.BudgTransactionDto);
                            item.BudgTypeId = 2;
                            newEntityCosts = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                            await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                            if (newEntityCosts == null)
                                return await Result<BudgTransactionInitialYearDto>.FailAsync($"Error: Failed to create cost entity.");
                        }
                        AddNewDetail(child, newEntityCosts.Id, 2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        isAdded = true;
                        massegAdd = massegAdd + ',' + child.AccAccountCode;
                    }

                    var existingMoneyDetail = await gbpositoryManager.BudgTransactionDetaileRepository
                        .GetOneVW(x => x.AccAccountId == child.AccAccountId && x.Finyear == session.FinYear && x.TypeID == 1);

                    if (existingMoneyDetail != null && existingMoneyDetail.AccAccountId > 0)
                    {
                        var moneyDetail = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(existingMoneyDetail.Id);
                        if (moneyDetail != null)
                        {
                            var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(moneyDetail.TId ?? 0, 1);
                            if (Statuse == 2)
                            {
                                exitupdate = true;
                            }


                            if (exitupdate == false)
                            {
                                UpdateDetailValues(moneyDetail, child, 1);
                                gbpositoryManager.BudgTransactionDetaileRepository.Update(moneyDetail);
                                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                            else
                            {
                                masseg = masseg + ',' + child.AccAccountCode;
                            }

                        }
                    }
                    else if (child.MoneyValue > 0)
                    {
                        if (newEntityMoney == null)
                        {
                            var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, entity.BudgTransactionDto.DateGregorian, session.FinYear, 1);
                            entity.BudgTransactionDto.DocTypeId = 1;
                            entity.BudgTransactionDto.Code = code;
                            entity.BudgTransactionDto.StatusId = 1;
                            var item = _mapper.Map<BudgTransaction>(entity.BudgTransactionDto);
                            item.BudgTypeId = 1;
                            newEntityMoney = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                            await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                            if (newEntityMoney == null)
                                return await Result<BudgTransactionInitialYearDto>.FailAsync($"Error: Failed to create money entity.");
                        }
                        AddNewDetail(child, newEntityMoney.Id, 1);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        isAdded = true;
                        massegAdd = massegAdd + ',' + child.AccAccountCode;
                    }
                }

                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                if (isAdded == true)
                {
                    return await Result<BudgTransactionInitialYearDto>.SuccessAsync(localization.GetMessagesResource("success") + " للبنود" + massegAdd);
                }
                if (exitupdate == true)
                {
                    return await Result<BudgTransactionInitialYearDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse") + " للبنود" + masseg);
                }

                if (newEntityCosts == null)
                    return await Result<BudgTransactionInitialYearDto>.SuccessAsync(new BudgTransactionInitialYearDto(), "Items Update successfully.");

                var entityMap = _mapper.Map<BudgTransactionInitialYearDto>(newEntityCosts);
                return await Result<BudgTransactionInitialYearDto>.SuccessAsync(entityMap, "Items added successfully.");
            }
            catch (Exception exc)
            {
                return await Result<BudgTransactionInitialYearDto>.FailAsync($"Exception: {exc.Message}");
            }
        }

        //public async Task<IResult<BudgTransactionInitialYearDto>> AddDetaileYear(BudgTransactionInitialYearVM entity, CancellationToken cancellationToken = default)
        //{
        //    if (entity == null || entity.BudgTransactionDto == null || entity.Childrenyear == null || entity.Childrenyear.Count == 0)
        //        return await Result<BudgTransactionInitialYearDto>.FailAsync($"Error: Invalid entity.");

        //    try
        //    {
        //        bool exitupdate = false;
        //        string masseg = "";
        //        entity.BudgTransactionDto.FacilityId = session.FacilityId;
        //        entity.BudgTransactionDto.FinYear = session.FinYear;
        //        if (!string.IsNullOrEmpty(entity.BudgTransactionDto.DateGregorian))
        //        {

        //            bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(entity.BudgTransactionDto.FinYear ?? 0, entity.BudgTransactionDto.DateGregorian);
        //            if (!chkPeriod)
        //                return await Result<BudgTransactionInitialYearDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
        //        }

        //        await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
        //        bool isAdded = false;
        //        BudgTransaction newEntityCosts = null;
        //        BudgTransaction newEntityMoney = null;

        //        foreach (var child in entity.Childrenyear)
        //        {
        //            var existingCostDetail = await gbpositoryManager.BudgTransactionDetaileRepository
        //                .GetOneVW(x => x.AccAccountId == child.AccAccountId && x.Finyear == session.FinYear && x.TypeID == 2);

        //            if (existingCostDetail != null && existingCostDetail.AccAccountId > 0)
        //            {
        //                var costDetail = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(existingCostDetail.Id);
        //                if (costDetail != null)
        //                {

        //                    var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(costDetail.TId??0, 1);
        //                    if (Statuse == 2)
        //                    {
        //                        exitupdate == true;

        //                    }
        //                    if (exitupdate == false)
        //                    {
        //                        UpdateDetailValues(costDetail, child, 2);
        //                        gbpositoryManager.BudgTransactionDetaileRepository.Update(costDetail);
        //                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //                    }
        //                    else
        //                    {
        //                        masseg = masseg + child.AccAccountCode;
        //                    }

        //                }
        //            }
        //            else if (child.CostsValue > 0)
        //            {
        //                if (newEntityCosts == null)
        //                {
        //                    var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId,entity.BudgTransactionDto.DateGregorian, session.FinYear, 7);
        //                    entity.BudgTransactionDto.DocTypeId = 7;
        //                    entity.BudgTransactionDto.StatusId = 1;
        //                    entity.BudgTransactionDto.Code = code;
        //                    var item = _mapper.Map<BudgTransaction>(entity.BudgTransactionDto);
        //                    item.BudgTypeId = 2;
        //                    newEntityCosts = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
        //                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //                    if (newEntityCosts == null)
        //                        return await Result<BudgTransactionInitialYearDto>.FailAsync($"Error: Failed to create cost entity.");
        //                }
        //                AddNewDetail(child, newEntityCosts.Id, 2);
        //                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //                isAdded = true;
        //            }

        //            var existingMoneyDetail = await gbpositoryManager.BudgTransactionDetaileRepository
        //                .GetOneVW(x => x.AccAccountId == child.AccAccountId && x.Finyear == session.FinYear && x.TypeID == 1);

        //            if (existingMoneyDetail != null && existingMoneyDetail.AccAccountId > 0)
        //            {
        //                var moneyDetail = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(existingMoneyDetail.Id);
        //                if (moneyDetail != null)
        //                {
        //                    var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(moneyDetail.TId ?? 0, 7);
        //                    if (Statuse == 2)
        //                    {
        //                        return await Result<BudgTransactionInitialYearDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

        //                    }

        //                    UpdateDetailValues(moneyDetail, child, 1);
        //                    gbpositoryManager.BudgTransactionDetaileRepository.Update(moneyDetail);
        //                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //                }
        //            }
        //            else if (child.MoneyValue > 0)
        //            {
        //                if (newEntityMoney == null)
        //                {
        //                    var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, entity.BudgTransactionDto.DateGregorian, session.FinYear, 1);
        //                    entity.BudgTransactionDto.DocTypeId = 1;
        //                    entity.BudgTransactionDto.Code = code;
        //                    entity.BudgTransactionDto.StatusId = 1;
        //                    var item = _mapper.Map<BudgTransaction>(entity.BudgTransactionDto);
        //                    item.BudgTypeId = 1;
        //                    newEntityMoney = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
        //                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //                    if (newEntityMoney == null)
        //                        return await Result<BudgTransactionInitialYearDto>.FailAsync($"Error: Failed to create money entity.");
        //                }
        //                AddNewDetail(child, newEntityMoney.Id, 1);
        //                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //                isAdded = true;
        //            }
        //        }
        //        if (exitupdate == true)
        //        {
        //            return await Result<BudgTransactionInitialYearDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse")+ masseg);

        //        }
        //        await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
        //        if (newEntityCosts == null)
        //            return await Result<BudgTransactionInitialYearDto>.SuccessAsync(new BudgTransactionInitialYearDto(), "Items Update successfully."); ;

        //        var entityMap = _mapper.Map<BudgTransactionInitialYearDto>(newEntityCosts);
        //        return await Result<BudgTransactionInitialYearDto>.SuccessAsync(entityMap, "Items added successfully.");
        //    }
        //    catch (Exception exc)
        //    {
        //        return await Result<BudgTransactionInitialYearDto>.FailAsync($"Exception: {exc.Message}");
        //    }
        //}





        private void UpdateDetailValues(BudgTransactionDetaile itemDetaile, BudgTransactionDetaileYearDto child, int typeId)
        {

            itemDetaile.Credit = typeId == 2 ? child.CostsValue ?? 0 : child.MoneyValue ?? 0;
            itemDetaile.Debit = 0;
        }

        // دالة لإضافة تفاصيل جديدة
        private void AddNewDetail(BudgTransactionDetaileYearDto child, long transactionId, int typeId)
        {

            child.Credit = typeId == 2 ? child.CostsValue ?? 0 : child.MoneyValue ?? 0;
            child.Debit = 0;
            child.CcId ??= 0;
            child.Cc2Id ??= 0;
            child.Cc3Id ??= 0;
            child.Rate ??= 0;
            child.ExchangeRate ??= 0;
            child.CurrencyId ??= 0;
            child.ReferenceTypeId ??= 0;
            var newDetail = _mapper.Map<BudgTransactionDetaile>(child);
            newDetail.TId = transactionId;
            newDetail.TypeID = typeId;
            newDetail.CcId = 0;
            newDetail.ExpenseId = 0;
            newDetail.IsDeleted = false;
            newDetail.CreatedOn = DateTime.Now;
            newDetail.CreatedBy = (int)session.UserId;


            gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(newDetail);


        }

        public async Task<IResult<BudgTransactionLinksInitialmultipleDto>> AddLinksMultiple(BudgTransactionLinksInitialmultipleVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionLinksInitialmultipleDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionLinksInitialmultipleDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionLinksInitialmultipleDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, entity.BudgTransactionDto.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                BudgTransactions.CustomerId = 0;
                BudgTransactions.CcId = 0;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionLinksInitialmultipleDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionLinksInitialmultipleDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;

                    if (item2.Credit == null)
                    {
                        item2.Credit = 0;
                    }
                    if (item2.Debit == null)
                    {
                        item2.Debit = 0;
                    }
                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    item2.TypeID = BudgTransactions.BudgTypeId;
                    if (item2 == null) return await Result<BudgTransactionLinksInitialmultipleDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionLinksInitialmultipleDto>(newEntity);


                return await Result<BudgTransactionLinksInitialmultipleDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionLinksInitialmultipleDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<BudgTransactionLinksInitialmultipleEditDto>> UpdateLinksMultiple(BudgTransactionLinksInitialmultipleEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;
                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 8);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }
                if (BudgTransactions == null) return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;
                        item2.TypeID = BudgTransactions.BudgTypeId;
                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }
                        if (item2 == null) return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");
                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;
                        item2.TypeID = BudgTransactions.BudgTypeId;
                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }

                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionLinksInitialmultipleEditDto>.SuccessAsync(_mapper.Map<BudgTransactionLinksInitialmultipleEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionLinksInitialmultipleEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<BudgTransactionlinkFinalMultiDto>> AddlinkFinalMultiple(BudgTransactionlinkFinalMultiVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionlinkFinalMultiDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionlinkFinalMultiDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionlinkFinalMultiDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }
                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, entity.BudgTransactionDto.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionlinkFinalMultiDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionlinkFinalMultiDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.Id = 0;
                    item2.IsDeleted = false;

                    if (item2.Credit == null)
                    {
                        item2.Credit = 0;
                    }
                    if (item2.Debit == null)
                    {
                        item2.Debit = 0;
                    }
                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    item2.TypeID = BudgTransactions.BudgTypeId;
                    if (item2 == null) return await Result<BudgTransactionlinkFinalMultiDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionlinkFinalMultiDto>(newEntity);


                return await Result<BudgTransactionlinkFinalMultiDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionlinkFinalMultiDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }



        public async Task<IResult<BudgTransactionlinkFinalMultiEditDto>> UpdatelinkFinalMultiple(BudgTransactionlinkFinalMultieEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;
                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 5);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }
                if (BudgTransactions == null) return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;
                        item2.TypeID = BudgTransactions.BudgTypeId;
                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }
                        if (item2 == null) return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");
                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;
                        item2.TypeID = BudgTransactions.BudgTypeId;
                        if (item2.Credit == null)
                        {
                            item2.Credit = 0;
                        }
                        if (item2.Debit == null)
                        {
                            item2.Debit = 0;
                        }

                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionlinkFinalMultiEditDto>.SuccessAsync(_mapper.Map<BudgTransactionlinkFinalMultiEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionlinkFinalMultiEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<BudgTransactionReinforcementsDto>> AddReinforcements(BudgTransactionReinforcementsVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionReinforcementsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionReinforcementsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionReinforcementsDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, BudgTransactions.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                BudgTransactions.DeptID = 0;
                BudgTransactions.AppId = 0;
                BudgTransactions.ReferenceId = 0;
                BudgTransactions.ReferenceType = 0;
                BudgTransactions.ProjectId = 0;
                BudgTransactions.CollectionEmpId = 0;
                BudgTransactions.BankId = 0;
                BudgTransactions.PaymentTypeId = 0;
                BudgTransactions.CcId = 0;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionReinforcementsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionReinforcementsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    child.Credit = child.Value;
                    if (child.Credit == null)
                    {
                        child.Credit = 0;
                    }
                    if (child.Debit == null)
                    {
                        child.Debit = 0;
                    }
                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;
                    child.TypeID = BudgTransactions.BudgTypeId;
                    child.DateGregorian = BudgTransactions.DateGregorian;

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;

                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    if (item2 == null) return await Result<BudgTransactionReinforcementsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionReinforcementsDto>(newEntity);


                return await Result<BudgTransactionReinforcementsDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionReinforcementsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<BudgTransactionReinforcementsEditDto>> UpdateReinforcements(BudgTransactionReinforcementsEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionReinforcementsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;
                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 3);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionReinforcementsEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }

                if (BudgTransactions == null) return await Result<BudgTransactionReinforcementsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionReinforcementsEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionReinforcementsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    child.Credit = child.Value;
                    if (child.Credit == null)
                    {
                        child.Credit = 0;
                    }
                    if (child.Debit == null)
                    {
                        child.Debit = 0;
                    }

                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.TypeID = BudgTransactions.BudgTypeId;
                        item2.DateGregorian = BudgTransactions.DateGregorian;

                        item2.CreatedBy = (int)session.UserId;

                        if (item2 == null) return await Result<BudgTransactionReinforcementsEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionReinforcementsEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");


                        child.Credit = child.Value;
                        if (child.Credit == null)
                        {
                            child.Credit = 0;
                        }
                        if (child.Debit == null)
                        {
                            child.Debit = 0;
                        }
                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.TypeID = BudgTransactions.BudgTypeId;
                        item3.DateGregorian = BudgTransactions.DateGregorian;

                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;



                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionReinforcementsEditDto>.SuccessAsync(_mapper.Map<BudgTransactionReinforcementsEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionReinforcementsEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<BudgTransactionInitialCreditsDto>> AddInitialCredits(BudgTransactionInitialCreditsVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionInitialCreditsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionDto;
                if (BudgTransactions == null) return await Result<BudgTransactionInitialCreditsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                BudgTransactions.FacilityId = session.FacilityId;
                BudgTransactions.FinYear = session.FinYear;
                if (!string.IsNullOrEmpty(BudgTransactions.DateGregorian))
                {

                    bool chkPeriod = await accRepositoryManager.AccPeriodsRepository.CheckDateInPeriodByYear(BudgTransactions.FinYear ?? 0, BudgTransactions.DateGregorian);
                    if (!chkPeriod)
                        return await Result<BudgTransactionInitialCreditsDto>.FailAsync(localization.GetMessagesResource("DateOutOfPERIOD"));
                }


                var code = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsCode(session.FacilityId, BudgTransactions.DateGregorian, session.FinYear, BudgTransactions.DocTypeId ?? 0);
                BudgTransactions.Code = code;
                BudgTransactions.DeptID = 0;
                BudgTransactions.AppId = 0;
                BudgTransactions.ReferenceId = 0;
                BudgTransactions.ReferenceType = 0;
                BudgTransactions.ProjectId = 0;
                BudgTransactions.CollectionEmpId = 0;
                BudgTransactions.BankId = 0;
                BudgTransactions.PaymentTypeId = 0;
                BudgTransactions.CcId = 0;
                var item = _mapper.Map<BudgTransaction>(BudgTransactions);
                item.BudgTypeId = 1;
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var newEntity = await gbpositoryManager.BudgTransactionsRepository.AddAndReturn(item);
                if (newEntity == null) return await Result<BudgTransactionInitialCreditsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionInitialCreditsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    child.Credit = child.Value;
                    if (child.Credit == null)
                    {
                        child.Credit = 0;
                    }
                    if (child.Debit == null)
                    {
                        child.Debit = 0;
                    }
                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;
                    child.TypeID = BudgTransactions.BudgTypeId;
                    child.DateGregorian = BudgTransactions.DateGregorian;

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    item2.TId = newEntity.Id;
                    item2.IsDeleted = false;

                    item2.CreatedOn = DateTime.UtcNow;
                    item2.CreatedBy = (int)session.UserId;
                    if (item2 == null) return await Result<BudgTransactionInitialCreditsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);

                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<BudgTransactionInitialCreditsDto>(newEntity);


                return await Result<BudgTransactionInitialCreditsDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionInitialCreditsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<BudgTransactionInitialCreditsEditDto>> UpdateInitialCredits(BudgTransactionInitialCreditsEditVM entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<BudgTransactionInitialCreditsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var BudgTransactions = entity.BudgTransactionEditDto;

                var Statuse = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(BudgTransactions.Id, BudgTransactions.DocTypeId ?? 1);
                if (Statuse == 2)
                {
                    return await Result<BudgTransactionInitialCreditsEditDto>.FailAsync(localization.GetMessagesResource("chkitemsUpdaeStatuse"));

                }

                if (BudgTransactions == null) return await Result<BudgTransactionInitialCreditsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                var item = await gbpositoryManager.BudgTransactionsRepository.GetById(BudgTransactions.Id);
                if (item == null) return await Result<BudgTransactionInitialCreditsEditDto>.FailAsync($"--- there is no Data with this id: {BudgTransactions.Id}---");
                item.ModifiedOn = DateTime.UtcNow;

                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(BudgTransactions, item);

                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                gbpositoryManager.BudgTransactionsRepository.Update(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.Children == null || entity.Children.Count == 0) return await Result<BudgTransactionInitialCreditsEditDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
                foreach (var child in entity.Children)
                {

                    child.Credit = child.Value;
                    if (child.Credit == null)
                    {
                        child.Credit = 0;
                    }
                    if (child.Debit == null)
                    {
                        child.Debit = 0;
                    }

                    child.CcId = 0;
                    child.Cc2Id = 0;
                    child.Cc3Id = 0;
                    child.Rate = 0;
                    child.ExchangeRate = 0;
                    child.CurrencyId = 0;
                    child.DateGregorian = BudgTransactions.DateGregorian;

                    var item2 = _mapper.Map<BudgTransactionDetaile>(child);
                    if (item2.Id == 0 && item2.IsDeleted != true)
                    {
                        item2.TId = item.Id;
                        item2.TypeID = 1;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.Now;
                        item2.CreatedBy = (int)session.UserId;
                        item2.ExpenseId = 0;

                        if (item2 == null) return await Result<BudgTransactionInitialCreditsEditDto>.FailAsync($"Error in Add of: BudgTransactionDetaile, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgTransactionDetaileRepository.AddAndReturn(item2);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                    else
                    {
                        var item3 = await gbpositoryManager.BudgTransactionDetaileRepository.GetById(item2.Id);
                        if (item3 == null) return await Result<BudgTransactionInitialCreditsEditDto>.FailAsync($"--- there is no Data in BudgTransactionDetaile with this id: {item2.Id}---");


                        child.Credit = child.Value;
                        if (child.Credit == null)
                        {
                            child.Credit = 0;
                        }
                        if (child.Debit == null)
                        {
                            child.Debit = 0;
                        }
                        child.DateGregorian = BudgTransactions.DateGregorian;

                        _mapper.Map(child, item3);
                        item3.TId = item.Id;
                        item3.TypeID = 1;
                        item3.ModifiedOn = DateTime.UtcNow;
                        item3.ModifiedBy = (int)session.UserId;

                        item3.ExpenseId = 0;

                        gbpositoryManager.BudgTransactionDetaileRepository.Update(item3);
                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }



                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<BudgTransactionInitialCreditsEditDto>.SuccessAsync(_mapper.Map<BudgTransactionInitialCreditsEditDto>(item), "Item updated successfully");
            }
            catch (Exception exc)
            {

                return await Result<BudgTransactionInitialCreditsEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<int?> GetBudgTransactionsStatuse(long Id, int DocTypeID)
        {
            int? StatuseId = 0;
            StatuseId = await gbpositoryManager.BudgTransactionsRepository.GetBudgTransactionsStatuse(Id, DocTypeID);
            if (StatuseId != 0)
            {
                return StatuseId;
            }
            return StatuseId ?? 0;

        }
    }

}