using System;
using MediatR;
using System.Collections.Generic;
using BG.CampusLife.Application.Posts.DTOs;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Posts.Queries.AllUserPosts
{
    public class GetAllUserPostsQuery : IRequest<List<Post>>
    {
    }
}
