using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Infrastructure.Persistence.Configurations;

public class FinancialSceneConfiguration : IEntityTypeConfiguration<FinancialScene>
{
    public void Configure(EntityTypeBuilder<FinancialScene> builder)
    {
        builder
            .HasOne(x => x.FinancialPlan)
            .WithOne(b => b.FinancialScene)
            .HasForeignKey<FinancialScene>(b => b.FinancialPlanId);
    }
}
