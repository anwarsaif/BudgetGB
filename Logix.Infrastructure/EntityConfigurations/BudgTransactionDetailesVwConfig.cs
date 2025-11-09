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
    public class BudgTransactionDetailesVwConfig : IEntityTypeConfiguration<BudgTransactionDetailesVw>
    {
        public void Configure(EntityTypeBuilder<BudgTransactionDetailesVw> entity)
        {

            entity.Property(b => b.CreatedBy)
.HasColumnName("Insert_User_ID")
.HasColumnType("int")
.IsRequired(false);

            entity.Property(b => b.CreatedOn)
            .HasColumnName("Insert_Date")
            .HasColumnType("datetime");
            //.IsRequired(false);


            entity.Property(b => b.ModifiedBy)
                      .HasColumnName("Update_User_ID")
                      .HasColumnType("int")
                      .IsRequired(false);


            entity.Property(b => b.ModifiedOn)
            .HasColumnName("Update_Date")
            .HasColumnType("datetime")
            .IsRequired(false);

            entity.Property(b => b.IsDeleted)
.HasColumnName("FlagDelete")
.HasColumnType("bit")
.IsRequired(false);
            entity.ToView("Budg_Transaction_Detailes_VW");
        }
    }
    
}
