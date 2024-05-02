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

namespace BG.CampusLife.Application.Categories.Queries.GetById
{
    public class GetCategoryByIdHandler : BaseHandler<GetCategoryByIdHandler>, IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdHandler(ILogger<GetCategoryByIdHandler> logger, IMapper mapper, ICategoryRepository categoryRepository) : base(logger, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetById(request.Id);
            if (!result.Succeeded)
                throw new NotFoundException(result.Message);
            return Mapper.Map<CategoryDto>(result.Entity);
        }
    }
}