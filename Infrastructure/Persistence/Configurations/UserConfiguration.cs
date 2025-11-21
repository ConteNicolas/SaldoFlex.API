using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(x => x.Account)
            .WithOne(x => x.User)
            .HasForeignKey<User>(x => x.AccountId);
    }
}
