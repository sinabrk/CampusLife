using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : BaseHandler<DeleteCategoryHandler>, IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public DeleteCategoryHandler(ILogger<DeleteCategoryHandler> logger, IMapper mapper, ICategoryRepository categoryRepository) : base(logger, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.Delete(request.Id);
            if (!result.Succeeded)
                throw new NotFoundException(result.Message);
            return Unit.Value;
        }
    }
}