namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : IRepositories
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityDbContext _identityContext;
    public readonly ICampusContext _context;
    public readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IEmailSender _emailSender;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public DbRepositories
        (SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IEmailSender emailSender, IHubContext<NotificationHub> hubContext, IWebHostEnvironment webHostEnvironment, ICampusContext context, IdentityDbContext identityContext, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _configuration = configuration;
        _roleManager = roleManager;
        _emailSender = emailSender;
        _hubContext = hubContext;
        _webHostEnvironment = webHostEnvironment;
        _context = context;
        _identityContext = identityContext;
        _userManager = userManager;
    }

    public IUserCalendarRepository UserCalendarRepository => this;
    public ICategoryRepository CategoryRepository => this;
    public IDocumentRepository DocumentRepository => this;
    public IEventRepository EventRepository => this;
    public ILocationRepository LocationRepository => this;
    public IMarketRepository MarketRepository => this;
    public INotificationRepository NotificationRepository => this;
    public IPostRepository PostRepository => this;
    public IPropertyRepository PropertyRepository => this;
    public ITagRepository TagRepository => this;
    public IUniversityRepository UniversityRepository => this;
    public IUserRepository UserRepository => this;

    //Services
    public INotificationService NotificationServices => this;
    public ISignInManager SignInManagerServices => this;
    public IUserManager UserManagerServices => this;

    public async Task<T> GetEntityById<T>(Guid id) where T : BaseEntity
    {
        return await _context.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}