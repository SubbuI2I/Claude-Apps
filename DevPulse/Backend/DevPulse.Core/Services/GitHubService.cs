using Octokit;
using Microsoft.Extensions.Configuration;

namespace DevPulse.Core.Services;

public interface IGitHubService
{
    Task<Repository?> GetRepositoryAsync(string owner, string repo);
}

public class GitHubService : IGitHubService
{
    private readonly GitHubClient _client;

    public GitHubService(IConfiguration configuration)
    {
        var token = configuration["GitHub:Token"];
        _client = new GitHubClient(new ProductHeaderValue("DevPulse"))
        {
            Credentials = new Credentials(token)
        };
    }

    public async Task<Repository?> GetRepositoryAsync(string owner, string repo)
    {
        try
        {
            return await _client.Repository.Get(owner, repo);
        }
        catch
        {
            return null;
        }
    }
}
