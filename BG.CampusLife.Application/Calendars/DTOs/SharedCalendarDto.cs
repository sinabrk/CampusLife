using System;
using AutoMapper;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Calendars.DTOs
{
    public class SharedCalendarDto : IMapFrom<SharedCalendar>
    {
        // Maybe add user details
        public Guid Id { get; set; }
        public string SharedUserId { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedCalendar, SharedCalendarDto>().ReverseMap();
        }
    }
}