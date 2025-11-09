using Logix.Domain.ACC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.GB
{
    public class AccAccountsVwConfig : IEntityTypeConfiguration<AccAccountsVw>
    {
        public void Configure(EntityTypeBuilder<AccAccountsVw> entity)
        {
            entity.ToView("ACC_Accounts_VW");
        }
    } 

}
