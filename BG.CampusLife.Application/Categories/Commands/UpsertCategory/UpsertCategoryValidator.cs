using System;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Repositories;
using FluentValidation;

namespace BG.CampusLife.Application.Categories.Commands.UpsertCategory
{
    public class UpsertCategoryValidator : AbstractValidator<UpsertCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpsertCategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Level).GreaterThan((byte)0).LessThanOrEqualTo((byte)3);
            RuleFor(c => c.ParentId).MustAsync(IsParentExists).WithMessage("ParentId is Invalid");
        }
        
        private async Task<bool> IsParentExists(Guid? parentId, CancellationToken cancellationToken)
        {
            if (!parentId.HasValue)
                return true;
            
            return await _categoryRepository.IsCategoryExists(parentId.Value);
        }
    }
}