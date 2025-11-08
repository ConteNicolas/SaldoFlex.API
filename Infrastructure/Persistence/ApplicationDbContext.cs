using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    
    //Commons
    public DbSet<User> Users { get; set; }
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
