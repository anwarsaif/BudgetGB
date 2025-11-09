using AutoMapper;
using iText.Commons.Bouncycastle.Asn1.X509;
using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.GB;
using Logix.Application.DTOs.RPT;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.GB;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.GB;
using Logix.Domain.Main;
using Logix.Domain.RPT;
using Org.BouncyCastle.Math.EC.Rfc7748;
using static QuestPDF.Helpers.Colors;

namespace Logix.Application.Services.GB
{
    public class GBDashboardService:IGBDashboardService
    {
        private readonly IGbRepositoryManager gbpositoryManager;
        private readonly IMapper mapper;
        private readonly ISessionHelper session;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly IRptRepositoryManager rptRepositoryManager;
       

        public GBDashboardService(IAccRepositoryManager accRepositoryManager, IRptRepositoryManager rptRepositoryManager, IMapper mapper, ISessionHelper session, IGbRepositoryManager gbpositoryManager)
        {
            this.accRepositoryManager = accRepositoryManager;
            this.rptRepositoryManager = rptRepositoryManager;
            this.gbpositoryManager = gbpositoryManager;
            this.mapper = mapper;
            this.session = session;
        }



        //public async Task<IResult<IEnumerable<GbStatisticsDto>>> GetGBStatistics()
        //{

        //    try
        //    {
        //        var docTypes = await gbpositoryManager.BudgDocTypeRepository.GetAll(x=>x.FlagDelete==false && x.DocTypeId !=6 && x.DocTypeId!=9 && x.DocTypeId != 4 && x.DocTypeId != 2 && x.DocTypeId != 3);
        //        var docTypeDtos = new List<GbStatisticsDto>();

        //        Random random = new Random();
        //        decimal TotalCreditSum = 0.0m;
        //        decimal TotalDebitSum = 0.0m;

        //        string[] colors = {"green", "red", "yellow", "purple", "blue", "orange" };
        //        foreach (var docType in docTypes)
        //        {
        //            var transactions = await gbpositoryManager.BudgTransactionDetaileRepository
        //                .GetAllVW(x => x.IsDeleted == false && x.DocTypeId == docType.DocTypeId && x.StatusId == 2 && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);
        //            decimal creditSum = 0.0m;
        //            if (docType.DocTypeId==1 || docType.DocTypeId == 3 || docType.DocTypeId == 7)
        //            {
        //                decimal value = 0;
        //                foreach (var transaction in transactions)
        //                {
        //                     value = 0;
        //                    var natureAccount = await accRepositoryManager.AccGroupRepository.GetOne(x => x.NatureAccount, s => s.AccGroupId == transaction.AccGroupId);

        //                    if (natureAccount != null && natureAccount == 1)
        //                    {
        //                        value += transaction.Credit;
        //                    }
        //                    else if (natureAccount != null && natureAccount == -1)
        //                    {
        //                        value + == transaction.Debit;
        //                    }


        //                }

        //                TotalCreditSum += value;
        //            }

        //            else if (docType.DocTypeId == 2 || docType.DocTypeId == 4 || docType.DocTypeId == 5)
        //            {
        //                creditSum = transactions.Sum(x => x.Debit);
        //                TotalDebitSum += creditSum;
        //            }
        //            else if (docType.DocTypeId == 8)
        //            {
        //                var transactionsLin = await gbpositoryManager.BudgTransactionDetaileRepository
        //              .GetAllVW(x => x.IsDeleted == false && x.DocTypeId == docType.DocTypeId && x.StatusId ==1 && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);

        //                creditSum = transactionsLin.Sum(x => x.Debit);
        //                TotalDebitSum += creditSum;

        //            }




        //                int randomIndex = random.Next(colors.Length);
        //            string randomColor = colors[randomIndex];

        //            var dto = new GbStatisticsDto
        //            {
        //                DocTypeId = docType.DocTypeId,
        //                DocTypeName = "إجمالي" + " "+ docType.DocTypeName,
        //                DocTypeName2 = "Total" + " " + docType.DocTypeName2,
        //                Color = randomColor,
        //                Icon = "",
        //                CreditSum = (int)creditSum
        //            };

