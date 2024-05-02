using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Universities.Commands.DTO;
using Uni = BG.CampusLife.Domain.Entities.University;

namespace BG.CampusLife.Application.Universities.Commands.CreateUniversity
{
    public class CreateUniversityHandler : BaseHandler<CreateUniversityHandler>,
        IRequestHandler<CreateUniversityCommand, CreateOrUpdateUniversityDto>
    {
        private IUniversityRepository _repo;
        private CreateUniversityValidation _validation;

        public CreateUniversityHandler(IUniversityRepository repo, IMapper mapper,
            ILogger<CreateUniversityHandler> logger) : base(logger, mapper)
        {
            _repo = repo;
            _validation = new(_repo);
        }

        public async Task<CreateOrUpdateUniversityDto> Handle(CreateUniversityCommand request,
            CancellationToken cancellationToken)
        {
            if (_validation.PossibleDuplicate(request.Location.City, request.Location.Country, request.Name))
                throw new DuplicateException(
                    $"University with the name {request.Name} at the same location ({request.Location.Country}, {request.Location.City} already exist in system.");
            
            var result = await _repo.Create(new Uni()
            {
                Id = request.Id,
                Name = request.Name,
                Location = request.Location
            });
            
            return Mapper.Map<CreateOrUpdateUniversityDto>(result.Entity);
        }
    }
}