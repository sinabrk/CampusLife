using System;
using System.Linq;
using AutoMapper;
using BG.CampusLife.Application.Common.Mappings;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Properties.Commands.UpsertProperty
{
    public class UpsertPropertyDto : IMapFrom<Property>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public PropertyControlTypes ControlType { get; set; }
        public string Name { get; set; }
        public string Options { get; set; }
        public bool Required { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, UpsertPropertyDto>()
                .ForMember(cld => cld.CategoryName, opt => opt.MapFrom(c => c.Category.Title))
                .ForMember(cld => cld.ControlType, opt => opt.MapFrom(item => item.ControlType.ToString()))
                .ForMember(cld => cld.Options,
                    opt =>
                        opt.MapFrom(c =>
                            c.Options.Split("^", StringSplitOptions.RemoveEmptyEntries)
                                .Select(i => i.Trim()).ToList()));
        }
    }
}