        //            docTypeDtos.Add(dto);



        //        }

        //        //var dtT = new GbStatisticsDto
        //        //{
        //        //    DocTypeId = 0,
        //        //    DocTypeName = "الاجمالي",
        //        //    DocTypeName2 = "Total",
        //        //    Color = "yellow",
        //        //    Icon = "",
        //        //    CreditSum = (int)((TotalCreditSum - TotalDebitSum))            
        //        //    };

        //        //docTypeDtos.Add(dtT);

        //        var BudgExpenses=await gbpositoryManager.BudgExpensesLinksRepository.GetAllVW(x=>x.IsDeleted==false && x.Finyear==session.FinYear && x.FacilityId==session.FacilityId);

        //        if(BudgExpenses != null)
        //        {
        //            var dto = new GbStatisticsDto
        //            {
        //                DocTypeId = 6,
        //                DocTypeName = " إجمالي المنصرف من البنود",
        //                DocTypeName2 = " Total Expenses",
        //                Color = "green",
        //                Icon = "",
        //                CreditSum = (int)BudgExpenses.Sum(x => x.Amount),
        //            };
        //            docTypeDtos.Add(dto);



        //        }


        //        return Result<IEnumerable<GbStatisticsDto>>.Success(docTypeDtos, "Success", 200);
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Result<IEnumerable<GbStatisticsDto>>.FailAsync($"Exp in acc GetStatistics: {ex.Message}");
        //    }
        //}
        public async Task<IResult<IEnumerable<GbStatisticsDto>>> GetGBStatistics()
        {
            try
            {
                var docTypes = await gbpositoryManager.BudgDocTypeRepository.GetAll(x => x.FlagDelete == false && x.DocTypeId != 6 && x.DocTypeId != 9 && x.DocTypeId != 4 && x.DocTypeId != 2 && x.DocTypeId != 3);
                var docTypeDtos = new List<GbStatisticsDto>();

                Random random = new Random();
                decimal TotalCreditSum = 0.0m;
                decimal TotalDebitSum = 0.0m;

                string[] colors = { "green", "red", "yellow", "purple", "blue", "orange" };
                foreach (var docType in docTypes)
                {
                    var CanceledItems = await gbpositoryManager.BudgTransactionDetaileRepository.GetAllVW(a => a.DocTypeId == 9&&a.IsDeleted==false);
                    var transactions = await gbpositoryManager.BudgTransactionDetaileRepository
                        .GetAllVW(x => x.IsDeleted == false && x.DocTypeId == docType.DocTypeId && x.StatusId == 2 && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);
                    decimal creditSum = 0.0m;
                    if (docType.DocTypeId == 1 || docType.DocTypeId == 3 || docType.DocTypeId == 7)
                    {
                        decimal value = 0;
                        foreach (var transaction in transactions)
                        {
                            var natureAccount = await accRepositoryManager.AccGroupRepository.GetOne(x => x.NatureAccount, s => s.AccGroupId == transaction.AccGroupId);

                            if (natureAccount != null && natureAccount== 1)
                            {
                                value += transaction.Credit;
                            }
                            else if (natureAccount != null && natureAccount== -1)
                            {
                                value += transaction.Debit;
                            }
                        } 
                        creditSum += value;
                        TotalDebitSum += creditSum;

                    }
                    else if (docType.DocTypeId == 2 || docType.DocTypeId == 4 || docType.DocTypeId == 5)
                    {
                        if (docType.DocTypeId == 5)
                        {
                            var transactionsLin = await gbpositoryManager.BudgTransactionDetaileRepository
                      .GetAllVW(x => x.IsDeleted == false && x.DocTypeId == 5 && !CanceledItems.Select(y => y.MReferenceNo).Contains(x.MReferenceNo) && x.StatusId == 2 && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);
                            creditSum = transactionsLin.Sum(x => x.Debit);
                            TotalDebitSum += creditSum;
                        }
                        else
                        {
                            creditSum = transactions.Sum(x => x.Debit);
                            TotalDebitSum += creditSum;
                        }
                    }
                    else if (docType.DocTypeId == 8)
                    {
                        var transactionsLin = await gbpositoryManager.BudgTransactionDetaileRepository
                      .GetAllVW(x => x.IsDeleted == false&& !CanceledItems.Select(y => y.MReferenceNo).Contains(x.TId) && x.DocTypeId == docType.DocTypeId && x.StatusId == 2 && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);

                        creditSum = transactionsLin.Sum(x => x.Debit);
                        TotalDebitSum += creditSum;

                    }

                    int randomIndex = random.Next(colors.Length);
                    string randomColor = colors[randomIndex];

                    var dto = new GbStatisticsDto
                    {
                        DocTypeId = docType.DocTypeId,
                        DocTypeName = "إجمالي" + " " + docType.DocTypeName,
                        DocTypeName2 = "Total" + " " + docType.DocTypeName2,
                        Color = randomColor,
                        Icon = "",
                        CreditSum = (int)creditSum
                    };

                    docTypeDtos.Add(dto);
                }

                var BudgExpenses = await gbpositoryManager.BudgExpensesLinksRepository.GetAllVW(x => x.IsDeleted == false && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);

                if (BudgExpenses != null)
                {
                    var dto = new GbStatisticsDto
                    {
                        DocTypeId = 6,
                        DocTypeName = " إجمالي المنصرف من البنود",
                        DocTypeName2 = " Total Expenses",
                        Color = "green",
                        Icon = "",
                        CreditSum = (int)BudgExpenses.Sum(x => x.Amount),
                    };
                    docTypeDtos.Add(dto);
                }

                return Result<IEnumerable<GbStatisticsDto>>.Success(docTypeDtos, "Success", 200);
            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<GbStatisticsDto>>.FailAsync($"Exp in acc GetStatistics: {ex.Message}");
            }
        }
        //public async Task<IResult<IEnumerable<GbStatisticsDto>>> GetGBStatistics()
        //{

