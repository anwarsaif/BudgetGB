using System.ComponentModel.DataAnnotations;
using Logix.Application.Validators;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.GB
{

    public class PrintBudgExpensesLinksVM
    {
        public string? FacilityName { get; set; }
        public string? FacilityName2 { get; set; }
        public string? FacilityAddress { get; set; }
        public string? FacilityMobile { get; set; }
        public string? FacilityLogoPrint { get; set; }
        public string? FacilityLogoFooter { get; set; }

        public string? UserName { get; set; }

        public BudgExpensesLinksEditDto BudgExpensesLinksEditDto { get; set; }
        public PrintBudgExpensesLinksVM()
        {
            BudgExpensesLinksEditDto = new BudgExpensesLinksEditDto();

        }

    }
    public class BudgExpensesLinksDto
    {
       
        public long Id { get; set; }

        public long LinkID { get; set; }

        public long FinancialNo { get; set; }
        [CustomDisplay("Amount", "Acc")]
        public decimal? Amount { get; set; }
 
        
        public int? CreatedBy { get; set; }


        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }



        [CustomDisplay("RequestNo", "Acc")]
        public long? AppCode { get; set; }

        [CustomDisplay("RequestDate", "Acc")]
        public string? AppDate { get; set; }
        [CustomDisplay("Acc_No", "Acc")]
        public string? AccountCode { get; set; }
        [CustomDisplay("Acc_Name", "Acc")]
        public string? AccountName { get; set; }
        [CustomDisplay("linkNo", "Core")]
        public string? LinkCode { get; set; }
        [CustomDisplay("LinkAmount", "Core")]
        public decimal? LinkAmount { get; set; }
         [CustomDisplay("ExpensesAmount", "Core")]
        public decimal? ExpensesAmount { get; set; }

        [CustomDisplay("RequestAmount", "Core")]
        public decimal? RequestAmount { get; set; }
        [CustomDisplay("ExpensesAmount", "Core")]
        public decimal? ExRequestAmount { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? ExRemainingِِAmount { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        [CustomDisplay("branch", "Common")]
        public long? BranchId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
      
        [CustomDisplay("Entity", "PM")]
        public int AccAccountType { get; set; }
        [CustomDisplay("LinkAmountInitial", "Core")]
        public decimal? LinkAmountInitial { get; set; }
        [CustomDisplay("items_No", "Acc")]
        public string? ItemCode { get; set; }
        [CustomDisplay("itemsNameAr", "Acc")]
        public string? ItemName { get; set; }
        public long? AppId { get; set; }


    }
    public class BudgExpensesLinksEditDto
    {

        public long Id { get; set; }

        public long LinkID { get; set; }

        public long FinancialNo { get; set; }
        [CustomDisplay("Amount", "Acc")]
        public decimal? Amount { get; set; }

      


        [CustomDisplay("RequestNo", "Acc")]
        public long? AppCode { get; set; }

        [CustomDisplay("RequestDate", "Acc")]
        public string? AppDate { get; set; }
        [CustomDisplay("Acc_No", "Acc")]
        public string? AccountCode { get; set; }
        [CustomDisplay("Acc_Name", "Acc")]
        public string? AccountName { get; set; }
        [CustomDisplay("linkNo", "Core")]
        public string? LinkCode { get; set; }
        [CustomDisplay("LinkAmount", "Core")]
        public decimal? LinkAmount { get; set; }
        [CustomDisplay("ExpensesAmount", "Core")]
        public decimal? ExpensesAmount { get; set; }

        [CustomDisplay("RequestAmount", "Core")]
        public decimal? RequestAmount { get; set; }
        [CustomDisplay("ExpensesAmount", "Core")]
        public decimal? ExRequestAmount { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? ExRemainingِِAmount { get; set; }
        [CustomDisplay("Available", "Core")]
        public decimal? AmountTotal { get; set; }
        [CustomDisplay("branch", "Common")]
        public long? BranchId { get; set; }
        [CustomDisplay("VATName", "Acc")]
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

        [CustomDisplay("Entity", "PM")]
        public int AccAccountType { get; set; }

        public string? BraName { get; set; }

        [CustomDisplay("items_No", "Acc")]
        public string? ItemCode { get; set; }
        [CustomDisplay("itemsNameAr", "Acc")]
        public string? ItemName { get; set; }
    }
}
