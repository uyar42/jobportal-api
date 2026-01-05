using AutoMapper;
using JobPortal.API.DTOs;
using JobPortal.API.Entities;

public class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, JobDto>();
        CreateMap<JobCreateDto, Job>();
    }
}
