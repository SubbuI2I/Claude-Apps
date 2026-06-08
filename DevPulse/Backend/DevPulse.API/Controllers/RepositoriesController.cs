using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevPulse.Core.Services;
using System.Security.Claims;

namespace DevPulse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RepositoriesController : ControllerBase
{
    private readonly IRepositoryService _repositoryService;
    private readonly IGitHubService _gitHubService;

    public RepositoriesController(IRepositoryService repositoryService, IGitHubService gitHubService)
    {
        _repositoryService = repositoryService;
        _gitHubService = gitHubService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRepositories()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var repos = await _repositoryService.GetUserRepositoriesAsync(userId);
        return Ok(repos);
    }

    [HttpPost("connect")]
    public async Task<IActionResult> ConnectRepository([FromBody] ConnectRepositoryRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Verify repository exists on GitHub
        var ghRepo = await _gitHubService.GetRepositoryAsync(request.Owner, request.Name);
        if (ghRepo == null)
            return NotFound("Repository not found on GitHub");

        var repo = await _repositoryService.ConnectRepositoryAsync(
            userId,
            request.Owner,
            request.Name,
            ghRepo.HtmlUrl,
            ghRepo.Id
        );

        return Ok(new { message = "Repository connected successfully", repository = repo });
    }

    [HttpGet("{repoId}")]
    public async Task<IActionResult> GetRepository(Guid repoId)
    {
        var repo = await _repositoryService.GetRepositoryAsync(repoId);
        if (repo == null)
            return NotFound();

        return Ok(repo);
    }

    [HttpDelete("{repoId}")]
    public async Task<IActionResult> DisconnectRepository(Guid repoId)
    {
        var success = await _repositoryService.DisconnectRepositoryAsync(repoId);
        if (!success)
            return NotFound();

        return Ok(new { message = "Repository disconnected successfully" });
    }
}

public class ConnectRepositoryRequest
{
    public string Owner { get; set; } = null!;
    public string Name { get; set; } = null!;
}
