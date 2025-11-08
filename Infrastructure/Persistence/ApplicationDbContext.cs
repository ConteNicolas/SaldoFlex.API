using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected ApplicationDbContext() { }
    
    //Commons
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Setting> Settings { get; set; }

    //FinalPlan
    public DbSet<FinancialPlan> FinancialPlans { get; set; }
    
    // FinancialScenes
    public DbSet<FinancialScene> FinancialScenes { get; set; }
    public DbSet<FinancialSceneGroup> FinancialSceneGroups { get; set; }
    public DbSet<FinancialSceneTransaction> FinancialSceneTransactions { get; set; }
}
