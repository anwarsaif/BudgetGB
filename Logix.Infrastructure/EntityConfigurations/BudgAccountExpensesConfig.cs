using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logix.Domain.GB;

namespace Logix.Infrastructure.EntityConfigurations.GB
{

    public class BudgAccountExpensesConfig : IEntityTypeConfiguration<BudgAccountExpenses>
    {
        public void Configure(EntityTypeBuilder<BudgAccountExpenses> entity)
        {

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");


            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");


        }
    }
}
