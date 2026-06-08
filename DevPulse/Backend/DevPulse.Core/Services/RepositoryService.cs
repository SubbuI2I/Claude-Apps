using DevPulse.Data;
using DevPulse.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DevPulse.Core.Services;

public interface IRepositoryService
{
    Task<Repository?> ConnectRepositoryAsync(Guid userId, string owner, string repoName, string repoUrl, long gitHubId);
    Task<List<Repository>> GetUserRepositoriesAsync(Guid userId);
    Task<Repository?> GetRepositoryAsync(Guid repoId);
    Task<bool> DisconnectRepositoryAsync(Guid repoId);
}

public class RepositoryService : IRepositoryService
{
    private readonly DevPulseContext _context;

    public RepositoryService(DevPulseContext context)
    {
        _context = context;
    }

    public async Task<Repository?> ConnectRepositoryAsync(Guid userId, string owner, string repoName, string repoUrl, long gitHubId)
    {
        var existingRepo = await _context.Repositories
            .FirstOrDefaultAsync(r => r.UserId == userId && r.GitHubId == gitHubId);

        if (existingRepo != null)
            return existingRepo;

        var repository = new Repository
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Owner = owner,
            Name = repoName,
            RepositoryUrl = repoUrl,
            GitHubId = gitHubId,
            ConnectedAt = DateTime.UtcNow,
            LastSyncedAt = DateTime.UtcNow
        };

        _context.Repositories.Add(repository);
        await _context.SaveChangesAsync();

        return repository;
    }

    public async Task<List<Repository>> GetUserRepositoriesAsync(Guid userId)
    {
        return await _context.Repositories
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.LastSyncedAt)
            .ToListAsync();
    }

    public async Task<Repository?> GetRepositoryAsync(Guid repoId)
    {
        return await _context.Repositories
            .Include(r => r.Metrics)
            .FirstOrDefaultAsync(r => r.Id == repoId);
    }

    public async Task<bool> DisconnectRepositoryAsync(Guid repoId)
    {
        var repo = await _context.Repositories.FindAsync(repoId);
        if (repo == null)
            return false;

        _context.Repositories.Remove(repo);
        await _context.SaveChangesAsync();
        return true;
    }
}
