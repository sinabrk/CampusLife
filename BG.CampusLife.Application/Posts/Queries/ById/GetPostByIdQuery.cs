using MediatR;
using System;
using BG.CampusLife.Application.Posts.DTOs;

namespace BG.CampusLife.Application.Posts.Queries.GetPost.ById
{
    public class GetPostByIdQuery : IRequest<GetPostDto>
    {
        public Guid Id { get; set; }
    }
}
