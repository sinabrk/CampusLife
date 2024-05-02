namespace BG.CampusLife.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity '{name}' with the id ({key}) was not found.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

}