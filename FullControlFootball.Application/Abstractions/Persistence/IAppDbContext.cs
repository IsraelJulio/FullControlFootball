using FullControlFootball.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullControlFootball.Application.Abstractions.Persistence;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserAuthProvider> UserAuthProviders { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<CareerSave> CareerSaves { get; }
    DbSet<Season> Seasons { get; }
    DbSet<Country> Countries { get; }
    DbSet<Competition> Competitions { get; }
    DbSet<Club> Clubs { get; }
    DbSet<Player> Players { get; }
    DbSet<ClubPlayer> ClubPlayers { get; }
    DbSet<SaveClub> SaveClubs { get; }
    DbSet<SavePlayer> SavePlayers { get; }
    DbSet<SeasonCompetition> SeasonCompetitions { get; }
    DbSet<CompetitionStanding> CompetitionStandings { get; }
    DbSet<CompetitionStandingRow> CompetitionStandingRows { get; }
    DbSet<CompetitionTopScorer> CompetitionTopScorers { get; }
    DbSet<CompetitionTopAssist> CompetitionTopAssists { get; }
    DbSet<TransferWindow> TransferWindows { get; }
    DbSet<TransferTransaction> TransferTransactions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
