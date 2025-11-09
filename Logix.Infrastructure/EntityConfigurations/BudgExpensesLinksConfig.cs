using Logix.Domain.Gb;
using Logix.Domain.GB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.GB
{
    public class BudgExpensesLinksConfig : IEntityTypeConfiguration<BudgExpensesLinks>
    {
        public void Configure(EntityTypeBuilder<BudgExpensesLinks> entity)
        {

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }

}
