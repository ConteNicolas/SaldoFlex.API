using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Infrastructure.Persistence.Configurations;

public class FinancialPlanConfiguration : IEntityTypeConfiguration<FinancialPlan>
{
    public void Configure(EntityTypeBuilder<FinancialPlan> builder)
    {
        builder
            .HasOne(x => x.FinancialScene)
            .WithOne(b => b.FinancialPlan)
            .HasForeignKey<FinancialPlan>(b => b.FinancialSceneId);
    }
}