        //    try
        //    {
        //        var docTypes = await gbpositoryManager.BudgDocTypeRepository.GetAll(x => x.FlagDelete == false && x.DocTypeId != 6 && x.DocTypeId != 9 && x.DocTypeId != 4 && x.DocTypeId != 2 && x.DocTypeId != 3);
        //        var docTypeDtos = new List<GbStatisticsDto>();

        //        Random random = new Random();
        //        decimal TotalCreditSum = 0.0m;
        //        decimal TotalDebitSum = 0.0m;

        //        string[] colors = { "green", "red", "yellow", "purple", "blue", "orange" };
        //        foreach (var docType in docTypes)
        //        {
        //            var transactions = await gbpositoryManager.BudgTransactionDetaileRepository
        //                .GetAllVW(x => x.IsDeleted == false && x.DocTypeId == docType.DocTypeId && x.StatusId == 2 && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);
        //            decimal creditSum = 0.0m;
        //            if (docType.DocTypeId == 1 || docType.DocTypeId == 3 || docType.DocTypeId == 7)
        //            {
        //                creditSum = transactions.Sum(x => x.Credit);
        //                TotalCreditSum += creditSum;
        //            }

        //            else if (docType.DocTypeId == 2 || docType.DocTypeId == 4 || docType.DocTypeId == 5)
        //            {
        //                creditSum = transactions.Sum(x => x.Debit);
        //                TotalDebitSum += creditSum;
        //            }
        //            else if (docType.DocTypeId == 8)
        //            {
        //                var transactionsLin = await gbpositoryManager.BudgTransactionDetaileRepository
        //              .GetAllVW(x => x.IsDeleted == false && x.DocTypeId == docType.DocTypeId && x.StatusId == 1 && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);

        //                creditSum = transactionsLin.Sum(x => x.Debit);
        //                TotalDebitSum += creditSum;

