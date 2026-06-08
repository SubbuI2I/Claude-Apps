using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevPulse.Core.Services;
using System.Security.Claims;

namespace DevPulse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MetricsController : ControllerBase
{
    private readonly IMetricsService _metricsService;

    public MetricsController(IMetricsService metricsService)
    {
        _metricsService = metricsService;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardMetrics()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var metrics = await _metricsService.GetAggregatedMetricsAsync(userId);
        return Ok(metrics);
    }

    [HttpGet("{repoId}")]
    public async Task<IActionResult> GetRepositoryMetrics(Guid repoId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var fromDate = from ?? DateTime.UtcNow.AddDays(-30);
        var toDate = to ?? DateTime.UtcNow;

        var metrics = await _metricsService.GetRepositoryMetricsAsync(repoId, fromDate, toDate);
        return Ok(metrics);
    }

    [HttpGet("{repoId}/latest")]
    public async Task<IActionResult> GetLatestMetric(Guid repoId)
    {
        var metric = await _metricsService.GetLatestMetricAsync(repoId);
        if (metric == null)
            return NotFound();

        return Ok(metric);
    }

    [HttpPost("{repoId}")]
    public async Task<IActionResult> RecordMetrics(Guid repoId, [FromBody] RecordMetricsRequest request)
    {
        var metric = await _metricsService.RecordMetricsAsync(
            repoId,
            request.CommitCount,
            request.PullRequestCount,
            request.IssueCount,
            request.StarCount,
            request.ForksCount,
            request.AveragePRMergeTimeHours
        );

        return Ok(new { message = "Metrics recorded successfully", metric });
    }
}

public class RecordMetricsRequest
{
    public int CommitCount { get; set; }
    public int PullRequestCount { get; set; }
    public int IssueCount { get; set; }
    public int StarCount { get; set; }
    public int ForksCount { get; set; }
    public double? AveragePRMergeTimeHours { get; set; }
}
