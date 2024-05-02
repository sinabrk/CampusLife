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

namespace BG.CampusLife.Application.Categories.Queries.GetByCode
{
    public class GetCategoryByCodeHandler : BaseHandler<GetCategoryByCodeHandler>, IRequestHandler<GetCategoryByCodeQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByCodeHandler(ILogger<GetCategoryByCodeHandler> logger, IMapper mapper, ICategoryRepository categoryRepository) : base(logger, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetByCode(request.Code);
            if (!result.Succeeded)
                throw new NotFoundException(result.Message);
            return Mapper.Map<CategoryDto>(result.Entity);
        }
    }
}