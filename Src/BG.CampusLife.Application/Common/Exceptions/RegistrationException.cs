namespace BG.CampusLife.Application.Common.Exceptions;

public class RegistrationException : Exception
{
    public string[] Errors { get; set; }
    public RegistrationException():base("Register Failed!")
    {

    }
    public RegistrationException(string[] errors)
    {
        Errors = errors;
    }
}