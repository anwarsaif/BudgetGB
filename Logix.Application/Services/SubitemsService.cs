using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.GB;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.ACC;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.ACC;
using Logix.Application.Interfaces.IServices.Gb;
using Logix.Application.Services;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Logix.Domain.GB;


namespace Logix.Application.Services.GB
{
    public class SubitemsService : GenericQueryService<AccAccount, SubitemsDto, AccAccountsVw>, ISubitemsService
    {
        private readonly IGbRepositoryManager gbpositoryManager;

        private readonly IMapper _mapper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;
        private readonly IAccRepositoryManager accRepositoryManager;

        public SubitemsService(IQueryRepository<AccAccount> queryRepository, IGbRepositoryManager gbpositoryManager, IMapper mapper, ISessionHelper session, ILocalizationService localization, ISysConfigurationAppHelper SysConfigurationAppHelper, IAccRepositoryManager AccRepositoryManager) : base(queryRepository, mapper)
        {
            this.gbpositoryManager = gbpositoryManager;
            this._mapper = mapper;

            this.session = session;
            this.localization = localization;
            this.sysConfigurationAppHelper = SysConfigurationAppHelper;
            this.accRepositoryManager = AccRepositoryManager;
        }

        public async Task<IResult<SubitemsDto>> AddExcel(SubitemsDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<SubitemsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                if (!string.IsNullOrEmpty(entity.AccAccountCode))
                {
                    var AccAccountCode = await gbpositoryManager.SubitemsRepository.GetAll(s => s.AccAccountCode == entity.AccAccountCode && s.FacilityId == session.FacilityId);
                    if (AccAccountCode != null)
                    {
                        if (AccAccountCode.Count() > 0)
                        {
                            return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("ItemsNumberAlready"));
                        }
                    }

                }
                var AccountRestrictedtLevel = await sysConfigurationAppHelper.GetValue(260, session.FacilityId);
                int AccountLevel = 0;
                long AccGroupId = 0;
                var AccountLeveldata = await gbpositoryManager.SubitemsRepository.GetOne(s => s.AccountLevel, x => x.AccAccountId == entity.AccAccountParentId && x.FacilityId == session.FacilityId && x.IsDeleted == false);
                if (AccountRestrictedtLevel == "1")
                {
                    string subAccountLevel = await sysConfigurationAppHelper.GetValue(99, session.FacilityId);
                    var parentAccountLevel = await gbpositoryManager.SubitemsRepository.GetAccountLevel(entity.AccAccountParentId ?? 0, session.FacilityId);
                    if (string.IsNullOrEmpty(subAccountLevel))
                    {

                        return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("levelItemsConfiguration"));
                    }
                    if (parentAccountLevel >= int.Parse(subAccountLevel))
                    {
                        return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("Itemslargerlevel"));


                    }
                    if (entity.IsSub == false)
                    {
                        if (int.Parse(subAccountLevel) != parentAccountLevel + 1)
                        {

                            return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("Itemslargerlevel"));
                        }
                    }
                }
                var item = _mapper.Map<AccAccount>(entity);
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newEntity = await gbpositoryManager.SubitemsRepository.AddAndReturn(item);
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

       
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<SubitemsDto>(newEntity);


                return await Result<SubitemsDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<SubitemsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }
        public async Task<IResult<SubitemsDto>> Add(SubitemsDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<SubitemsDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                if (entity.Numbring == false && string.IsNullOrEmpty(entity.AccAccountCode))
                {

                    return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("Enteraccountnumberfirst"));

                }
                entity.AccAccountParentId ??= 0;
                if (!string.IsNullOrEmpty(entity.AccAccountCode) && entity.Numbring == false)
                {
                    var AccAccountCode = await gbpositoryManager.SubitemsRepository.GetAll(s => s.AccAccountCode == entity.AccAccountCode && s.FacilityId == session.FacilityId);
                    if (AccAccountCode != null)
                    {
                        if (AccAccountCode.Count() > 0)
                        {
                            return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("ItemsNumberAlready"));
                        }
                    }

                }
                var AccountRestrictedtLevel = await sysConfigurationAppHelper.GetValue(260, session.FacilityId);
                int AccountLevel = 0;
                long AccGroupId = 0;
                var AccountLeveldata = await gbpositoryManager.SubitemsRepository.GetOne(s => s.AccountLevel, x => x.AccAccountId == entity.AccAccountParentId && x.FacilityId == session.FacilityId && x.IsDeleted == false && x.SystemId==65);
                if (AccountLeveldata == null)
                {
                    AccountLevel =  1;
                }
                else
                {
                    AccountLevel = AccountLeveldata.Value  + 1;
                }
               
                //AccGroupId = await gbpositoryManager.SubitemsRepository.GetAccGroupId(entity.AccAccountParentId ?? 0, session.FacilityId) ?? 0;
     

                if (AccountRestrictedtLevel == "1")
                {
                    string subAccountLevel = await sysConfigurationAppHelper.GetValue(99, session.FacilityId);
                    var parentAccountLevel = await gbpositoryManager.SubitemsRepository.GetAccountLevel(entity.AccAccountParentId ?? 0, session.FacilityId);
                    if (string.IsNullOrEmpty(subAccountLevel))
                    {

                        return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("levelItemsConfiguration"));
                    }
                    if (parentAccountLevel >= int.Parse(subAccountLevel))
                    {
                        return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("Itemslargerlevel"));


                    }
                    if (entity.IsSub == false)
                    {
                        if (int.Parse(subAccountLevel) != parentAccountLevel + 1)
                        {

                            return await Result<SubitemsDto>.FailAsync(localization.GetMessagesResource("Itemslargerlevel"));
                        }
                    }
                }



                //entity.AccGroupId = AccGroupId;
                entity.AccAccountType = 0;
                entity.DeptID = 0;
                entity.FacilityId = session.FacilityId;

                //=================================
                // Variables
                string accAccountCodeParent = "";
                int AccountLevelParentId = 0;
                int? noOfDigit = 0;
                string accAccountCode = null;
                long maxAccountCode = 0;
                if (entity.EnableAccountParentId == true)
                {

              
                    // Retrieve max account code and account level
                   
                        var maxAccountCodes = await gbpositoryManager.SubitemsRepository.GetAll(x => x.AccAccountParentId == entity.AccAccountParentId);

                        if (maxAccountCodes != null && maxAccountCodes.Any())
                        {
                            maxAccountCode = maxAccountCodes.Select(t => !string.IsNullOrEmpty(t.AccAccountCode) ? Convert.ToInt64(t.AccAccountCode) : 0).DefaultIfEmpty(0).Max();
                            accAccountCodeParent = maxAccountCode.ToString();
                        AccountLevelParentId = maxAccountCodes.First().AccountLevel ?? 0;


                    }

                }
                else
                {
                    var maxAccountCodes = await gbpositoryManager.SubitemsRepository.GetAll(x => x.AccGroupId == entity.AccGroupId &&x.SystemId==65 );
                    if (maxAccountCodes != null && maxAccountCodes.Any())
                    {
                        maxAccountCode = maxAccountCodes.Select(t => !string.IsNullOrEmpty(t.AccAccountCode) ? Convert.ToInt64(t.AccAccountCode) : 0).DefaultIfEmpty(0).Max();
                        accAccountCodeParent = maxAccountCode.ToString();
                        AccountLevelParentId = AccountLevel;
                    }
                }






                // Determine number of digits
                var LevelDigit = await accRepositoryManager.AccAccountsLevelRepository.GetOne(s => s.NoOfDigit, s => s.LevelId == AccountLevelParentId);
                    if (LevelDigit != null)
                    {
                        noOfDigit = LevelDigit;
                    }


                    // Generate new account code
                    int cntAccount = 0;
                    if (string.IsNullOrEmpty(entity.AccAccountCode))
                    {
                        if (noOfDigit != 0)
                        {
                            int accountIsFound = 0;
                            while (accountIsFound == 0)
                            {
                                int newAccountNumber = cntAccount + 1;
                                string newAccountCode = newAccountNumber.ToString();

                                // Calculate the remaining digits needed for padding
                                int remainingDigits = Math.Max(0, noOfDigit.Value - accAccountCodeParent.Length - newAccountCode.Length);
                                string paddedAccountCode = new string('0', remainingDigits) + newAccountCode;

                                long accAccountCodeParentValue = Convert.ToInt64(accAccountCodeParent);
                                long paddedAccountCodeValue = Convert.ToInt64(paddedAccountCode);

                                accAccountCode = (accAccountCodeParentValue + paddedAccountCodeValue).ToString();

                                // Check if the account code already exists
                                var existingAccount = await gbpositoryManager.SubitemsRepository.GetOne(X => X.AccAccountCode == accAccountCode);
                                if (existingAccount == null)
                                {
                                    accountIsFound = 1;
                                }
                                else
                                {
                                    cntAccount++;
                                }
                            }
                        }
                        else
                        {
                            accAccountCode = accAccountCodeParent + (cntAccount + 1).ToString();
                        }
                }
                else
                {
                    accAccountCode = entity.AccAccountCode;
                }
                if (entity.EnableAccountParentId == true)
                {
                    entity.AccAccountCode = accAccountCode;
                }
                else
                {
                    entity.AccAccountCode ="0"+ entity.AccGroupId + accAccountCode.ToString();

                }






                entity.AccountLevel = AccountLevel;
                //============================================
                await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = _mapper.Map<AccAccount>(entity);
                var newEntity = await gbpositoryManager.SubitemsRepository.AddAndReturn(item);

                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.EnableAccountParentId == false)
                {
                    var itemParent = await gbpositoryManager.SubitemsRepository.GetById(newEntity.AccAccountId);
                    if (itemParent == null) return Result<SubitemsDto>.Fail($"--- there is no Data with this id: {newEntity.AccAccountId}---");
                    itemParent.AccAccountParentId = newEntity.AccAccountId;
                    gbpositoryManager.SubitemsRepository.Update(itemParent);
                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }
                if (entity.ChildrenExpenses != null)
                {

                    foreach (var child in entity.ChildrenExpenses)
                    {


                        var item2 = _mapper.Map<BudgAccountExpenses>(child);
                        item2.AccAccountId = newEntity.AccAccountId;
                        item2.IsDeleted = false;
                        item2.CreatedOn = DateTime.UtcNow;
                        item2.CreatedBy = (int)session.UserId;
                        if (item2 == null) return await Result<SubitemsDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

                        var newEntity2 = await gbpositoryManager.BudgAccountExpensesRepository.AddAndReturn(item2);

                        await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }
                }
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<SubitemsDto>(newEntity);


                return await Result<SubitemsDto>.SuccessAsync(entityMap, localization.GetMessagesResource("success"));
            }
            catch (Exception exc)
            {

                return await Result<SubitemsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<long> GetAccountLevel(long AccAccountParentId)
        {
            var parentAccountLevel = await gbpositoryManager.SubitemsRepository.GetAccountLevel(AccAccountParentId , session.FacilityId);

            return parentAccountLevel??1;
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.SubitemsRepository.GetById(Id);
            if (item == null) return Result<SubitemsDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            gbpositoryManager.SubitemsRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<SubitemsDto>.SuccessAsync(_mapper.Map<SubitemsDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<SubitemsDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.SubitemsRepository.GetById(Id);
            if (item == null) return Result<SubitemsDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.UtcNow;
            item.ModifiedBy = (int)session.UserId;
            gbpositoryManager.SubitemsRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<SubitemsDto>.SuccessAsync(_mapper.Map<SubitemsDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<SubitemsDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<SubitemsEditDto>> Update(SubitemsEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<SubitemsEditDto>.FailAsync($"Error in {this.GetType()} : the passed entity IS NULL.");
            var AccountRestrictedtLevel = await sysConfigurationAppHelper.GetValue(260, session.FacilityId);
            int AccountLevel = 0;
            long AccGroupId = 0;
            var AccountLeveldata = await gbpositoryManager.SubitemsRepository.GetOne(s => s.AccountLevel, x => x.AccAccountId == entity.AccAccountParentId && x.FacilityId == session.FacilityId && x.IsDeleted == false);

           
                AccountLevel = AccountLeveldata.Value + 1;
                AccGroupId = await gbpositoryManager.SubitemsRepository.GetAccGroupId(entity.AccAccountParentId ?? 0, session.FacilityId) ?? 0;
            

            if (AccountRestrictedtLevel == "1")
            {
                string subAccountLevel = await sysConfigurationAppHelper.GetValue(99, session.FacilityId);
                var parentAccountLevel = await gbpositoryManager.SubitemsRepository.GetAccountLevel(entity.AccAccountParentId ?? 0, session.FacilityId);
                if (string.IsNullOrEmpty(subAccountLevel))
                {

                    return await Result<SubitemsEditDto>.FailAsync(localization.GetMessagesResource("levelItemsConfiguration"));
                }
                if (parentAccountLevel >= int.Parse(subAccountLevel))
                {
                    return await Result<SubitemsEditDto>.FailAsync(localization.GetMessagesResource("Itemslargerlevel"));


                }
                if (entity.IsSub == false)
                {
                    if (int.Parse(subAccountLevel) != parentAccountLevel + 1)
                    {

                        return await Result<SubitemsEditDto>.FailAsync(localization.GetMessagesResource("Itemslargerlevel"));
                    }
                }
            }
            var item = await gbpositoryManager.SubitemsRepository.GetById(entity.AccAccountId);

            if (item == null) return await Result<SubitemsEditDto>.FailAsync($"--- there is no Data with this id: {entity.AccAccountId}---");
            item.ModifiedOn = DateTime.UtcNow;
            entity.AccountLevel = AccountLevel;
           
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);
            await gbpositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

            gbpositoryManager.SubitemsRepository.Update(item);
            await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
            if (entity.ChildrenExpenses != null)
            {

        
            foreach (var child in entity.ChildrenExpenses)
            {
              

                var item2 = _mapper.Map<BudgAccountExpenses>(child);
                if (item2.Id == 0 && item2.IsDeleted != true)
                {
                    item2.AccAccountId = item.AccAccountId;
                    item2.IsDeleted = false;
                    item2.CreatedOn = DateTime.Now;
                    item2.CreatedBy = (int)session.UserId;

                    if (item2 == null) return await Result<SubitemsEditDto>.FailAsync($"Error in Add of: Budg Account Expenses, the passed entity is NULL !!!.");

                    var newEntity2 = await gbpositoryManager.BudgAccountExpensesRepository.AddAndReturn(item2);
                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }
                  else if (item2.Id != 0)
                     {
                    var item3 = await gbpositoryManager.BudgAccountExpensesRepository.GetById(item2.Id);
                    if (item3 == null) return await Result<SubitemsEditDto>.FailAsync($"--- there is no Data in Budg Account Expenses with this id: {item2.Id}---");

                    _mapper.Map(child, item3);
                    item3.AccAccountId = item.AccAccountId;
                    item3.ModifiedOn = DateTime.UtcNow;
                    item3.ModifiedBy = (int)session.UserId;


                    gbpositoryManager.BudgAccountExpensesRepository.Update(item3);
                    await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                }



            }
            }
            try
            {
                await gbpositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<SubitemsEditDto>.SuccessAsync(_mapper.Map<SubitemsEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<SubitemsEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult> UpdateParentId(long Id, long AccGroupId, CancellationToken cancellationToken = default)
        {
            var item = await gbpositoryManager.SubitemsRepository.GetById(Id);
            if (item == null) return Result<SubitemsDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = false;
            item.IsActive =true;
            item.AccAccountParentId = item.AccAccountId;
            item.AccGroupId = AccGroupId;   
            gbpositoryManager.SubitemsRepository.Update(item);
            try
            {
                await gbpositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<SubitemsDto>.SuccessAsync(_mapper.Map<SubitemsDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<SubitemsDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


    }

}
