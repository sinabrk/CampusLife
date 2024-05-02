using BG.CampusLife.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using BG.CampusLife.Application.Posts.DTOs;

namespace BG.CampusLife.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest<CreateOrUpdatePostDto>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }

        public List<Document> Attachments { get; set; }

        public Guid UserId { get; set; }
    }
}
