
namespace BG.CampusLife.Infrastructure.Identity;

public class IdentityDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) :
        base(options, operationalStoreOptions)
    {
    }
}