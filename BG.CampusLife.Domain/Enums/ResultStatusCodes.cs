namespace BG.CampusLife.Domain.Enums
{
    public enum ResultStatusCodes
    {
        Nothing = 100,
        Successful = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        UnAuthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalError = 500,
        NotImplemented = 501,
    }
}