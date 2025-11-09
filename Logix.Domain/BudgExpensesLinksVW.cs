using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.GB
{
    [Keyless]
    public class BudgExpensesLinksVW : TraceEntity
    {
        [Column("ID")]
        public long Id { get; set; }

        [Column("Link_ID")]
        public long LinkID { get; set; }
        [Column("Financial_No")]
        public long FinancialNo { get; set; }
        [Column(TypeName = "decimal(18, 5)")]
        public decimal? Amount { get; set; }
        [StringLength(255)]
        public string? LinkCode { get; set; }
        [Column("App_Code")]
        public long? AppCode { get; set; }


        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Fin_year")]
        public long? Finyear { get; set; }
        [Column("Doc_Type_ID")]
        public int? DocTypeId { get; set; }
        [Column("Doc_Type_Name")]
        [StringLength(50)]
        public string? DocTypeName { get; set; }
        [Column("Doc_Type_Name2")]
        [StringLength(50)]
        public string? DocTypeName2 { get; set; }
        [Column("App_Date")]
        [StringLength(50)]
        public string? AppDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Debit { get; set; }
        [Column("Customer_Code")]
        public string? CustomerCode { get; set; }
        [Column("Customer_Name")]
        [StringLength(2500)]
        public string? CustomerName { get; set; }
        [Column("Customer_Name2")]
        [StringLength(2500)]
        public string? CustomerName2 { get; set; }

        [Column("Acc_Account_Type")]
        public int AccAccountType { get; set; }
        [Column(TypeName = "decimal(18, 5)")]
        public decimal? RequestAmount { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Account_Code")]
        [StringLength(50)]
        public string? AccountCode { get; set; }
        [Column("Account_Name")]
        [StringLength(255)]
        public string? AccountName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
    }
}
