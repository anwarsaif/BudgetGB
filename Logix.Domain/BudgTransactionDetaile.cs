using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.ACC;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.GB
{
    [Table("Budg_Transaction_Detailes")]
    public partial class BudgTransactionDetaile : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("T_ID")]
        public long? TId { get; set; }
        [Column("Date_Gregorian")]
        [StringLength(10)]
        public string? DateGregorian { get; set; }
        [Column("Acc_Account_ID")]
        public long? AccAccountId { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Debit { get; set; } = 0;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Credit { get; set; } = 0;
        [Column("Reference_Type_ID")]
        public int? ReferenceTypeId { get; set; }
        /// <summary>
        /// رقم المرجع في نظام التقسيط
        /// </summary>
        [Column("Reference_No")]
        public long? ReferenceNo { get; set; }
        [StringLength(2000)]
        public string? Description { get; set; }
        //[Column("Insert_User_ID")]
        //public int? InsertUserId { get; set; }
        //[Column("Update_User_ID")]
        //public int? UpdateUserId { get; set; }
        //[Column("Delete_User_ID")]
        //public int? DeleteUserId { get; set; }

        //[Column("Insert_Date", TypeName = "datetime")]
        //public DateTime? InsertDate { get; set; }
        //[Column("Update_Date", TypeName = "datetime")]
        //public DateTime? UpdateDate { get; set; }
        //[Column("Delete_Date", TypeName = "datetime")]
        //public DateTime? DeleteDate { get; set; }
        //public bool? FlagDelete { get; set; }
        public bool? Auto { get; set; }
        [Column("Currency_ID")]
        public int? CurrencyId { get; set; }
        [Column("Exchange_Rate", TypeName = "decimal(18, 10)")]
        public decimal? ExchangeRate { get; set; }
        [Column("CC2_ID")]
        public long? Cc2Id { get; set; }
        [Column("CC3_ID")]
        public long? Cc3Id { get; set; }
        [Column("Reference_Code")]
        [StringLength(50)]
        public string? ReferenceCode { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }
        public long? TypeID { get; set; }
        [Column("Expense_Id")]
        public long? ExpenseId { get; set; }

    }
}
