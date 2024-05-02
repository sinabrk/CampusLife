using System;
using System.Collections.Generic;
using AutoMapper;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Notifications.Queries.GetUserNotifications
{
    public class UserNotificationDto : IMapFrom<Notification>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Visited { get; set; }
        public string SendDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, UserNotificationDto>()
                .ForMember(i => i.SendDate,
                    o =>
                        o.MapFrom(n => n.SendDate.ToString("g")));
        }
    }
}