        //            }




        //            int randomIndex = random.Next(colors.Length);
        //            string randomColor = colors[randomIndex];

        //            var dto = new GbStatisticsDto
        //            {
        //                DocTypeId = docType.DocTypeId,
        //                DocTypeName = "إجمالي" + " " + docType.DocTypeName,
        //                DocTypeName2 = "Total" + " " + docType.DocTypeName2,
        //                Color = randomColor,
        //                Icon = "",
        //                CreditSum = (int)creditSum
        //            };

        //            docTypeDtos.Add(dto);



        //        }

        //        //var dtT = new GbStatisticsDto
        //        //{
        //        //    DocTypeId = 0,
        //        //    DocTypeName = "الاجمالي",
        //        //    DocTypeName2 = "Total",
        //        //    Color = "yellow",
        //        //    Icon = "",
        //        //    CreditSum = (int)((TotalCreditSum - TotalDebitSum))            
        //        //    };

        //        //docTypeDtos.Add(dtT);

        //        var BudgExpenses = await gbpositoryManager.BudgExpensesLinksRepository.GetAllVW(x => x.IsDeleted == false && x.Finyear == session.FinYear && x.FacilityId == session.FacilityId);

        //        if (BudgExpenses != null)
        //        {
        //            var dto = new GbStatisticsDto
        //            {
        //                DocTypeId = 6,
        //                DocTypeName = " إجمالي المنصرف من البنود",
        //                DocTypeName2 = " Total Expenses",
        //                Color = "green",
        //                Icon = "",
        //                CreditSum = (int)BudgExpenses.Sum(x => x.Amount),
        //            };
        //            docTypeDtos.Add(dto);



        //        }


        //        return Result<IEnumerable<GbStatisticsDto>>.Success(docTypeDtos, "Success", 200);
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Result<IEnumerable<GbStatisticsDto>>.FailAsync($"Exp in acc GetStatistics: {ex.Message}");
        //    }
        //}

        public async Task<IResult<IEnumerable<RptReportDto>>> GetReports(long systemId, string groupId)
        {
            if (systemId == 0 || string.IsNullOrEmpty(groupId)) return await Result<IEnumerable<RptReportDto>>.FailAsync("خطأ, يرجى ادخال البيانات المطلوبة");
            try
            {
                IEnumerable<RptReport> reportsList = new List<RptReport>();
                var reports = await rptRepositoryManager.RptReportRepository.GetAll(d => d.SystemId == systemId && d.IsDeleted == false);
                if (reports != null)
                {
                    reportsList = reports.ToList().Where(r => r.SysGroupId != null && r.SysGroupId.Split(",").Contains(groupId)).ToList();

                }
                return await Result<IEnumerable<RptReportDto>>.SuccessAsync(mapper.Map<List<RptReportDto>>(reportsList), "");

            }
            catch (Exception ex)
            {
                return await Result<IEnumerable<RptReportDto>>.FailAsync($"Exp in acc GetStatistics: {ex.Message}");
            }
        }

        public int CheckPage(long screenId)
        {
            try
            {
                if (screenId == 70)
                {
                    long facilityId = session.GetData<SysUser>("user").FacilityId ?? 0;
                    long finYearId = session.GetData<long>("finYear");
                    var count = accRepositoryManager.AccJournalMasterRepository.GetCount(j => j.FlagDelete == false && j.FacilityId == facilityId && j.FinYear == finYearId && j.StatusId == 1);
                    return count;
                }

                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<IResult<IEnumerable<BudgTransactionVw>>> GetAllBudgTransactionVw(CancellationToken cancellationToken = default)
        {
            try
            {
                var res = await rptRepositoryManager.RptReportRepository.GetAllBudgTransactionVw();
                return await Result<IEnumerable<BudgTransactionVw>>.SuccessAsync(res);
            }
            catch (Exception exp)
            {
                return await Result<IEnumerable<BudgTransactionVw>>.FailAsync(exp.Message);
            }
        }

    }

}