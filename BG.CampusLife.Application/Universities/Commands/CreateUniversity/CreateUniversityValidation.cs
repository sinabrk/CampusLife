using BG.CampusLife.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Linq;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Universities.Commands.CreateUniversity
{
    public class CreateUniversityValidation : AbstractValidator<CreateUniversityCommand>
    {
        private readonly IUniversityRepository _repo;
        public CreateUniversityValidation(IUniversityRepository repo)
        {
            _repo = repo;
        }

        public CreateUniversityValidation()
        {
            RuleFor(uni => uni.Location).NotEmpty().WithMessage("Please provide the location!");
            RuleFor(uni => uni.Location).Must(CheckAddress).WithMessage("Please provide the full address: Street, Door Number, Postal Code, State, City, Country");
            RuleFor(uni => uni.Name).NotEmpty().WithMessage("Name can not be empty");
        }

        private static bool CheckAddress(Domain.Entities.Location location)
        {
            return location != null && location.GetType().GetProperties().All(item => item.Name is not ("Country" or "City") || item.GetValue(location) != null);
        }

        public bool PossibleDuplicate(string city, string country, string name)
        {
            var uni = _repo.GetUniversityByName(name);
            return uni.Result.Entity.Location != null && uni.Result.Entity.Location.City == city && uni.Result.Entity.Location.Country == country;
        }
    }
}
