using AutoMapper;
using JobPortal.API.Common;
using JobPortal.API.Data;
using JobPortal.API.DTOs;
using JobPortal.API.Entities;
using Microsoft.EntityFrameworkCore;

public class JobService : IJobService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public JobService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<JobDto>> GetJobsAsync(JobQueryParameters query)
    {
        var jobsQuery = _context.Jobs.AsQueryable();

        // FILTERING
        if (!string.IsNullOrWhiteSpace(query.Title))
            jobsQuery = jobsQuery.Where(j => j.Title.Contains(query.Title));

        if (!string.IsNullOrWhiteSpace(query.Company))
            jobsQuery = jobsQuery.Where(j => j.Company.Contains(query.Company));

        // SORTING
        jobsQuery = query.SortBy switch
        {
            "salary" => query.Desc
                ? jobsQuery.OrderByDescending(j => j.Salary)
                : jobsQuery.OrderBy(j => j.Salary),

            _ => query.Desc
                ? jobsQuery.OrderByDescending(j => j.CreatedAt)
                : jobsQuery.OrderBy(j => j.CreatedAt)
        };

        var totalCount = await jobsQuery.CountAsync();

        var jobs = await jobsQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        // 🔥 AUTOMAPPER BURADA
        var jobDtos = _mapper.Map<List<JobDto>>(jobs);

        return new PagedResult<JobDto>
        {
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize,
            Items = jobDtos
        };
    }

    public async Task<JobDto> CreateAsync(JobCreateDto dto)
    {
        // 🔥 DTO → ENTITY
        var job = _mapper.Map<Job>(dto);

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        // 🔥 ENTITY → DTO
        return _mapper.Map<JobDto>(job);
    }

    public async Task<bool> UpdateAsync(int id, JobCreateDto dto)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job == null) return false;

        // 🔥 DTO → EXISTING ENTITY
        _mapper.Map(dto, job);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job == null) return false;

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();
        return true;
    }
}
