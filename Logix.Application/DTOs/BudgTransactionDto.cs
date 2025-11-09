using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logix.Application.Validators;
using System.Security.Cryptography.Xml;
using System.Globalization;
using Logix.Domain.GB;
using Logix.Application.DTOs.OPM;
using Logix.Domain.ACC;

namespace Logix.Application.DTOs.GB
{

    public class BudgTransactionfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
  
        [CustomDisplay("TransactionType", "Acc")]

        public int? DocTypeId { get; set; }

        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();



        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }


        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }


        public int? StatusId { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }


    }

    public class PrintBudgTransactionVM
    {
        public string? FacilityName { get; set; }
        public string? FacilityName2 { get; set; }
        public string? FacilityAddress { get; set; }
        public string? FacilityMobile { get; set; }
        public string? FacilityLogoPrint { get; set; }
        public string? FacilityLogoFooter { get; set; }

        public string? UserName { get; set; }
        public string? ProjectName { get; set; }
        public string? DateGregorian { get; set; }
        public string? DeptName { get; set; }
        public string? ItemsNo { get; set; }
        public string? ItemsName { get; set; }
        public decimal Amount { get; set; }

        public string? AccAccountName { get; set; }
        public string? Code{ get; set; }
        public List<BudgTransactionDetaileDto> BudgeDetails { get; set; }
        public PrintBudgTransactionVM()
        {
            BudgeDetails = new List<BudgTransactionDetaileDto>();
          
        }

    }
    public class BudgTransactionDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }
        

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        
        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }
        [CustomDisplay("Status", "Acc")]
        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }
        [CustomDisplay("AppId", "Acc")]

        public long? AppId { get; set; }
       // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }
       
 

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }

        [CustomDisplay("AccAccountTransFromCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransFromCode { get; set; }
        [CustomDisplay("AccAccountTransFromName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransFromName { get; set; }

        [CustomDisplay("AccAccountTransToCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransToCode { get; set; }
        [CustomDisplay("AccAccountTransToName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransToName { get; set; }

        public bool? IsAdd { get; set; }=false;
        [CustomDisplay("Budgetcategory", "Core")]
        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }


    }

    public class BudgTransactionTransferVM
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [StringLength(10)]
        public string? DateGregorian { get; set; }

        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }
        public int? DocTypeId { get; set; }
        public string? UserFullname { get; set; }
        public decimal? sumDebit { get; set; }
        public decimal? sumCredit { get; set; }
        public bool IsSelected { get; set; }

        public List<BudgTransactionDetailesVw> Children { get; set; }
    }
    
    #region ============================== Links 

    public class BudgTransactionLinksDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }
        [CustomRequired]
        [CustomDisplay("LinkInitialNO", "Core")]
        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        [CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
       
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCodeS { get; set; }
        public string? CustomerNameS { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountTypeS { get; set; }

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        public int? DeptIDe { get; set; }
        [CustomDisplay("Type", "Core")]

        public long? TypeId { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }
        public long? BudgTypeId { get; set; }

    }
    public class BudgTransactionLinksFinalfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]




        [StringLength(255)]
        public string? AccAccountName { get; set; }
        public int? DocTypeId { get; set; }
        public bool? IsDeleted { get; set; }


        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }

        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
        public string? CustomerCodeS { get; set; }
        public string? CustomerNameS { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public int? AccAccountTypeS { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
    }
    public class BudgTransactionLinksEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Type", "Core")]

        public long? TypeId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }
        public long? BudgTypeId { get; set; }


    }

    public class BudgTransactionLinksVM
    {
        public BudgTransactionLinksVM()
        {
            Children = new List<BudgTransactionDetaileLinksDto>();
            BudgTransactionLinksDto = new BudgTransactionLinksDto();


        }
        public BudgTransactionLinksDto? BudgTransactionLinksDto { get; set; }

        public BudgTransactionDetaileLinksDto BudgTransactionDetaileDto { get; set; }
        public BudgTransactionDetaileLinksDto? BudgTransactionDetaileDto2 { get; set; }



        public List<BudgTransactionDetaileLinksDto> Children { get; set; }
        public bool AllowAmountTotal { get; set; }
        public bool AllowAmountDiscounts { get; set; }
        public bool AllowAmountLinks { get; set; }
        public bool AllowAmountReinforcements { get; set; }
        public bool AllowAmountTransfersTO { get; set; }
        public bool AllowAmountTransfersFrom { get; set; }

        [CustomDisplay("Amount", "Core")]
        public decimal Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }
    }
    public class BudgTransactionLinksEditVM
    {
        public BudgTransactionLinksEditVM()
        {
            Children = new List<BudgTransactionDetaileLinksEditDto>();
        }
        public BudgTransactionDetaileLinksEditDto BudgTransactionDetaileEditDto { get; set; }
        public BudgTransactionDetaileLinksEditDto? BudgTransactionDetaileEditDto2 { get; set; }
        public List<BudgTransactionDetaileLinksEditDto> Children { get; set; }
        public BudgTransactionLinksEditDto BudgTransactionLinksEditDto { get; set; }
        public bool AllowAmountTotal { get; set; }
        public bool AllowAmountDiscounts { get; set; }
        public bool AllowAmountLinks { get; set; }
        public bool AllowAmountReinforcements { get; set; }
        public bool AllowAmountTransfersTO { get; set; }
        public bool AllowAmountTransfersFrom { get; set; }

    }
    #endregion  ============================== Links 

    public class BudgTransactionEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
       

       


        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }

    

        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
    

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }
    
        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }
      
        [StringLength(50)]
    
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
    
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }

    
        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }

  
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker","Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
    }
    public class BudgTransactionVM2
    {
        public BudgTransactionVM2()
        {
            BudgTransactionDto = new BudgTransactionDto();
            Children2 = new List<BudgTransactionDetailesVw>();


        }
        public BudgTransactionDto BudgTransactionDto { get; set; }
       
        public List<BudgTransactionDetailesVw> Children2 { get; set; }

    }
    public class BudgTransactionVM
    {
        public BudgTransactionVM()
        {
            Children = new List<BudgTransactionDetaileDto>();
            BudgTransactionDto=new BudgTransactionDto();
            Children2 = new List<BudgTransactionDetailesVw>();
            Childrenyear = new List<BudgTransactionDetaileYearDto>();


        }
        public BudgTransactionDto BudgTransactionDto { get; set; }

        public BudgTransactionDetaileDto BudgTransactionDetaileDto { get; set; }
        public BudgTransactionDetaileDto? BudgTransactionDetaileDto2 { get; set; }


        public List<BudgTransactionDetaileDto> Children { get; set; }
        public List<BudgTransactionDetailesVw> Children2 { get; set; }
        public List<BudgTransactionDetaileYearDto> Childrenyear { get; set; }

    }

    public class BudgTransactionEditVM
    {
        public BudgTransactionEditVM()
        {
            Children = new List<BudgTransactionDetaileEditDto>();
        }
        public BudgTransactionEditDto BudgTransactionEditDto { get; set; }
        public BudgTransactionDetaileEditDto BudgTransactionDetaileEditDto { get; set; }
        public BudgTransactionDetaileEditDto? BudgTransactionDetaileEditDto2 { get; set; }
        public List<BudgTransactionDetaileEditDto> Children { get; set; }


    }
    public class BudgItemTransactionsVM
    {
        public BudgItemTransactionsVM()
        {
            Children = new List<BudgTransactionDetaileDto>();
            BudgTransactionDto = new BudgTransactionDto();


        }
        public BudgTransactionDto BudgTransactionDto { get; set; }
        public BudgTransactionDetaileDto BudgTransactionDetaileDto { get; set; }


        public List<BudgTransactionDetaileDto> Children { get; set; }

      
    }

    #region =====================================InitialCredits
    public class BudgTransactionInitialCreditsDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }
        [CustomDisplay("AppId", "Acc")]

        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }

        [CustomDisplay("AccAccountTransFromCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransFromCode { get; set; }
        [CustomDisplay("AccAccountTransFromName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransFromName { get; set; }

        [CustomDisplay("AccAccountTransToCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransToCode { get; set; }
        [CustomDisplay("AccAccountTransToName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransToName { get; set; }

        public bool? IsAdd { get; set; } = false;
        [CustomDisplay("Budgetcategory", "Core")]
        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }


    }
    public class BudgTransactionInitialCreditsEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
    }

    public class BudgTransactionInitialCreditsfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]




        [StringLength(255)]
        public string? AccAccountName { get; set; }
        public int? DocTypeId { get; set; }
        public bool? IsDeleted { get; set; }

        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }

        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
    }
    public class BudgTransactionInitialCreditsVM
    {
        public BudgTransactionInitialCreditsVM()
        {
            Children = new List<BudgTransactionDetaileDto>();
            BudgTransactionDto = new BudgTransactionInitialCreditsDto();


        }
        public BudgTransactionInitialCreditsDto BudgTransactionDto { get; set; }
        public List<BudgTransactionDetaileDto> Children { get; set; }


    }
    public class BudgTransactionInitialCreditsEditVM
    {
        public BudgTransactionInitialCreditsEditVM()
        {
            Children = new List<BudgTransactionDetaileEditDto>();
        }
        public BudgTransactionInitialCreditsEditDto BudgTransactionEditDto { get; set; }
        public List<BudgTransactionDetaileEditDto> Children { get; set; }


    }
    #endregion ====================================InitialCredits

    #region =====================================InitialYear
    public class BudgTransactionInitialYearDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }
        [CustomDisplay("AppId", "Acc")]

        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }

        [CustomDisplay("AccAccountTransFromCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransFromCode { get; set; }
        [CustomDisplay("AccAccountTransFromName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransFromName { get; set; }

        [CustomDisplay("AccAccountTransToCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransToCode { get; set; }
        [CustomDisplay("AccAccountTransToName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransToName { get; set; }

        public bool? IsAdd { get; set; } = false;
        [CustomDisplay("Budgetcategory", "Core")]
        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }


    }
    public class BudgTransactionInitialYearEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
    }

    public class BudgTransactionInitialYearfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]




        [StringLength(255)]
        public string? AccAccountName { get; set; }
        public int? DocTypeId { get; set; }
        public bool? IsDeleted { get; set; }

        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }

        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
    }
    public class BudgTransactionInitialYearVM
    {
        public BudgTransactionInitialYearVM()
        {
            Children = new List<BudgTransactionDetaileDto>();
            BudgTransactionDto = new BudgTransactionInitialYearDto();
            Childrenyear = new List<BudgTransactionDetaileYearDto>();

        }
        public BudgTransactionInitialYearDto BudgTransactionDto { get; set; }
        public List<BudgTransactionDetaileDto> Children { get; set; }
        public List<BudgTransactionDetaileYearDto> Childrenyear { get; set; }



    }

    #endregion ====================================InitialCredits
    #region============================ Reinforcements
    public class BudgTransactionReinforcementsDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }
        [CustomDisplay("AppId", "Acc")]

        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }

        [CustomDisplay("AccAccountTransFromCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransFromCode { get; set; }
        [CustomDisplay("AccAccountTransFromName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransFromName { get; set; }

        [CustomDisplay("AccAccountTransToCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransToCode { get; set; }
        [CustomDisplay("AccAccountTransToName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransToName { get; set; }

        public bool? IsAdd { get; set; } = false;
        [CustomDisplay("Budgetcategory", "Core")]
        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
    


    }
    public class BudgTransactionReinforcementsEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
    }

    public class BudgTransactionReinforcementsVM
    {
        public BudgTransactionReinforcementsVM()
        {
            Children = new List<BudgTransactionDetaileDto>();
            BudgTransactionDto = new BudgTransactionReinforcementsDto();
            Children2 = new List<BudgTransactionDetailesVw>();


        }
        public BudgTransactionReinforcementsDto BudgTransactionDto { get; set; }



        public List<BudgTransactionDetaileDto> Children { get; set; }
        public List<BudgTransactionDetailesVw> Children2 { get; set; }

    }
    public class BudgTransactionReinforcementsfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]




        [StringLength(255)]
        public string? AccAccountName { get; set; }
        public int? DocTypeId { get; set; }
        public bool? IsDeleted { get; set; }


        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }

        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
    }


    public class BudgTransactionReinforcementsEditVM
    {
        public BudgTransactionReinforcementsEditVM()
        {
            Children = new List<BudgTransactionDetaileEditDto>();
        }

        public BudgTransactionReinforcementsEditDto BudgTransactionEditDto { get; set; }

        public List<BudgTransactionDetaileEditDto> Children { get; set; }


    }

    #endregion =================================Reinforcements

    #region ============================== Links Initial
    public class BudgTransactionLinksInitialDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }
        [CustomDisplay("LinkInitialNO", "Core")]

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        public long? TypeId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }
        public long? BudgTypeId { get; set; }

    }
    public class BudgTransactionLinksInitialEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }

        public long? BudgTypeId { get; set; }

    }
    public class BudgTransactionLinksInitialVM
    {
        public BudgTransactionLinksInitialVM()
        {
            BudgTransactionDto = new BudgTransactionLinksInitialDto();
            Children = new List<BudgTransactionDetaileLinksInitialDto>();


        }
        public BudgTransactionLinksInitialDto? BudgTransactionDto { get; set; }
        public List<BudgTransactionDetaileLinksInitialDto> Children { get; set; }

        public BudgTransactionDetaileLinksInitialDto BudgTransactionDetaileDto { get; set; }
        public bool AllowAmountTotal { get; set; }
        public bool AllowAmountDiscounts { get; set; }
        public bool AllowAmountLinks { get; set; }
        public bool AllowAmountReinforcements { get; set; }
        public bool AllowAmountTransfersTO { get; set; }
        public bool AllowAmountTransfersFrom { get; set; }

        [CustomDisplay("Amount", "Core")]
        public decimal Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal AmountTotal { get; set; }
        public decimal AmountTransfers { get; set; }
      


    }
    public class BudgTransactionLinksInitialEditVM
    {

        public BudgTransactionLinksInitialEditVM()
        {
            BudgTransactionEditDto = new BudgTransactionLinksInitialEditDto();
            Children = new List<BudgTransactionDetaileLinksInitialEditDto>();
        }
        public BudgTransactionLinksInitialEditDto? BudgTransactionEditDto { get; set; }
        public List<BudgTransactionDetaileLinksInitialEditDto> Children { get; set; }

        public BudgTransactionDetaileLinksInitialEditDto BudgTransactionDetaileEditDto { get; set; }

        public bool AllowAmountTotal { get; set; }
        public bool AllowAmountDiscounts { get; set; }
        public bool AllowAmountLinks { get; set; }
        public bool AllowAmountReinforcements { get; set; }
        public bool AllowAmountTransfersTO { get; set; }
        public bool AllowAmountTransfersFrom { get; set; }

        [CustomDisplay("Amount", "Core")]
        public decimal Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal Credit { get; set; }
        public decimal AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal AmountTotal { get; set; }
        public decimal AmountTransfers { get; set; }

    }
    public class BudgTransactionLinksInitialfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
   
 


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        public int? DocTypeId { get; set; }
        public bool? IsDeleted { get; set; }

   
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }

        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
    }
    #endregion  ============================== Links Initial

    #region ===================================== Discounts

    public class BudgTransactionDiscountsVM
    {
        public BudgTransactionDiscountsVM()
        {
            BudgTransactionDto = new BudgTransactionDiscountsDto();
            Children = new List<BudgTransactionDetaileDiscountsDto>();


        }
        public BudgTransactionDiscountsDto? BudgTransactionDto { get; set; }
        public List<BudgTransactionDetaileDiscountsDto> Children { get; set; }

        public BudgTransactionDetaileDiscountsDto BudgTransactionDetaileDto { get; set; }
   

   

    }
    public class BudgTransactionDiscountsEditVM
    {
        public BudgTransactionDiscountsEditVM()
        {
            BudgTransactionEditDto = new BudgTransactionDiscountsEditDto();

            Children = new List<BudgTransactionDetaileDiscountsEditDto>();
        }
     

        public BudgTransactionDiscountsEditDto? BudgTransactionEditDto { get; set; }
        public List<BudgTransactionDetaileDiscountsEditDto> Children { get; set; }

        public BudgTransactionDetaileDiscountsEditDto BudgTransactionDetaileEditDto { get; set; }
    }
    public class BudgTransactionDiscountsDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }

        public long? BudgTypeId { get; set; }


    }

    public class BudgTransactionDiscountsEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        public long? BudgTypeId { get; set; }

    }
    public class BudgTransactionDiscountsfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]




        [StringLength(255)]
        public string? AccAccountName { get; set; }
        public int? DocTypeId { get; set; }
        public bool? IsDeleted { get; set; }


        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }

        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
    }

    #endregion ===================================== Discounts

    #region ============================== Links Initial multiple
    public class BudgTransactionLinksInitialmultipleVM
    {
        public BudgTransactionLinksInitialmultipleVM()
        {
            BudgTransactionDto = new BudgTransactionLinksInitialmultipleDto();
            Children = new List<BudgTransactionDetaileLinksMultipleDto>();


        }
        public BudgTransactionLinksInitialmultipleDto? BudgTransactionDto { get; set; }
        public List<BudgTransactionDetaileLinksMultipleDto> Children { get; set; }





    }
    public class BudgTransactionLinksInitialmultipleEditVM
    {
        public BudgTransactionLinksInitialmultipleEditVM()
        {
            BudgTransactionEditDto = new BudgTransactionLinksInitialmultipleEditDto();
            Children = new List<BudgTransactionDetaileLinksMultipleDto>();


        }
        public BudgTransactionLinksInitialmultipleEditDto? BudgTransactionEditDto { get; set; }
        public List<BudgTransactionDetaileLinksMultipleDto> Children { get; set; }





    }
    public class BudgTransactionLinksInitialmultipleDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }
        [CustomDisplay("LinkInitialNO", "Core")]

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Type", "Core")]
        public long? TypeId { get; set; }

        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }

    }
    public class BudgTransactionLinksInitialmultiplfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]




        [StringLength(255)]
        public string? AccAccountName { get; set; }
        public int? DocTypeId { get; set; }
        public bool? IsDeleted { get; set; }


        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }

        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
        [CustomDisplay("AccAccountTransFromCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransFromCode { get; set; }
        [CustomDisplay("AccAccountTransFromName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransFromName { get; set; }

        [CustomDisplay("AccAccountTransToCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransToCode { get; set; }
        [CustomDisplay("AccAccountTransToName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransToName { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }
    }

    public class BudgTransactionLinksInitialmultipleEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }


    }
    #endregion  ============================== Links Initial multiple

    #region ============================== Links Final multiple

    public class BudgTransactionlinkFinalMultiDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }
        [CustomDisplay("LinkInitialNO", "Core")]

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }



    }

    public class BudgTransactionlinkFinalMultiEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
    }

    public class BudgTransactionlinkFinalMultiVM
    {
        public BudgTransactionlinkFinalMultiVM()
        {
            BudgTransactionDto = new BudgTransactionlinkFinalMultiDto();
            Children = new List<BudgTransactionDetailelinkFinalMultiDto>();


        }
        public BudgTransactionlinkFinalMultiDto? BudgTransactionDto { get; set; }
        public List<BudgTransactionDetailelinkFinalMultiDto> Children { get; set; }





    }
    public class BudgTransactionlinkFinalMultieEditVM
    {
        public BudgTransactionlinkFinalMultieEditVM()
        {
            BudgTransactionEditDto = new BudgTransactionlinkFinalMultiEditDto();
            Children = new List<BudgTransactionDetailelinkFinalMultiDto>();


        }
        public BudgTransactionlinkFinalMultiEditDto? BudgTransactionEditDto { get; set; }
        public List<BudgTransactionDetailelinkFinalMultiDto> Children { get; set; }





    }

    #endregion  ============================== Links Final multiple


    #region ============================== Costsitems 
    public class BudgTransactionCostsitemsDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }
        //[CustomRequired]
        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }
        [CustomDisplay("AppId", "Acc")]

        public long? AppId { get; set; }
        // [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }

        [CustomDisplay("AccAccountTransFromCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransFromCode { get; set; }
        [CustomDisplay("AccAccountTransFromName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransFromName { get; set; }

        [CustomDisplay("AccAccountTransToCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransToCode { get; set; }
        [CustomDisplay("AccAccountTransToName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransToName { get; set; }

        public bool? IsAdd { get; set; } = false;
        [CustomDisplay("Budgetcategory", "Core")]
        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }



    }

    public class BudgTransactionCostsitemsVM
    {
        public BudgTransactionCostsitemsVM()
        {
            Children = new List<BudgTransactionDetaileDto>();
            BudgTransactionDto = new BudgTransactionCostsitemsDto();


        }
        public BudgTransactionCostsitemsDto BudgTransactionDto { get; set; }

        public BudgTransactionDetaileDto BudgTransactionDetaileDto { get; set; }


        public List<BudgTransactionDetaileDto> Children { get; set; }

    }


    public class BudgTransactionCostsitemsEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }

    }
    public class BudgTransactionCostsitemsEditVM
    {
        public BudgTransactionCostsitemsEditVM()
        {
            Children = new List<BudgTransactionDetaileEditDto>();
        }
        public BudgTransactionCostsitemsEditDto BudgTransactionEditDto { get; set; }
        public List<BudgTransactionDetaileEditDto> Children { get; set; }


    }

    #endregion ============================== Costsitems 

    #region ===================================== Transfers

    public class BudgTransactionTransfersVM
    {
        public BudgTransactionTransfersVM()
        {
            Children = new List<BudgTransactionDetaileDto>();
            BudgTransactionDto = new BudgTransactionTransfersDto();
            Children2 = new List<BudgTransactionDetailesVw>();


        }
        public BudgTransactionTransfersDto BudgTransactionDto { get; set; }

        public BudgTransactionDetaileDto BudgTransactionDetaileDto { get; set; }
        public BudgTransactionDetaileDto? BudgTransactionDetaileDto2 { get; set; }


        public List<BudgTransactionDetaileDto> Children { get; set; }
        public List<BudgTransactionDetailesVw> Children2 { get; set; }

    }
    public class BudgTransactionTransfersEditVM
    {
     
        public BudgTransactionTransfersEditVM()
        {
            Children = new List<BudgTransactionDetaileEditDto>();
        }
        public BudgTransactionTransfersEditDto BudgTransactionEditDto { get; set; }
        public BudgTransactionDetaileEditDto BudgTransactionDetaileEditDto { get; set; }
        public BudgTransactionDetaileEditDto? BudgTransactionDetaileEditDto2 { get; set; }
        public List<BudgTransactionDetaileEditDto> Children { get; set; }
    }
    public class BudgTransactionTransfersDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        public string? encId { get; set; }


        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("AccountingPeriod", "Acc")]
        public long? PeriodId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }


        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }



        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]

        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }
      
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }

        [CustomDisplay("ExchangeRate", "Acc")]
        public decimal? ExchangeRate { get; set; }

        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        [RequiredDDL(0)]
        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }

        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
     
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]
        public long AccAccountId { get; set; }
  
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
      

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }



        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        public string? AttachFile { get; set; }

        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        //[RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }


    }

    public class BudgTransactionTransfersEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }

        public string? DateHijri { get; set; }
        [CustomRequired]
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public string? TTime { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }





        [CustomDisplay("branch", "Common")]
        public long? CcId { get; set; }

        public int? DocTypeId { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }

        public int? ModifiedBy { get; set; }



        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? StatusId { get; set; }

        public int? PaymentTypeId { get; set; }

        public string? ChequNo { get; set; }

        public string? ChequDateHijri { get; set; }

        public long? BankId { get; set; }

        public string? Bian { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("AmountCertified", "Acc")]
        public decimal? Amount { get; set; }

        public string? AmountWrite { get; set; }

        public long? ReferenceNo { get; set; }

        public long? CollectionEmpId { get; set; }


        public string? ReferenceCode { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public long? ProjectId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Entitybeneficiary", "Core")]

        public int? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public long? CustomerId { get; set; }

        public long? AppId { get; set; }
        public long AccAccountId { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Note", "Acc")]
        public string? Note { get; set; }

        [StringLength(50)]

        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("JE_No", "Acc")]
        public string? Code2 { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? JENO { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        public string? AttachFile { get; set; }


        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Undertaker", "Core")]
        public int AccAccountType { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Projects", "Acc")]
        public string? ProjectName { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptID { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
    }
    public class BudgTransactionTransfersfilterDto
    {
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("Undertaker", "Core")]




        [StringLength(255)]
        public string? AccAccountName { get; set; }
        public int? DocTypeId { get; set; }
        public bool? IsDeleted { get; set; }


        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("Entitybeneficiary", "Core")]
        public long? DeptIDS { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }
        [CustomDisplay("AppId", "Acc")]
        public long? AppId { get; set; }

        public int? CategoryId { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? BudgTypeId { get; set; }
        [CustomDisplay("DocDate", "Acc")]

        public string? DateGregorian { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }
        [CustomDisplay("AccAccountTransFromCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransFromCode { get; set; }
        [CustomDisplay("AccAccountTransFromName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransFromName { get; set; }

        [CustomDisplay("AccAccountTransToCode", "Acc")]
        [StringLength(50)]
        public string? AccAccountTransToCode { get; set; }
        [CustomDisplay("AccAccountTransToName", "Acc")]
        [StringLength(255)]
        public string? AccAccountTransToName { get; set; }
        [CustomDisplay("TransfersType", "Core")]
        public int TransfersTypeId { get; set; }
    }

    #endregion ===================================== Transfers

}
