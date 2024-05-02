namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IUserRepository
{
    public async Task<Result<User>> GetUserById(string userId)
    {
        var result = new Result<User>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<User>().Where(c => c.UserId == userId).FirstOrDefaultAsync(),
        };

        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.Message = MessageHelper.ErrorNotFound(nameof(User), userId);
        result.StatusCode = ResultStatusCodes.NotFound;

        return result;
    }

    public async Task<Result<User>> GetUserByEmail(string email)
    {
        var result = new Result<User>()
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = await _context.Set<User>().FirstOrDefaultAsync(c => c.NormalizedEmail == email.ToUpper()),
        };

        if (result.Entity is not null) return result;

        result.Succeeded = false;
        result.Message = MessageHelper.ErrorNotFound("User", email);
        result.StatusCode = ResultStatusCodes.NotFound;

        return result;
    }

    public async Task<Result<User>> UpdateUserProfile(User user, CancellationToken cancellationToken)
    {
        var result = new Result<User>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
        };

        var entity = await _context.Set<User>()
            // .Include(u => u.Image)
            .Where(u => u.UserId == user.UserId).FirstOrDefaultAsync(cancellationToken);

        entity.FirstName = user.FirstName;
        entity.Bio = user.Bio;
        entity.LastName = user.LastName;
        entity.Gender = user.Gender;
        entity.Birthday = user.Birthday;
        entity.Private = user.Private;
        entity.Bio = user.Bio;
        entity.MarriageStatus = user.MarriageStatus;
        // entity.Image = user.Image;
        entity.UniversityId = user.UniversityId;
        entity.LocationId = user.LocationId;
        entity.Started = user.Started;
        entity.Graduation = user.Graduation;
        entity.Graduated = user.Graduated;
        entity.Title = user.Title;
        entity.PersonalEmail = user.PersonalEmail;
        entity.NormalizedPersonalEmail = user.PersonalEmail.ToUpper();
        entity.AdditionalEmail = user.AdditionalEmail;
        entity.NormalizedAdditionalEmail = user.AdditionalEmail.ToUpper();

        await _context.SaveChangesAsync(cancellationToken);
        result.Entity = user;

        return result;
    }

    public async Task<Result<User>> GetUserProfile(string userId, string role)
    {
        var result = new Result<User>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Created,
            Entity = await _context.Set<User>()
                .Where(u => u.UserId == userId)
                .Select(t => new User()
                {
                    UniversityId = t.UniversityId,
                    University = t.University,
                    LocationId = t.LocationId,
                    Location = t.Location,
                    Private = t.Private,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Gender = t.Gender,
                    Birthday = t.Birthday,
                    Bio = t.Bio,
                    MarriageStatus = t.MarriageStatus,
                    Started = (role.Contains(Roles.Student.ToString())) ? t.Started : null,
                    Graduation = (role.Contains(Roles.Student.ToString())) ? t.Graduation : null,
                    Graduated = (role.Contains(Roles.Student.ToString())) ? t.Graduated : null,
                    Title = (role.Contains(Roles.Faculty.ToString())) ? t.Title : null,
                    PersonalEmail = (role.Contains(Roles.Faculty.ToString())) ? t.PersonalEmail : null,
                    AdditionalEmail = (role.Contains(Roles.Explorer.ToString())) ? t.AdditionalEmail : null,
                    ImageId = t.ImageId,
                    Image = t.Image,
                }).FirstOrDefaultAsync()
        };

        return result;
    }

    public async Task<Result<User>> CreateUser(User user, CancellationToken cancellationToken)
    {
        _context.Set<User>().Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<User>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entity = user
        };
    }

    public async Task<Result<User>> GetUserList(List<UserData> users,
        Guid locationId, Guid universityId)
    {
        var entities = 
            (
                await _context.Set<User>()
                .Where(item =>
                    (locationId == Guid.Empty || item.LocationId == locationId) &&
                    (universityId == Guid.Empty || item.UniversityId == universityId)
                )
                .AsNoTracking()
                .ToListAsync()
            )
            .Join(users,
                campusUser => campusUser.UserId,
                applicationUser => applicationUser.Id,
                (campusUser, applicationUser) => new User
                {
                    UserId = applicationUser.Id,
                    Id = campusUser.Id,
                    Email = applicationUser.Email
                }).ToList();

        return new Result<User>
        {
            Succeeded = true,
            StatusCode = ResultStatusCodes.Successful,
            Entities = entities,
            Total = entities.Count,
        };
    }
}