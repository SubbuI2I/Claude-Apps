namespace DevPulse.Data.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? FullName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public ICollection<Repository> Repositories { get; set; } = new List<Repository>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}

public class Repository
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Owner { get; set; } = null!;
    public string RepositoryUrl { get; set; } = null!;
    public long GitHubId { get; set; }
    public string? Description { get; set; }
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastSyncedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public ICollection<Metric> Metrics { get; set; } = new List<Metric>();
}

public class Metric
{
    public Guid Id { get; set; }
    public Guid RepositoryId { get; set; }
    public int CommitCount { get; set; }
    public int PullRequestCount { get; set; }
    public int IssueCount { get; set; }
    public int StarCount { get; set; }
    public int ForksCount { get; set; }
    public double? AveragePRMergeTimeHours { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }

    public Repository Repository { get; set; } = null!;
}

public class Session
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }

    public User User { get; set; } = null!;
}

public class ActivityLog
{
    public Guid Id { get; set; }
    public Guid RepositoryId { get; set; }
    public string ActivityType { get; set; } = null!; // "commit", "pr", "issue"
    public string Author { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? ExternalUrl { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public Repository Repository { get; set; } = null!;
}
