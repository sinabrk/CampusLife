namespace BG.CampusLife.Application.Identity.Commands.ConfirmEmailToken;

public class ConfirmEmailTokenCommand : IRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
}