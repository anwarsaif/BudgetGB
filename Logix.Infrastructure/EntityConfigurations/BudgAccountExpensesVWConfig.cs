using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Logix.Domain.GB;

namespace Logix.Infrastructure.EntityConfigurations.GB
{
    public class BudgAccountExpensesVWConfig : IEntityTypeConfiguration<BudgAccountExpensesVW>
    {
        public void Configure(EntityTypeBuilder<BudgAccountExpensesVW> entity)
        {

            entity.ToView("Budg_Account_Expenses_VW");
        }
    }

}
