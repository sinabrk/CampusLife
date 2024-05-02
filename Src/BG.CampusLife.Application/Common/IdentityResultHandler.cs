namespace BG.CampusLife.Application.Common;

public class IdentityResultHandler
{
    internal IdentityResultHandler(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }

    public static IdentityResultHandler Success()
    {
        return new IdentityResultHandler(true, System.Array.Empty<string>());
    }

    public static IdentityResultHandler Failure(IEnumerable<string> errors)
    {
        return new IdentityResultHandler(false, errors);
    }

}