using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasOne(x => x.User)
            .WithOne(x => x.Account)
            .HasForeignKey<Account>(x => x.UserId);
    }
}
