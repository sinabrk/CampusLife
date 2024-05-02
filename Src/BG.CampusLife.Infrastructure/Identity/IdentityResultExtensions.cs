namespace BG.CampusLife.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static IdentityResultHandler ToResult(this IdentityResult result)
    {
        return result.Succeeded
            ? IdentityResultHandler.Success()
            : IdentityResultHandler.Failure(result.Errors.Select(e => e.Description));
    }
}