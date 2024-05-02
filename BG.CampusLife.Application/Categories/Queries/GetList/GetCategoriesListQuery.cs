using System;
using System.Collections.Generic;
using BG.CampusLife.Application.Categories.DTOs;
using BG.CampusLife.Domain.Enums;
using MediatR;

namespace BG.CampusLife.Application.Categories.Queries.GetList
{
    public class GetCategoriesListQuery : IRequest<List<CategoryDto>>
    {
        public int Level { get; set; }
        public CategoryTypes CategoryType { get; set; }
        public Guid? ParentId { get; set; }

        public bool Status { get; set; }
    }
}