using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.GB
{
    [Table("Budg_Expenses_Links")]
    public partial class BudgExpensesLinks: TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
       
        [Column("Link_ID")]
        public long LinkID { get; set; }
        [Column("Financial_No")]
        public long FinancialNo { get; set; }
        [Column(TypeName = "decimal(18, 5)")]
        public decimal? Amount { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
       
    }
}
