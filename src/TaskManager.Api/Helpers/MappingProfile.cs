using AutoMapper;
using TaskManager.Api.Models;
using TaskManager.Api.DTOs;

namespace TaskManager.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Task, TaskDto>().ReverseMap();
        }
    }
}