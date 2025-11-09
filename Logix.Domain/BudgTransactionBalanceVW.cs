using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.GB
{
   
    [Keyless]
    public class BudgTransactionBalanceVW 
    {

        [Column("Acc_Account_ID")]
        public long AccAccountId { get; set; }
        [Column("Acc_Account_Name")]
        [StringLength(255)]
        public string? AccAccountName { get; set; }
        [Column("Acc_Account_Name2")]
        [StringLength(255)]
        public string? AccAccountName2 { get; set; }
        [Column("Acc_group_ID")]
        public long? AccGroupId { get; set; }
        [Column("Acc_Account_Code")]
        [StringLength(50)]
        public string? AccAccountCode { get; set; }
        [StringLength(500)]
        public string? Note { get; set; }
        [Column("Fin_year")]
        public long? FinYear { get; set; }
        public int? itemType { get; set; }

        public decimal AmountInitial { get; set; }

        public decimal AmountTransfersFrom { get; set; }

        public decimal AmountTransfersTo { get; set; }

        public decimal AmountReinforcements { get; set; }

        public decimal AmountDiscounts { get; set; }

        public decimal LinksFinal { get; set; }

        public decimal InitialLinks { get; set; }

        public decimal AmountLinks { get; set; }

        public decimal AmountTotal { get; set; }
        public bool? FlagDelete { get; set; }
        [Column("Budg_Type_Id")]
        public long? BudgTypeId { get; set; }

    }
}
