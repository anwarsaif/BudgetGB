using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logix.Domain.Base;

namespace Logix.Domain.GB
{
    [Keyless]
    public partial class BudgTransactionDetailesVw:TraceEntity
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("T_ID")]
        public long? TId { get; set; }
        [Column("Acc_Account_ID")]
        public long? AccAccountId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Debit { get; set; } = 0;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Credit { get; set; } = 0;
        [Column("Reference_Type_ID")]
        public int? ReferenceTypeId { get; set; }
        [Column("Reference_No")]
        public long? ReferenceNo { get; set; }
        [StringLength(2000)]
        public string? Description { get; set; }
      
        [Column("Acc_Account_Name")]
        [StringLength(258)]
        public string? AccAccountName { get; set; }
        [Column("Acc_Account_Code")]
        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [Column("Acc_group_ID")]
        public long? AccGroupId { get; set; }
        [Column("Acc_Account_Name2")]
        [StringLength(255)]
        public string? AccAccountName2 { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column("CostCenter_Code")]
        [StringLength(50)]
        public string? CostCenterCode { get; set; }
        [Column("CostCenter_Name")]
        [StringLength(150)]
        public string? CostCenterName { get; set; }
        [Column("Date_Gregorian")]
        [StringLength(10)]
        public string? DateGregorian { get; set; }
        public long? Code { get; set; }
        [StringLength(4000)]
        public string? Name { get; set; }
        [Column("Exchange_Rate", TypeName = "decimal(18, 10)")]
        public decimal? ExchangeRate { get; set; }
        [Column("Currency_ID")]
        public int? CurrencyId { get; set; }
        [Column("Reference_Type_Name")]
        [StringLength(50)]
        public string? ReferenceTypeName { get; set; }
        [Column("Parent_ID")]
        public int? ParentId { get; set; }
        [Column("CC2_ID")]
        public long? Cc2Id { get; set; }
        [Column("CC3_ID")]
        public long? Cc3Id { get; set; }
        [Column("Reference_Code")]
        [StringLength(50)]
        public string? ReferenceCode { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }

        [Column("Period_ID")]
        public long? PeriodId { get; set; }

    
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
        [Column("M_Code")]
        [StringLength(50)]
        public string? MCode { get; set; }
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

        public long? TypeID { get; set; }

        [Column("Status_Id")]
        public int? StatusId { get; set; }
        [Column("MReference_No")]
        public long? MReferenceNo { get; set; }
        [Column("Account_Note")]
        public string? AccountNote { get; set; }
        [Column("WF_Status_Id")]
        public int? WFStatusId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Expense_Id")]
        public long? ExpenseId { get; set; }
        [Column("Budg_Type_Id")]
        public long? BudgTypeId { get; set; }

    }
}
