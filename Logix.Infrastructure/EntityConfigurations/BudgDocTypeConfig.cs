using Logix.Domain.GB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.GB
{
    public class BudgDocTypeConfig : IEntityTypeConfiguration<BudgDocType>
    {
        public void Configure(EntityTypeBuilder<BudgDocType> entity)
        {

            entity.HasKey(e => e.DocTypeId)
                    .HasName("PK_ACC_Budget_Doc_Type");

            entity.Property(e => e.DocTypeId).ValueGeneratedNever();

            entity.Property(e => e.FlagDelete).HasDefaultValueSql("((0))");

            entity.Property(e => e.TypeId).HasComment("هل تقديري ام فعلي");

        }
    }

}
