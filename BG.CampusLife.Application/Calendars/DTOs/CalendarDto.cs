using System;
using AutoMapper;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Calendars.DTOs
{
    public class CalendarDto : IMapFrom<Calendar>
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid EntityId { get; set; }
        
        public string Notes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Calendar, CalendarDto>().ReverseMap();
        }
    }
}