using Logix.Domain.ACC;
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
    
    public class BudgTransactionBalanceVWConfig : IEntityTypeConfiguration<BudgTransactionBalanceVW>
    {
        public void Configure(EntityTypeBuilder<BudgTransactionBalanceVW> entity)
        {
            entity.ToView("Budg_Transaction_Balance_VW");
        }
    }
}
