using Microsoft.EntityFrameworkCore;
using DevPulse.Data.Models;

namespace DevPulse.Data;

public class DevPulseContext : DbContext
{
    public DevPulseContext(DbContextOptions<DevPulseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Repository> Repositories { get; set; }
    public DbSet<Metric> Metrics { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Repository
        modelBuilder.Entity<Repository>()
            .HasKey(r => r.Id);
        modelBuilder.Entity<Repository>()
            .HasOne(r => r.User)
            .WithMany(u => u.Repositories)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Repository>()
            .HasIndex(r => new { r.UserId, r.GitHubId })
            .IsUnique();

        // Metric
        modelBuilder.Entity<Metric>()
            .HasKey(m => m.Id);
        modelBuilder.Entity<Metric>()
            .HasOne(m => m.Repository)
            .WithMany(r => r.Metrics)
            .HasForeignKey(m => m.RepositoryId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Metric>()
            .HasIndex(m => new { m.RepositoryId, m.PeriodStart, m.PeriodEnd });

        // Session
        modelBuilder.Entity<Session>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Session>()
            .HasOne(s => s.User)
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Session>()
            .HasIndex(s => s.Token)
            .IsUnique();

        // ActivityLog
        modelBuilder.Entity<ActivityLog>()
            .HasKey(a => a.Id);
        modelBuilder.Entity<ActivityLog>()
            .HasOne(a => a.Repository)
            .WithMany()
            .HasForeignKey(a => a.RepositoryId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ActivityLog>()
            .HasIndex(a => new { a.RepositoryId, a.OccurredAt });
    }
}
