using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.GB
{
    [Keyless]
    public class BudgAccountExpensesVW : TraceEntity
    {
        
        [Column("ID")]
        public long Id { get; set; }
        [Column("Acc_Account_ID")]
        public long AccAccountId { get; set; }
        [StringLength(2500)]
        [Column("Expense_Name")]
        public string? ExpenseName { get; set; }
        [Column("Acc_Account_Name")]
        [StringLength(255)]
        public string? AccAccountName { get; set; }
        [Column("Acc_Account_Name2")]
        [StringLength(255)]
        public string? AccAccountName2 { get; set; }

        [Column("Acc_Account_Code")]
        [StringLength(50)]
        public string? AccAccountCode { get; set; }
    }
}
