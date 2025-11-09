using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.GB
{

    [Table("Budg_Account_Expenses")]
    public partial class BudgAccountExpenses : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Acc_Account_ID")]
        public long AccAccountId { get; set; }
        [StringLength(2500)]
        [Column("Expense_Name")]
        public string? ExpenseName { get; set; }

      

    }
}
