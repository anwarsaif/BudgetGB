using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.GB
{
    [Keyless]
    public partial class BudgTransactionVw
    {
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
        [Column("Insert_User_ID")]
        public int? InsertUserId { get; set; }
        [Column("Update_User_ID")]
        public int? UpdateUserId { get; set; }
        [Column("Delete_User_ID")]
        public int? DeleteUserId { get; set; }
        [Column("Insert_Date", TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [Column("Update_Date", TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [Column("Delete_Date", TypeName = "datetime")]
        public DateTime? DeleteDate { get; set; }
        public bool? FlagDelete { get; set; }
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
        [Column("Doc_Type_Name")]
        [StringLength(50)]
        public string? DocTypeName { get; set; }
        [Column("Doc_Type_Name2")]
        [StringLength(50)]
        public string? DocTypeName2 { get; set; }
        [Column("USER_FULLNAME")]
        [StringLength(50)]
        public string? UserFullname { get; set; }
        [Column("Reference_No")]
        public long? ReferenceNo { get; set; }
        [Column("Status_Name")]
        [StringLength(50)]
        public string? StatusName { get; set; }
        [Column("Fin_year_Gregorian")]
        public int FinYearGregorian { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [StringLength(4000)]
        public string? AmountWrite { get; set; }
        [StringLength(250)]
        public string? BankName { get; set; }
        [Column("Payment_Type_Name")]
        [StringLength(255)]
        public string? PaymentTypeName { get; set; }
        [Column("Reference_Code")]
        [StringLength(50)]
        public string? ReferenceCode { get; set; }
        [Column("Collection_Emp_ID")]
        public long? CollectionEmpId { get; set; }
        [Column("Collection_Emp_Code")]
        [StringLength(50)]
        public string? CollectionEmpCode { get; set; }
        [Column("Collection_Emp_Name")]
        [StringLength(250)]
        public string? CollectionEmpName { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Exchange_Rate", TypeName = "decimal(18, 10)")]
        public decimal? ExchangeRate { get; set; }
        [Column("Currency_ID")]
        public int? CurrencyId { get; set; }
        [Column("Reference_Type")]
        public int? ReferenceType { get; set; }
        [Column("Reference_ID")]
        public long? ReferenceId { get; set; }
        [Column("Project_ID")]
        public long? ProjectId { get; set; }
        [Column("Project_Code")]
        public long? ProjectCode { get; set; }
        [Column("Project_Name")]
        [StringLength(2500)]
        public string? ProjectName { get; set; }
        [Column("Customer_ID")]
        public long? CustomerId { get; set; }
        [Column("Customer_Code")]
        public string? CustomerCode { get; set; }
        [Column("Customer_Name")]
        [StringLength(2500)]
        public string? CustomerName { get; set; }
        [Column("Customer_Name2")]
        [StringLength(2500)]
        public string? CustomerName2 { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Acc_Account_Type")]
        public int AccAccountType { get; set; }
        [Column("ProjectNameLink")]
        public string? ProjectNameLink { get; set; }
        [Column("Dept_ID")]
        public long? DeptID { get; set; }
        [Column("WF_Status_Id")]
        public int? WFStatusId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Budg_Type_Id")]
        public long? BudgTypeId { get; set; }



    }
}
