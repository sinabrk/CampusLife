using System;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Repositories;
using FluentValidation;

namespace BG.CampusLife.Application.Identity.Commands.UpdateProfile
{
    public class UpdateProfileValidation : AbstractValidator<UpdateProfileCommand>
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly ILocationRepository _locationRepository;
        public UpdateProfileValidation(IUniversityRepository universityRepository, ILocationRepository locationRepository)
        {
            _universityRepository = universityRepository;
            _locationRepository = locationRepository;
            RuleFor(c => c.UniversityId).MustAsync(UniversityExists).WithMessage("University is not valid");
            RuleFor(c => c.LocationId).MustAsync(LocationExists).WithMessage("Location is not valid");
            RuleFor(c => c.FirstName).NotEmpty().NotNull();
            RuleFor(c => c.LastName).NotEmpty().NotNull();
            RuleFor(c => c.NationalityId).MustAsync(LocationExists).WithMessage("Nationality is not valid");
            RuleFor(c => c.HomeLocationId).MustAsync(LocationExists).WithMessage("HomeLocation is not valid");
        }

        private async Task<bool> LocationExists(Guid? arg1, CancellationToken arg2)
        {
            if (arg1 == null) return false;
            var entity = await _locationRepository.GetById(arg1.Value);
            return entity != null;
        }

        private async Task<bool> UniversityExists(Guid? arg1, CancellationToken arg2)
        {
            if (arg1 == null) return false;
            var entity = await _universityRepository.GetUniversityById(arg1.Value);
            return entity != null;
        }
    }
}