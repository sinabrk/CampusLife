using MediatR;
using System;

namespace BG.CampusLife.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
