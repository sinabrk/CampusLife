using System.Linq;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Posts.Commands.DeletePost
{
    public class DeletePostHandler : BaseHandler<DeletePostHandler>, IRequestHandler<DeletePostCommand>
    {
        private IPostRepository _repo;

        public DeletePostHandler(ILogger<DeletePostHandler> logger, IPostRepository repo) : base(logger, null)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Delete(request.Id);
            if (result.Succeeded) return Unit.Value;
            Logger.LogError(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}
