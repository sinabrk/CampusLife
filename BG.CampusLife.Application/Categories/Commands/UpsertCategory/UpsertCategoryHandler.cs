using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Categories.DTOs;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Categories.Commands.UpsertCategory
{
    public class UpsertCategoryHandler : BaseHandler<UpsertCategoryHandler>,IRequestHandler<UpsertCategoryCommand, CategoryDto>
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;
        public UpsertCategoryHandler(ILogger<UpsertCategoryHandler> logger, IMapper mapper, ICategoryRepository categoryRepository, ICurrentUserService currentUser, IUserRepository userRepository) : base(logger, mapper)
        {
            _categoryRepository = categoryRepository;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public async Task<CategoryDto> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);

            var result = await _categoryRepository.Upsert(new Category()
            {
                Id = request.Id,
                Title = request.Title,
                CategoryType = request.CategoryType,
                Level = request.Level,
                ParentId = request.ParentId,
                Code = request.Code,
                Slug = request.Slug,
                Status = request.Status,
                CreatedById = user.Entity.Id,
            });
            
            if (!result.Succeeded)
                throw new NotFoundException(result.Message);

            return Mapper.Map<CategoryDto>(result.Entity);
        }
    }
}