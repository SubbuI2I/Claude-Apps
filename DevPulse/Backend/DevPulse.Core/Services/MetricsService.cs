using DevPulse.Data;
using DevPulse.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DevPulse.Core.Services;

public interface IMetricsService
{
    Task<Metric?> RecordMetricsAsync(Guid repositoryId, int commits, int prs, int issues, int stars, int forks, double? avgPRTime);
    Task<List<Metric>> GetRepositoryMetricsAsync(Guid repositoryId, DateTime from, DateTime to);
    Task<Metric?> GetLatestMetricAsync(Guid repositoryId);
    Task<Dictionary<string, object>> GetAggregatedMetricsAsync(Guid userId);
}

public class MetricsService : IMetricsService
{
    private readonly DevPulseContext _context;

    public MetricsService(DevPulseContext context)
    {
        _context = context;
    }

    public async Task<Metric?> RecordMetricsAsync(Guid repositoryId, int commits, int prs, int issues, int stars, int forks, double? avgPRTime)
    {
        var now = DateTime.UtcNow;
        var dayStart = now.Date;
        var dayEnd = dayStart.AddDays(1).AddSeconds(-1);

        var metric = new Metric
        {
            Id = Guid.NewGuid(),
            RepositoryId = repositoryId,
            CommitCount = commits,
            PullRequestCount = prs,
            IssueCount = issues,
            StarCount = stars,
            ForksCount = forks,
            AveragePRMergeTimeHours = avgPRTime,
            RecordedAt = now,
            PeriodStart = dayStart,
            PeriodEnd = dayEnd
        };

        _context.Metrics.Add(metric);
        
        var repo = await _context.Repositories.FindAsync(repositoryId);
        if (repo != null)
        {
            repo.LastSyncedAt = now;
        }

        await _context.SaveChangesAsync();
        return metric;
    }

    public async Task<List<Metric>> GetRepositoryMetricsAsync(Guid repositoryId, DateTime from, DateTime to)
    {
        return await _context.Metrics
            .Where(m => m.RepositoryId == repositoryId && m.RecordedAt >= from && m.RecordedAt <= to)
            .OrderBy(m => m.RecordedAt)
            .ToListAsync();
    }

    public async Task<Metric?> GetLatestMetricAsync(Guid repositoryId)
    {
        return await _context.Metrics
            .Where(m => m.RepositoryId == repositoryId)
            .OrderByDescending(m => m.RecordedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<Dictionary<string, object>> GetAggregatedMetricsAsync(Guid userId)
    {
        var repos = await _context.Repositories
            .Where(r => r.UserId == userId)
            .Include(r => r.Metrics)
            .ToListAsync();

        var totalCommits = 0;
        var totalPRs = 0;
        var totalIssues = 0;
        var totalStars = 0;

        foreach (var repo in repos)
        {
            var latest = repo.Metrics.OrderByDescending(m => m.RecordedAt).FirstOrDefault();
            if (latest != null)
            {
                totalCommits += latest.CommitCount;
                totalPRs += latest.PullRequestCount;
                totalIssues += latest.IssueCount;
                totalStars += latest.StarCount;
            }
        }

        return new Dictionary<string, object>
        {
            { "totalRepositories", repos.Count },
            { "totalCommits", totalCommits },
            { "totalPullRequests", totalPRs },
            { "totalIssues", totalIssues },
            { "totalStars", totalStars }
        };
    }
}
