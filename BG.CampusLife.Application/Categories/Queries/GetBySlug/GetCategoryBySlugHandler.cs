using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Categories.DTOs;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Categories.Queries.GetBySlug
{
    public class GetCategoryBySlugHandler : BaseHandler<GetCategoryBySlugHandler>, IRequestHandler<GetCategoryBySlugQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryBySlugHandler(ILogger<GetCategoryBySlugHandler> logger, IMapper mapper, ICategoryRepository categoryRepository) : base(logger, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetBySlug(request.Slug);
            if (!result.Succeeded)
                throw new NotFoundException(result.Message);
            return Mapper.Map<CategoryDto>(result.Entity);
        }
    }
}