using Logix.Application.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.GB
{
    public class BudgAccountExpensesDto
    {

        public long Id { get; set; }
        public long AccAccountId { get; set; }

        [CustomDisplay("ExpenseName", "Core")]
        [StringLength(2500)]
        public string? ExpenseName { get; set; }
        public bool? IsDeleted { get; set; }
        [CustomDisplay("No", "Core")]
        public long IncreasId { get; set; }
        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
        

    }
    public class BudgAccountExpensesAddDto
    {
        public long Id { get; set; }
        [CustomDisplay("No", "Core")]
        public long IncreasId { get; set; }
        public long AccAccountId { get; set; }

        [CustomDisplay("ExpenseName", "Core")]
        [StringLength(2500)]
        public string? ExpenseName { get; set; }
        public bool? IsDeleted { get; set; }

    }


    public class BudgAccountExpensesEditDto
    {
        public long Id { get; set; }
        [CustomDisplay("No", "Core")]
        public long IncreasId { get; set; }
        public long AccAccountId { get; set; }

        [CustomDisplay("ExpenseName", "Core")]
        [StringLength(2500)]
        public string? ExpenseName { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }
}
