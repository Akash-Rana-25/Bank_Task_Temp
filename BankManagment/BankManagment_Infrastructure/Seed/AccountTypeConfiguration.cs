using BankManagment_Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace BankManagment_Infrastructure.Seed
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.HasData(
                new AccountType { Id = Guid.NewGuid(), Name = "Liability" },
                new AccountType { Id = Guid.NewGuid(), Name = "Asset" }
            );
        }
    }
}
