using System;
using BG.CampusLife.Application.Identity.DTOs;
using MediatR;

namespace BG.CampusLife.Application.Identity.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<UserDto>
    {
        public Guid? UniversityId { get; set; }
        public Guid? LocationId { get; set; }
        public bool Private { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Bio { get; set; }
        public Guid? NationalityId { get; set; }
        public Guid? HomeLocationId { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Graduation { get; set; }
        public bool Graduated { get; set; }
        public string Title { get; set; }
        public string PersonalEmail { get; set; }
        public string AdditionalEmail { get; set; }
    }
}