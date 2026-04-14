using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Domain.Common;
using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullControlFootball.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserAuthProvider> UserAuthProviders => Set<UserAuthProvider>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<CareerSave> CareerSaves => Set<CareerSave>();
    public DbSet<Season> Seasons => Set<Season>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Competition> Competitions => Set<Competition>();
    public DbSet<Club> Clubs => Set<Club>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<ClubPlayer> ClubPlayers => Set<ClubPlayer>();
    public DbSet<SaveClub> SaveClubs => Set<SaveClub>();
    public DbSet<SavePlayer> SavePlayers => Set<SavePlayer>();
    public DbSet<SeasonCompetition> SeasonCompetitions => Set<SeasonCompetition>();
    public DbSet<CompetitionStanding> CompetitionStandings => Set<CompetitionStanding>();
    public DbSet<CompetitionStandingRow> CompetitionStandingRows => Set<CompetitionStandingRow>();
    public DbSet<CompetitionTopScorer> CompetitionTopScorers => Set<CompetitionTopScorer>();
    public DbSet<CompetitionTopAssist> CompetitionTopAssists => Set<CompetitionTopAssist>();
    public DbSet<TransferWindow> TransferWindows => Set<TransferWindow>();
    public DbSet<TransferTransaction> TransferTransactions => Set<TransferTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(BaseEntity.CreatedAtUtc)).CurrentValue = utcNow;
            }

            if (entry.Entity is AuditableEntity auditable && entry.State == EntityState.Modified)
            {
                auditable.MarkUpdatedUtc(utcNow);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
