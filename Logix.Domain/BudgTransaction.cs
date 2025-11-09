using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.ACC;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.Gb
{
    [Table("Budg_Transaction")]
    public partial class BudgTransaction : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Code { get; set; }
        [Column("Date_Hijri")]
        [StringLength(10)]
        public string? DateHijri { get; set; }
        [Column("Date_Gregorian")]
        [StringLength(10)]
        public string? DateGregorian { get; set; }
        [Column("T_time")]
        [StringLength(50)]
        public string? TTime { get; set; }
        [StringLength(2000)]
        public string? Description { get; set; }
        [Column("Period_ID")]
        public long? PeriodId { get; set; }
        [Column("Fin_year")]
        public long? FinYear { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column("Doc_Type_ID")]
        public int? DocTypeId { get; set; }
        [Column("Transfers_Type_Id ")]
        public int TransfersTypeId { get; set; }
       
        [Column("Status_Id")]
        public int? StatusId { get; set; }
        [Column("Payment_Type_ID")]
        public int? PaymentTypeId { get; set; }
        [Column("Chequ_No")]
        [StringLength(50)]
        public string? ChequNo { get; set; }
        [Column("Chequ_Date_Hijri")]
        [StringLength(10)]
        public string? ChequDateHijri { get; set; }
        [Column("Bank_ID")]
        public long? BankId { get; set; }
        [StringLength(2500)]
        public string? Bian { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [StringLength(4000)]
        public string? AmountWrite { get; set; }
        [Column("Reference_No")]
        public long? ReferenceNo { get; set; }
        [Column("Collection_Emp_ID")]
        public long? CollectionEmpId { get; set; }
        [Column("Currency_ID")]
        public int? CurrencyId { get; set; }
        [Column("Exchange_Rate", TypeName = "decimal(18, 10)")]
        public decimal? ExchangeRate { get; set; }
        [Column("Reference_Code")]
        [StringLength(50)]
        public string? ReferenceCode { get; set; }
        [Column("Project_ID")]
        public long? ProjectId { get; set; }
        [Column("Reference_Type")]
        public int? ReferenceType { get; set; }
        [Column("Reference_ID")]
        public long? ReferenceId { get; set; }
        [Column("Customer_ID")]
        public long? CustomerId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Acc_Account_Type")]
        public int AccAccountType { get; set; }

        public string? ProjectName { get; set; }
        [Column("Dept_ID")]
        public long? DeptID { get; set; }
        [Column("Budg_Type_Id")]
        public long? BudgTypeId { get; set; }

    }

}

