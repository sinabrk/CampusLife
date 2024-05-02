using System.Reflection.Metadata.Ecma335;

namespace BG.CampusLife.Application.Common;

public static class MessageHelper
{
    public static string ErrorNotFound(string entity, string key) => $"{entity} with {key} not found";
    public static string CustomMessage(string message) => message;

    public static string UserPresenter(User user) => $"{user.FirstName} {user.LastName}";
}