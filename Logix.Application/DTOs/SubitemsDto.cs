using Logix.Application.Validators;
using Logix.Domain.ACC;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Logix.Application.DTOs.GB
{
    public class BranchListVM
    {
        public int BranchId { get; set; }
        public string? BraName { get; set; } = String.Empty;
        public string? BraName2 { get; set; } = String.Empty;
        public bool Selected { get; set; } = false;
    }
    public class SubitemsDto
    {

        public long AccAccountId { get; set; }
        [CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]

        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        //[CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("IsSub", "Acc")]
        public bool IsSub { get; set; }
        //[RequiredDDL(0)]

        public bool EnableAccountParentId { get; set; } = false;
        //public bool IsEnable { get; set; } = false;
        //[NonZeroIfEnabledSubitems(ErrorMessage = "*")]
        [RequiredDDL(0)]
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        /// <summary>
        /// مركز التكلفة الافتراضي
        ////[RequiredDDL(0)]
        [CustomDisplay("Customer", "Acc")]
        public long? CcId { get; set; }


        [RequiredDDL(0)]
        [CustomDisplay("Acc_Closing", "Acc")]
        public int? AccountCloseTypeId { get; set; }
        //[CustomRequired]
        [CustomDisplay("AccountLevel", "Acc")]

        public int? AccountLevel { get; set; }
        [CustomDisplay("Is_Help_Account", "Acc")]
        public bool IsHelpAccount { get; set; }

        public bool? Aggregate { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("items_Status", "Acc")]
        public bool IsActive { get; set; } = true;

        public long? FacilityId { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("Currency", "Acc")]
        public int? CurrencyId { get; set; }

        public int? BranchId { get; set; }


        public long? AccAccountParentId2 { get; set; }

        public long? AccAccountParentId3 { get; set; }
        [CustomDisplay("ParentItemsNo", "Core")]

        [StringLength(50)]
        public string? AccAccountParentCode { get; set; }
        //[CustomRequired]
        [CustomDisplay("ParentItemsName", "Core")]
        [StringLength(250)]
        public string? AccAccountnameParent { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("DeptSec", "Hr")]
        public int? DeptID { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("Entity", "PM")]
        public int? AccAccountType { get; set; }
        [CustomRequired]
        [CustomDisplay("REFRANCENO", "Core")]
        public string? RefranceNo { get; set; }


        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }


        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public bool Numbring { get; set; } = true;
        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public int? DeleteUserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeleteDate { get; set; }
        public bool? IsDeleted { get; set; }

        public string? CurrencyName { get; set; }

        [CustomDisplay("DeptSec", "Hr")]
        public string? DeptName { get; set; }
        [RequiredChildren]

        public List<SubitemsDto>? Children { get; set; }

        public DateTime RefranceDate { get; set; }
        //-----------------------------------------

        [CustomDisplay("itemsNameAr", "Acc")]
        [StringLength(255)]
        public string? AccAccountNameS { get; set; }
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCodeS { get; set; }
        [CustomDisplay("DocNo", "Acc")]
        public string? Code { get; set; }
        public string? Code2 { get; set; }
        [CustomDisplay("AccGroupName", "Acc")]
        public string? AccGroupName { get; set; }
        //[CustomRequired]
        [RequiredDDL(0)]
        [CustomDisplay("Finyear", "Acc")]
        public long? FinYear { get; set; }
        [RequiredChildren]

        public List<BranchListVM>? BranchList { get; set; }
        [CustomRequired]
        [CustomDisplay("duration", "Core")]
        public int? duration { get; set; }

        public string? Branches { get; set; }
        [CustomRequired]
        [CustomDisplay("itemType", "Core")]

        public int? itemType { get; set; }
        public int? SystemId { get; set; }
        [CustomRequired]
        [StringLength(10)]
        [CustomDisplay("StartDate", "Core")]
        public string? StartDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        [CustomRequired]
        [StringLength(10)]
        [CustomDisplay("EndDate", "Core")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        [CustomDisplay("Itemduration", "Core")]
        [RequiredDDL(0)]
        public int? DurationType { get; set; }

        [RequiredDDL(0)]
        [CustomDisplay("Budgetcategory", "Core")]

        public int? CategoryId { get; set; }
        [CustomRequired]
        [CustomDisplay("REFRANCEName", "Core")]
        public string? RefranceName { get; set; }

        public SubitemsDto()
        {
            ChildrenExpenses = new List<BudgAccountExpensesDto>();



        }
        public List<BudgAccountExpensesDto> ChildrenExpenses { get; set; }
        public bool mainParent { get; set; } = false;


    }
    public class SubitemsEditDto
    {
        public long AccAccountId { get; set; }
        [CustomRequired]
        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }
        //[CustomRequired]
        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }
        [RequiredDDL(0)]
        [CustomDisplay("ItemGroup", "Core")]
        public long? AccGroupId { get; set; }
        [CustomRequired]
        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [CustomDisplay("IsSub", "Acc")]
        public bool IsSub { get; set; }
        //////public bool EnableAccountParentId { get; set; } = false;
        //public bool IsEnable { get; set; } = false;
        //[NonZeroIfEnabledSubitemsEdit(ErrorMessage = "*")]
        [RequiredDDL(0)]
        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }
        /// <summary>
        /// مركز التكلفة الافتراضي
        ////[RequiredDDL(0)]
        //[CustomDisplay("Customer", "Acc")]
        //public long? CcId { get; set; }


        //[RequiredDDL(0)]
        //[CustomDisplay("Acc_Closing", "Acc")]
        //public int? AccountCloseTypeId { get; set; }
        //[CustomRequired]
        [CustomDisplay("AccountLevel", "Acc")]

        public int? AccountLevel { get; set; }
        //[CustomDisplay("Is_Help_Account", "Acc")]
        //public bool IsHelpAccount { get; set; }

        public bool? Aggregate { get; set; }
        //[RequiredDDL(0)]
        //[CustomDisplay("items_Status", "Acc")]
        //public bool IsActive { get; set; }

        //[RequiredDDL(0)]
        //[CustomDisplay("Currency", "Acc")]
        //public int? CurrencyId { get; set; }

        //public int? BranchId { get; set; }



        //public long? AccAccountParentId2 { get; set; }

        //public long? AccAccountParentId3 { get; set; }
        ////[CustomRequired]
        //[CustomDisplay("ParentItemsNo", "Core")]

        //[StringLength(50)]
        //public string? AccAccountParentCode { get; set; }
        ////[CustomRequired]
        //[CustomDisplay("ParentItemsName", "Core")]
        //[StringLength(250)]
        //public string? AccAccountnameParent { get; set; }
        //[RequiredDDL(0)]

        //[CustomDisplay("DeptSec", "Hr")]
        public int? DeptID { get; set; }
        [RequiredDDL(0)]

        [CustomDisplay("ItemsType", "Acc")]
        public int? AccAccountType { get; set; }
        [CustomRequired]
        [CustomDisplay("REFRANCENO", "Core")]
        public string? RefranceNo { get; set; }



        [CustomDisplay("DescriptionIndicative", "Core")]
        public string? Note { get; set; }
        //[CustomRequired]
        [CustomDisplay("DocNo", "Acc")]
        public string? Code { get; set; }
        public string? Code2 { get; set; }
        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]

        public string? Name { get; set; }
        public string? Name2 { get; set; }
        //public bool Numbring { get; set; }

        //public string? CurrencyName { get; set; }
        //[CustomDisplay("DeptSec", "Hr")]
        //public string? DeptName { get; set; }

        //[RequiredDDL(0)]
        //[CustomDisplay("Finyear", "Acc")]
        //public long? FinYear { get; set; }
        [RequiredEditChildrenAttribute]

        public List<BranchListVM>? BranchList { get; set; }
        [CustomRequired]
        [CustomDisplay("duration", "Core")]
        public int? duration { get; set; }

        [CustomDisplay("Branches", "Main")]
        public string? Branches { get; set; }

        [CustomDisplay("itemType", "Core")]
        [RequiredDDL(0)]
        public int? itemType { get; set; }
        [CustomRequired]
        [StringLength(10)]
        [CustomDisplay("StartDate", "Core")]
        public string? StartDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        [CustomRequired]
        [StringLength(10)]
        [CustomDisplay("EndDate", "Core")]
        public string? EndDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        [CustomDisplay("Itemduration", "Core")]
        [RequiredDDL(0)]
        public int? DurationType { get; set; }

        [RequiredDDL(0)]
        [CustomDisplay("Budgetcategory", "Core")]

        public int? CategoryId { get; set; }
        [CustomRequired]
        [CustomDisplay("REFRANCEName", "Core")]
        public string? RefranceName { get; set; }
        public SubitemsEditDto()
        {
            ChildrenExpenses = new List<BudgAccountExpensesDto>();



        }
        public List<BudgAccountExpensesDto> ChildrenExpenses { get; set; }
    }
    public class SubitemsReportVM
    {

        public long AccAccountId { get; set; }

        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountName { get; set; }


        [StringLength(255)]
        [CustomDisplay("itemsNameEn", "Acc")]
        public string? AccAccountName2 { get; set; }

        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCode { get; set; }


        [CustomDisplay("ParentItems", "Core")]
        public long? AccAccountParentId { get; set; }


        public long? FacilityId { get; set; }


        [CustomDisplay("Acc_ParentNo", "Acc")]

        [StringLength(50)]
        public string? AccAccountParentCode { get; set; }
        [CustomDisplay("Acc_ParentName", "Acc")]
        [StringLength(250)]
        public string? AccAccountnameParent { get; set; }



        //[CustomRequired]
        [CustomDisplay("DocNo", "Acc")]
        public string? Code { get; set; }
        public string? Code2 { get; set; }
        //[CustomRequired]
        [CustomDisplay("VATName", "Acc")]
        public string? Name { get; set; }
        //[RequiredDDL(0)]

        [CustomDisplay("DeptSec", "Hr")]
        public int? DeptID { get; set; }
        [CustomDisplay("AccGroupName", "Acc")]
        public string AccGroupName { get; set; } = null!;

        [CustomDisplay("items_Status", "Acc")]
        public bool? IsActive { get; set; } = true;
        public string? Name2 { get; set; }
        public decimal AmountInitial { get; set; }
        public decimal AmountReinforcements { get; set; }
        public decimal AmountTransfersFrom { get; set; }
        public decimal AmountTransfersTo { get; set; }
        public decimal AmountLinks { get; set; }
        public decimal AmountTotal { get; set; }
        public decimal AmountDiscounts { get; set; }
        public decimal AmountInitialLinks { get; set; }
        public decimal AmountLinksFinal { get; set; }


        [CustomDisplay("itemsNameAr", "Acc")]


        [StringLength(255)]
        public string? AccAccountNameS { get; set; }




        [CustomDisplay("items_No", "Acc")]

        [StringLength(50)]
        public string? AccAccountCodeS { get; set; }
        public decimal AmountExpenses { get; set; }

        [CustomDisplay("itemType", "Core")]
        public string? itemType { get; set; }
        public string? Refrance_No { get; set; }

        [CustomDisplay("itemType", "Core")]

        public int? itemTypeSr { get; set; }

    }
    public class SubitemsVM : AccAccountsVw
    {
        public decimal AmountInitial { get; set; } = 0;
        public decimal AmountTransfersFrom { get; set; } = 0;
        public decimal AmountTransfersTo { get; set; } = 0;
        public decimal AmountReinforcements { get; set; } = 0;
        public decimal AmountLinks { get; set; } = 0;
        public decimal AmountTotal { get; set; } = 0;
        public decimal AmountDiscounts { get; set; } = 0;
        public decimal AmountExpenses { get; set; } = 0;
        public decimal AmountExpensesPercentage { get; set; } = 0;
        public decimal AmountInitialLinks { get; set; } = 0;
        public decimal AmountLinksFinal { get; set; } = 0;
        public decimal AmountLinksFinalPercentage { get; set; } = 0;
        public decimal Modification { get; set; } = 0;
        public decimal ApprovedModification { get; set; } = 0;
        public string? itemTypeN { get; set; }
        public string? Refrance_No { get; set; }

        public List<SubitemsVM>? Children { get; set; } = new List<SubitemsVM>();
        public int Level { get; set; }
    }
    public class SubitemExportExcelVM
    {
        public long AccAccountId { get; set; }
        public string? AccAccountName { get; set; }
        public string? AccAccountName2 { get; set; }
        public string? AccAccountCode { get; set; }
        public decimal AmountInitial { get; set; } = 0;
        public decimal AmountTransfersFrom { get; set; } = 0;
        public decimal AmountTransfersTo { get; set; } = 0;
        public decimal AmountReinforcements { get; set; } = 0;
        public decimal AmountLinks { get; set; } = 0;
        public decimal AmountTotal { get; set; } = 0;
        public decimal AmountDiscounts { get; set; } = 0;

    }
}
