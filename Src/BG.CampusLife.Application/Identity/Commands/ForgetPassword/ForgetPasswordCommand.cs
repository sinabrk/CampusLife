namespace BG.CampusLife.Application.Identity.Commands.ForgetPassword;

public class ForgetPasswordCommand : IRequest
{
    public string UserName { get; set; }
}