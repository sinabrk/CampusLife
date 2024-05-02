using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Universities.Commands.DeleteUniversity
{
    public class DeleteUniversityHandler : BaseHandler<DeleteUniversityHandler>,
        IRequestHandler<DeleteUniversityByIdCommand>
    {
        private IUniversityRepository _repo;

        public DeleteUniversityHandler(IUniversityRepository repo, ILogger<DeleteUniversityHandler> logger) : base(
            logger, null)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteUniversityByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Delete(request.Id);
            if (result.Succeeded) return Unit.Value;
            Logger.LogError(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}