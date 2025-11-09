using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Logix.Domain.GB;

namespace Logix.Infrastructure.EntityConfigurations.GB
{
    public class BudgExpensesLinksVWConfig : IEntityTypeConfiguration<BudgExpensesLinksVW>
    {
        public void Configure(EntityTypeBuilder<BudgExpensesLinksVW> entity)
        {

            entity.ToView("Budg_Expenses_Links_VW");
        }
    }

}
