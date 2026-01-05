using JobPortal.API.DTOs;


public interface IJobService
{
    Task<PagedResult<JobDto>> GetJobsAsync(JobQueryParameters query);
    Task<JobDto> CreateAsync(JobCreateDto dto);
    Task<bool> UpdateAsync(int id, JobCreateDto dto);
    Task<bool> DeleteAsync(int id);
}
