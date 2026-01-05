using JobPortal.API.Common;
using JobPortal.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] JobQueryParameters query)
    {
        var result = await _jobService.GetJobsAsync(query);
        return Ok(ApiResponse<PagedResult<JobDto>>.Ok(result));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(JobCreateDto dto)
    {
        var job = await _jobService.CreateAsync(dto);
        return Ok(ApiResponse<JobDto>.Ok(job, "Job created"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, JobCreateDto dto)
    {
        var success = await _jobService.UpdateAsync(id, dto);
        if (!success)
            return NotFound(ApiResponse<string>.Fail("Job not found"));

        return Ok(ApiResponse<string>.Ok(null, "Job updated"));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _jobService.DeleteAsync(id);
        if (!success)
            return NotFound(ApiResponse<string>.Fail("Job not found"));

        return Ok(ApiResponse<string>.Ok(null, "Job deleted"));
    }
}
