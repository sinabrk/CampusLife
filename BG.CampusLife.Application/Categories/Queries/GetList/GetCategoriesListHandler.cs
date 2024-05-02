using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Categories.DTOs;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Categories.Queries.GetList
{
    public class GetCategoriesListHandler : BaseHandler<GetCategoriesListHandler>, IRequestHandler<GetCategoriesListQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesListHandler(ILogger<GetCategoriesListHandler> logger, IMapper mapper, ICategoryRepository categoryRepository) : base(logger, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetList(request.Level, request.CategoryType, request.ParentId, request.Status);
            return Mapper.Map<List<CategoryDto>>(result.Entities);
        }
    }
}