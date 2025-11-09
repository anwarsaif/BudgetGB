using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Logix.Application.Validators;

namespace Logix.Application.DTOs.GB
{
    public class BudgTransactionDetaileYearDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        [CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal Debit { get; set; } = 0;
        //[ModelBinder(typeof(CustomDecimalModelBinder))]
        [CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public int? DeleteUserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        [CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        [CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]

        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]

        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Entity", "PM")]
        public int AccAccountType { get; set; }
        public decimal Balance { get; set; } = 0;
        [CustomDisplay("AllYear", "Acc")]
        public bool AllYear { get; set; }
        [CustomDisplay("itemsNameAr", "Acc")]

        public string? AccAccountNameS { get; set; }
        [CustomDisplay("items_No", "Acc")]
        public string? AccAccountCodeS { get; set; }
        public decimal? MoneyValue { get; set; }
        public decimal? CostsValue { get; set; }
        public decimal? moneyValueAll { get; set; }
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        [StringLength(50)]
        public string? AccAccountParentCode { get; set; }
        [CustomDisplay("ParentItemsName", "Core")]
        [StringLength(250)]
        public string? AccAccountnameParent { get; set; }
        [CustomDisplay("Acc_ParentName", "Acc")]

        public string? AccAccountnameParent2 { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        public string? AccGroupName { get; set; }
        public string? AccGroupName2 { get; set; }
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYearCu { get; set; }
        public int? itemType { get; set; }

    }
    public class BudgTransactionDetaileDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal Debit { get; set; } = 0;
        //[ModelBinder(typeof(CustomDecimalModelBinder))]
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public int? DeleteUserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        [CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        [CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]

        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]

        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Entity", "PM")]
        public int AccAccountType { get; set; }
        public decimal Balance { get; set; } = 0;
        [CustomDisplay("AllYear", "Acc")]
        public bool AllYear { get; set; }
        [CustomDisplay("itemsNameAr", "Acc")]

        public string? AccAccountNameS { get; set; }
        [CustomDisplay("items_No", "Acc")]
        public string? AccAccountCodeS { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "*")]

        [CustomDisplay("Amount", "Acc")]
        public decimal Value { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? TypeID { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("Amount", "Acc")]
        public decimal Value2 { get; set; }
        public string? MCode { get; set; }

        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }
        [CustomDisplay("AppId", "Acc")]

        public long? AppId { get; set; }

        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; } = 0;
        [CustomDisplay("Status", "Acc")]
        public int? StatusId { get; set; } = 0;

    }
    public class BudgTransactionDetaileEditDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }



        public int? ModifiedBy { get; set; }





        public DateTime? ModifiedOn { get; set; }


        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        //[CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "*")]

        [CustomDisplay("Amount", "Acc")]
        public decimal Value { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? TypeID { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        public decimal? LinkDebit { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }
        [CustomDisplay("Amount", "Core")]
        public decimal? DebitEdit { get; set; }
    }
    public class BudgTransactionDetaileModel
    {
        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        [CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }
        [CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public int? DeleteUserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        //[CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Entity", "PM")]
        public int AccAccountType { get; set; }
        public decimal? Balance { get; set; }
    }
    public class BudgTransactionDetaileLinksInitialDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }



        public int? ModifiedBy { get; set; }





        public DateTime? ModifiedOn { get; set; }


        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        //[CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }

        [CustomDisplay("Amount", "Acc")]
        public decimal Value { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? TypeID { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        public decimal? LinkDebit { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }

        public decimal? Amount { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }

    }

    public class BudgTransactionDetaileLinksInitialEditDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }



        public int? ModifiedBy { get; set; }





        public DateTime? ModifiedOn { get; set; }


        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        //[CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }

        [CustomDisplay("Amount", "Acc")]
        public decimal Value { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? TypeID { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        public decimal? LinkDebit { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }
        [CustomDisplay("Amount", "Core")]
        public decimal? DebitEdit { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }

    }
    public class BudgTransactionDetaileDiscountsDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }



        public int? ModifiedBy { get; set; }





        public DateTime? ModifiedOn { get; set; }


        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        //[CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }

        [CustomDisplay("Amount", "Acc")]
        public decimal Value { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? TypeID { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        public decimal? LinkDebit { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }

    }

    public class BudgTransactionDetaileDiscountsEditDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }



        public int? ModifiedBy { get; set; }





        public DateTime? ModifiedOn { get; set; }


        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        //[CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }

        [CustomDisplay("Amount", "Acc")]
        public decimal Value { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? TypeID { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        public decimal? LinkDebit { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }
        [CustomDisplay("Amount", "Core")]
        public decimal? DebitEdit { get; set; }
    }
    public class BudgTransactionDetaileLinksMultipleDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        [Required(ErrorMessage = "*")]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }

        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }




        public bool? IsDeleted { get; set; }


        public string? ReferenceCode { get; set; }

        //public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        [Required(ErrorMessage = "*")]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }

        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "*")]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "*")]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }
        [CustomDisplay("ExpenseType", "Core")]
        public string? ExpenseName { get; set; }
        public bool AllowAmountTotal { get; set; }
        public bool AllowAmountDiscounts { get; set; }
        public bool AllowAmountLinks { get; set; }
        public bool AllowAmountReinforcements { get; set; }
        public bool AllowAmountTransfersTO { get; set; }
        public bool AllowAmountTransfersFrom { get; set; }
        public int? ModifiedBy { get; set; }





        public DateTime? ModifiedOn { get; set; }

    }


    public class BudgTransactionDetaileLinksDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal Debit { get; set; } = 0;
        //[ModelBinder(typeof(CustomDecimalModelBinder))]
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public int? DeleteUserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        [CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        [CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]

        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]

        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Name2 { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("Entity", "PM")]
        public int AccAccountType { get; set; }
        public decimal Balance { get; set; } = 0;
        [CustomDisplay("AllYear", "Acc")]
        public bool AllYear { get; set; }
        [CustomDisplay("itemsNameAr", "Acc")]

        public string? AccAccountNameS { get; set; }
        [CustomDisplay("items_No", "Acc")]
        public string? AccAccountCodeS { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "*")]

        [CustomDisplay("Amount", "Acc")]
        public decimal Value { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? TypeID { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("Amount", "Acc")]
        public decimal Value2 { get; set; }
        public string? MCode { get; set; }

        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }
        [CustomDisplay("AppId", "Acc")]

        public long? AppId { get; set; }

        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; } = 0;

    }
    public class BudgTransactionDetaileLinksEditDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }
        //[CustomRequired]
        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        public long? ReferenceNo { get; set; }
        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }



        public int? ModifiedBy { get; set; }





        public DateTime? ModifiedOn { get; set; }


        public bool? IsDeleted { get; set; }
        public bool? Auto { get; set; }

        public int? CurrencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public long? Cc2Id { get; set; }

        public long? Cc3Id { get; set; }

        public string? ReferenceCode { get; set; }

        public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        //[CustomRequired]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        public decimal? AmountTransfers { get; set; }
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]
        public string? StartDate { get; set; }


        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]
        public string? EndDate { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "*")]

        [CustomDisplay("Amount", "Acc")]
        public decimal Value { get; set; }
        [CustomDisplay("Type", "Core")]
        public long? TypeID { get; set; }
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        public decimal? LinkDebit { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }
        [CustomDisplay("Amount", "Core")]
        public decimal? DebitEdit { get; set; }

        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; } = 0;
    }

    public class BudgTransactionDetailelinkFinalMultiDto
    {


        public long Id { get; set; }
        public int IncreasId { get; set; }
        public long? TId { get; set; }
        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }

        public long? AccAccountId { get; set; }

        public long? CcId { get; set; }
        [Required(ErrorMessage = "*")]
        [CustomDisplay("Amount", "Core")]
        public decimal? Debit { get; set; }

        [CustomDisplay("Amount", "Acc")]
        public decimal? Credit { get; set; }

        public int? ReferenceTypeId { get; set; }


        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }




        public bool? IsDeleted { get; set; }


        public string? ReferenceCode { get; set; }

        //public decimal? Rate { get; set; }
        //[CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        public string? AccAccountName { get; set; }


        [CustomDisplay("AccNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }


        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        [Required(ErrorMessage = "*")]
        public string? AccAccountCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_No", "Acc")]
        public string? CostCenterCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("Cc_Name", "Acc")]
        public string? CostCenterName { get; set; }
        [CustomDisplay("Cc_NameEn", "Acc")]
        public string? CostCenterName2 { get; set; }
        [CustomDisplay("Approveditem", "Core")]
        public decimal? AmountInitial { get; set; }
        [CustomDisplay("Transferredfromhim", "Core")]
        public decimal? AmountTransfersFrom { get; set; }
        [CustomDisplay("Transferredtohim", "Core")]
        public decimal? AmountTransfersTO { get; set; }
        [CustomDisplay("Reinforcementitem", "Core")]
        public decimal? AmountReinforcements { get; set; }
        [CustomDisplay("Linksitem", "Core")]
        public decimal? AmountLinks { get; set; }
        [CustomDisplay("Discountitem", "Core")]
        public decimal? AmountDiscounts { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }

        [CustomDisplay("MovementNo", "Sal")]
        public string? Code { get; set; }
        [CustomDisplay("MovementNo", "Sal")]
        public string? Code2 { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "*")]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "*")]
        [CustomDisplay("ExpenseType", "Core")]
        public long? ExpenseId { get; set; }
        [CustomDisplay("ExpenseType", "Core")]
        public string? ExpenseName { get; set; }

        public int? ModifiedBy { get; set; }





        public DateTime? ModifiedOn { get; set; }

    }
    public class BudgTransactionDetaileItemTransactionsDto
    {


        [CustomDisplay("DocDate", "Acc")]
        public string? DateGregorian { get; set; }
        //[Required(ErrorMessage = "*")]

        [CustomDisplay("items_No", "Acc")]


        [StringLength(50)]
        public string? AccAccountCode { get; set; }

        [CustomDisplay("items_No", "Acc")]
        public string? AccAccountName { get; set; }

        [CustomDisplay("Approveditem", "Core")]
        public decimal AmountInitial { get; set; } = 0;


        [CustomDisplay("Linksitem", "Core")]
        public decimal AmountLinks { get; set; } = 0;
        [CustomDisplay("Available", "Core")]
        public decimal AmountTotal { get; set; } = 0;
        [StringLength(10)]
        [CustomDisplay("FromDate", "Common")]

        public string StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        [StringLength(10)]
        [CustomDisplay("ToDate", "Common")]

        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture); //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy/MM/dd", new CultureInfo("en-US")).ToString();


        [CustomDisplay("TransactionType", "Acc")]
        public string? DocTypeName { get; set; }

        public string? DocTypeName2 { get; set; }
        public string? Code { get; set; }
        public decimal Balance { get; set; } = 0;


        [CustomDisplay("Description", "Acc")]
        public string? Description { get; set; }

        public decimal AmountExpenses { get; set; } = 0;
        [CustomDisplay("Status", "Acc")]
        public int? StatusId { get; set; }
        [CustomDisplay("TransactionType", "Acc")]

        public int? DocTypeId { get; set; }

    }
}
