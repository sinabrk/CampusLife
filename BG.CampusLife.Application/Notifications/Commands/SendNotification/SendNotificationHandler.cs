using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Notifications.Commands.SendNotification
{
    public class SendNotificationHandler : BaseHandler<SendNotificationHandler>, IRequestHandler<SendNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserManager _userManager;
        public SendNotificationHandler(ILogger<SendNotificationHandler> logger, IMapper mapper, INotificationRepository notificationRepository, INotificationService notificationService, IUserManager userManager) : base(logger, mapper)
        {
            _notificationRepository = notificationRepository;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var users = new List<UserData>();
            var roles = await _userManager.GetRoles();
            foreach (var role in roles.Entities)
            {
                var userEntities = await _userManager.GetUsers(role);
                users.AddRange(userEntities.Entities);
            }

            var time = DateTime.Now;
            var notifications = users.Select(user => new Notification()
                {
                    Title = request.Title,
                    Body = request.Body,
                    SendDate = time,
                    Visited = false,
                    // Todo Fix
                    // UserId = user.Id,
                })
                .ToList();

            await _notificationRepository.BulkCreate(notifications);
            
            await _notificationService.SendNotificationToAll(new Notification()
            {
                Title = request.Title,
                Body = request.Body,
                SendDate = time,
                Visited = false,
            });
            return Unit.Value;
        }
    }
}