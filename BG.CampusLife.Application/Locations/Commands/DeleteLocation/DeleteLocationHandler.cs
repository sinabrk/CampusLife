using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Locations.Commands.DeleteLocation
{
    public class DeleteLocationHandler : BaseHandler<DeleteLocationHandler>, IRequestHandler<DeleteLocationCommand>
    {
        private readonly ILocationRepository _repo;

        public DeleteLocationHandler(ILogger<DeleteLocationHandler> logger, IMapper mapper, ILocationRepository repo) :
            base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Delete(request.Id);
            if (result.Succeeded) return Unit.Value;
            Logger.LogWarning(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}