using System;
using MediatR;

namespace BG.CampusLife.Application.Notifications.Commands.SetNotificationVisited
{
    public class SetNotificationVisitedCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}