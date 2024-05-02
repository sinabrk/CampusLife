using System;
using BG.CampusLife.Application.Categories.DTOs;
using MediatR;

namespace BG.CampusLife.Application.Categories.Queries.GetById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public Guid Id { get; set; }
    }
}