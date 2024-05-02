namespace BG.CampusLife.Application.Common;

public static class Configs
{
    public const string ApiUrl = "https://127.0.0.1:5001";

    public const string WebUrl = "https://127.0.0.1:4200";

    public static class ImageUrls
    {
        public static readonly string NotFoundImage = $"{ApiUrl}/Files/Static/not-found.png";
    }
}