namespace BG.CampusLife.Application.Interfaces;

public interface IRepositories
{
    IUserCalendarRepository UserCalendarRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IDocumentRepository DocumentRepository { get; }
    IEventRepository EventRepository { get; }
    ILocationRepository LocationRepository { get; }
    IMarketRepository MarketRepository { get; }
    INotificationRepository NotificationRepository { get; }
    IPostRepository PostRepository { get; }
    IPropertyRepository PropertyRepository { get; }
    ITagRepository TagRepository { get; }
    IUniversityRepository UniversityRepository { get; }
    IUserRepository UserRepository { get; }

    // Services
    INotificationService NotificationServices { get; }
    ISignInManager SignInManagerServices { get; }
    IUserManager UserManagerServices { get; }

    Task<T> GetEntityById<T>(Guid id) where T : BaseEntity;
}
