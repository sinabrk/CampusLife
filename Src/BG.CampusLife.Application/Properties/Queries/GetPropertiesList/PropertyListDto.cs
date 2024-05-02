using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BG.CampusLife.Application.Common.Mappings;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Properties.Queries.GetPropertiesList
{
    public class PropertyListDto : IMapFrom<Property>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ControlType { get; set; }
        public List<string> Options { get; set; }
        public bool Required { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, PropertyListDto>()
                .ForMember(cld => cld.Options,
                    opt =>
                        opt.MapFrom(c =>
                            c.Options.Split("^", StringSplitOptions.RemoveEmptyEntries)
                                .Select(i => i.Trim()).ToList()))
                .ForMember(cld => cld.ControlType,
                    opt=> opt.MapFrom(c => c.ControlType.ToString()));
        }
    }
}