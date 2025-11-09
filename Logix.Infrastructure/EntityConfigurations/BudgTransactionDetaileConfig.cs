using Logix.Domain.Gb;
using Logix.Domain.GB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.EntityConfigurations.GB
{
    public class BudgTransactionDetaileConfig:IEntityTypeConfiguration<BudgTransactionDetaile>
    {
        public void Configure(EntityTypeBuilder<BudgTransactionDetaile> entity)
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
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            //entity.Property(e => e.Auto).HasDefaultValueSql("((0))");

            //entity.Property(e => e.f).HasDefaultValueSql("((0))");

            //entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            //entity.Property(e => e.ReferenceNo).HasComment("رقم المرجع في نظام التقسيط");

        }
    }
}
