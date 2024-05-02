namespace BG.CampusLife.Infrastructure.Repositories;

public partial class DbRepositories : IRepositories
{
    public IUserCalendarRepository UserCalendarRepository => this;
    public ICategoryRepository CategoryRepository { get; set ; }
    public IDocumentRepository DocumentRepository { get; set; }
    public IEventRepository EventRepository { get; set; }
    public ILocationRepository LocationRepository { get; set; }
    public IMarketRepository MarketRepository { get; set; }
    public INotificationRepository NotificationRepository { get; set; }
    public IPostRepository PostRepository { get; set; }
    public IPropertyRepository PropertyRepository { get; set; }
    public ITagRepository TagRepository { get; set; }
    public IUniversityRepository UniversityRepository { get; set; }
    public IUserRepository UserRepository { get; set; }
    public ICurrentUserService CurrentUserService { get; set; }
    public INotificationService NotificationService { get; set; }
    public ISignInManager SignInManagerService { get; set; }
    public IUserManager UserManagerService { get; set; }
}
