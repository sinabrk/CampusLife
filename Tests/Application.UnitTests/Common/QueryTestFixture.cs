using AutoMapper;
using BG.CampusLife.Application.Common.Mappings;
using BG.CampusLife.Persistence;
using Xunit;

namespace Application.UnitTests.Common
{
    public class QueryTestFixture
    {
        public QueryTestFixture()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
            Context = CampusContextFactory.Create();
        }
        public IMapper Mapper { get; set; }
        public CampusContext Context { get; set; }
        
        public void Dispose()
        {
            CampusContextFactory.Destroy(Context);
        }
    }
    
    
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}