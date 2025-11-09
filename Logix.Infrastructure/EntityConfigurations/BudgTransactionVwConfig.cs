using Logix.Domain.GB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.GB
{
    public class BudgTransactionVwConfig : IEntityTypeConfiguration<BudgTransactionVw>
    {
        public void Configure(EntityTypeBuilder<BudgTransactionVw> entity)
        {
            entity.ToView("Budg_Transaction_VW");

            entity.Property(e => e.ChequDateHijri).IsFixedLength();

            entity.Property(e => e.DateGregorian).IsFixedLength();

            entity.Property(e => e.DateHijri).IsFixedLength();
        }
    }
}
