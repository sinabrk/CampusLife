using AutoMapper;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Locations.Commands.DTOs;
using BG.CampusLife.Application.Locations.Queries.DTOs;
using BG.CampusLife.Application.Posts.DTOs;
using BG.CampusLife.Application.Universities.Commands.DTO;
using BG.CampusLife.Application.Universities.Queries.DTO;
using System;
using System.Linq;
using System.Reflection;

namespace BG.CampusLife.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<Domain.Entities.Post, CreateOrUpdatePostDto>().ReverseMap();
            CreateMap<Domain.Entities.University, CreateOrUpdateUniversityDto>().ReverseMap();
            CreateMap<Domain.Entities.University, UniQueriesDto>().ReverseMap();
            CreateMap<Domain.Entities.Location, CreateLocationDto>().ReverseMap();
            CreateMap<Domain.Entities.Location, GetLocationDto>().ReverseMap();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}