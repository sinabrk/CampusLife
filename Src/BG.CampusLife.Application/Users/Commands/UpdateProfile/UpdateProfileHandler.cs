namespace BG.CampusLife.Application.Users.Commands.UpdateProfile;

public class UpdateProfileHandler : BaseHandler<UpdateProfileHandler>, IRequestHandler<UpdateProfileCommand>
{
    public UpdateProfileHandler(IMapper mapper, IRepositories repos, ICurrentUserService currentUserService) : base(mapper, repos, currentUserService)
    {}

    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        // TODO: Put this to Mapping method
        var user = new User
        {
            UserId = CurrentUserService.UserId,
            UniversityId = request.UniversityId,
            LocationId = request.LocationId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            Birthday = request.BirthDay,
            Bio = request.Bio,
            MarriageStatus = request.MarriageStatus,
            Started = request.Started,
            Graduation = request.Graduation,
            Graduated = request.Graduated,
            Private = request.Private,
            Title = request.Title,
            PersonalEmail = request.PersonalEmail,
            AdditionalEmail = request.AdditionalEmail,
            NormalizedPersonalEmail = request.PersonalEmail.ToUpper(),
            NormalizedAdditionalEmail = request.AdditionalEmail.ToUpper(),
            // Image
        };
        
        await Repositories.UserRepository.UpdateUserProfile(user, cancellationToken);
        
        return Unit.Value;
    }
}