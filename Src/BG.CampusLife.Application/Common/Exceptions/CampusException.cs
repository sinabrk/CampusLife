namespace BG.CampusLife.Application.Common.Exceptions;

public class CampusException : Exception
{
    public int Code { get; set; }
    public string Error { get; set; }

    public CampusException(string error, int code)
    {
        Error = error;
        Code = code;
    }
}