namespace BG.CampusLife.Persistence;

public class CampusSeed
{
    // todo It should Use identity context for production remove the seed.
    public static void SeedAsync(CampusContext context, IdentityDbContext identity)
    {
        try
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(GetPreconfiguredUsers(identity));
                context.SaveChanges();
            }

            var user = context.Users.FirstOrDefault(e => e.Email == "Test@Email.com");

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(GetPreconfiguredCategories(user));
                context.SaveChanges();
            }
            
            if (!context.Locations.Any())
            {
                context.Locations.AddRange(GetPreconfiguredLocations());
                context.SaveChanges();
            }
            
            if (!context.MarketItems.Any())
            {
                context.MarketItems.AddRange(GetPreconfiguredMarketItems(context.Locations.ToList(),
                    context.Categories.ToList(), user));
                context.SaveChanges();
            }
            
            if (!context.Properties.Any())
            {
                context.Properties.AddRange(GetPreconfiguredProperties(context.Categories.ToList()));
                context.SaveChanges();
            }

            if (!context.MarketItemProperties.Any())
            {
                context.MarketItemProperties.AddRange(GetPreconfiguredPropertyRelations(context.MarketItems.ToList(),
                    context.Properties.ToList()));
                context.SaveChanges();
            }
            
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(GetPreconfiguredPosts(context.Locations.ToList(),
                    context.Categories.ToList(), user));
                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                context.Tags.AddRange(GetPreconfiguredTags(user));
                context.SaveChanges();
            }

            if (!context.Universities.Any())
            {
                context.Universities.AddRange(GetPreconfiguredUniversities(context.Locations.ToList()));
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    /// <summary>
    /// names from https://github.com/moby/moby/blob/master/pkg/namesgenerator/names-generator.go
    /// </summary>
    /// <returns></returns>
    private static string GetRandomName()
    {
        var left = new[]
        {
            "admiring",
            "adoring",
            "affectionate",
            "agitated",
            "amazing",
            "angry",
            "awesome",
            "beautiful",
            "blissful",
            "bold",
            "boring",
            "brave",
            "busy",
            "charming",
            "clever",
            "cool",
            "compassionate",
            "competent",
            "condescending",
            "confident",
            "cranky",
            "crazy",
            "dazzling",
            "determined",
            "distracted",
            "dreamy",
            "eager",
            "ecstatic",
            "elastic",
            "elated",
            "elegant",
            "eloquent",
            "epic",
            "exciting",
            "fervent",
            "festive",
            "flamboyant",
            "focused",
            "friendly",
            "frosty",
            "funny",
            "gallant",
            "gifted",
            "goofy",
            "gracious",
            "great",
            "happy",
            "hardcore",
            "heuristic",
            "hopeful",
            "hungry",
            "infallible",
            "inspiring",
            "interesting",
            "intelligent",
            "jolly",
            "jovial",
            "keen",
            "kind",
            "laughing",
            "loving",
            "lucid",
            "magical",
            "mystifying",
            "modest",
            "musing",
            "naughty",
            "nervous",
            "nice",
            "nifty",
            "nostalgic",
            "objective",
            "optimistic",
            "peaceful",
            "pedantic",
            "pensive",
            "practical",
            "priceless",
            "quirky",
            "quizzical",
            "recursing",
            "relaxed",
            "reverent",
            "romantic",
            "sad",
            "serene",
            "sharp",
            "silly",
            "sleepy",
            "stoic",
            "strange",
            "stupefied",
            "suspicious",
            "sweet",
            "tender",
            "thirsty",
            "trusting",
            "unruffled",
            "upbeat",
            "vibrant",
            "vigilant",
            "vigorous",
            "wizardly",
            "wonderful",
            "xenodochial",
            "youthful",
            "zealous",
            "zen",
        };

        var right = new[]
        {
            "agnesi",
            "albattani",
            "allen",
            "almeida",
            "antonelli",
            "archimedes",
            "ardinghelli",
            "aryabhata",
            "austin",
            "babbage",
            "banach",
            "banzai",
            "bardeen",
            "bartik",
            "bassi",
            "beaver",
            "bell",
            "benz",
            "bhabha",
            "bhaskara",
            "black",
            "blackburn",
            "blackwell",
            "bohr",
            "booth",
            "borg",
            "bose",
            "bouman",
            "boyd",
            "brahmagupta",
            "brattain",
            "brown",
            "buck",
            "burnell",
            "cannon",
            "carson",
            "cartwright",
            "carver",
            "cerf",
            "chandrasekhar",
            "chaplygin",
            "chatelet",
            "chatterjee",
            "chaum",
            "chebyshev",
            "clarke",
            "cohen",
            "colden",
            "cori",
            "cray",
            "curran",
            "curie",
            "darwin",
            "davinci",
            "dewdney",
            "dhawan",
            "diffie",
            "dijkstra",
            "dirac",
            "driscoll",
            "dubinsky",
            "easley",
            "edison",
            "einstein",
            "elbakyan",
            "elgamal",
            "elion",
            "ellis",
            "engelbart",
            "euclid",
            "euler",
            "faraday",
            "feistel",
            "fermat",
            "fermi",
            "feynman",
            "franklin",
            "gagarin",
            "galileo",
            "galois",
            "ganguly",
            "gates",
            "gauss",
            "germain",
            "goldberg",
            "goldstine",
            "goldwasser",
            "golick",
            "goodall",
            "gould",
            "greider",
            "grothendieck",
            "haibt",
            "hamilton",
            "haslett",
            "hawking",
            "hellman",
            "heisenberg",
            "hermann",
            "herschel",
            "hertz",
            "heyrovsky",
            "hodgkin",
            "hofstadter",
            "hoover",
            "hopper",
            "hugle",
            "hypatia",
            "ishizaka",
            "jackson",
            "jang",
            "jemison",
            "jennings",
            "jepsen",
            "johnson",
            "joliot",
            "jones",
            "kalam",
            "kapitsa",
            "kare",
            "keldysh",
            "keller",
            "kepler",
            "khayyam",
            "khorana",
            "kilby",
            "kirch",
            "knuth",
            "kowalevski",
            "lalande",
            "lamarr",
            "lamport",
            "leakey",
            "leavitt",
            "lederberg",
            "lehmann",
            "lewin",
            "lichterman",
            "liskov",
            "lovelace",
            "lumiere",
            "mahavira",
            "margulis",
            "matsumoto",
            "maxwell",
            "mayer",
            "mccarthy",
            "mcclintock",
            "mclaren",
            "mclean",
            "mcnulty",
            "mendel",
            "mendeleev",
            "meitner",
            "meninsky",
            "merkle",
            "mestorf",
            "mirzakhani",
            "montalcini",
            "moore",
            "morse",
            "murdock",
            "moser",
            "napier",
            "nash",
            "neumann",
            "newton",
            "nightingale",
            "nobel",
            "noether",
            "northcutt",
            "noyce",
            "panini",
            "pare",
            "pascal",
            "pasteur",
            "payne",
            "perlman",
            "pike",
            "poincare",
            "poitras",
            "proskuriakova",
            "ptolemy",
            "raman",
            "ramanujan",
            "ride",
            "ritchie",
            "rhodes",
            "robinson",
            "roentgen",
            "rosalind",
            "rubin",
            "saha",
            "sammet",
            "sanderson",
            "satoshi",
            "shamir",
            "shannon",
            "shaw",
            "shirley",
            "shockley",
            "shtern",
            "sinoussi",
            "snyder",
            "solomon",
            "spence",
            "stonebraker",
            "sutherland",
            "swanson",
            "swartz",
            "swirles",
            "taussig",
            "tereshkova",
            "tesla",
            "tharp",
            "thompson",
            "torvalds",
            "tu",
            "turing",
            "varahamihira",
            "vaughan",
            "villani",
            "visvesvaraya",
            "volhard",
            "wescoff",
            "wilbur",
            "wiles",
            "williams",
            "williamson",
            "wilson",
            "wing",
            "wozniak",
            "wright",
            "wu",
            "yalow",
            "yonath",
            "zhukovsky",
        };
        var random = new Random();
        return left[random.Next(left.Length)] + " " + right[random.Next(right.Length)];
    }

    private static IEnumerable<User> GetPreconfiguredUsers(IdentityDbContext context)
    {
        return
            new List<User>()
            {
                new()
                {
                    UserId = context.Users.FirstOrDefault(e => e.Email == "Test@Email.com")?.Id,
                    UniversityId = null,
                    LocationId = null,
                    Gender = GenderType.Male,
                    Bio = "Test-Bio",
                    FirstName = "Test",
                    LastName = "Email",
                    Email = "Test@Email.com",
                    NormalizedEmail = "Test@Email.com".ToUpper(),
                },
                new()
                {
                    UserId = context.Users.FirstOrDefault(e => e.Email == "User@Email.com")?.Id,
                    UniversityId = null,
                    LocationId = null,
                    Gender = GenderType.Female,
                    Bio = "Test-Bio",
                    FirstName = "User",
                    LastName = "Email",
                    Email = "User@Email.com",
                    NormalizedEmail = "User@Email.com".ToUpper(),
                },
            };
    }

    private static IEnumerable<Location> GetPreconfiguredLocations()
    {
        var random = new Random();

        var locations = new List<Location>();
        for (var i = 0; i < 10; i++)
            locations.Add(
                new Location
                {
                    Title = GetRandomName(),
                    Level = 1,
                    Longitude = random.NextDouble(),
                    Latitude = random.NextDouble(),
                    Status = true,
                });
        return locations;
    }

    private static IEnumerable<Category> GetPreconfiguredCategories(User user)
    {
        var random = new Random();
        var categories = new List<Category>();
        for (var i = 0; i < 10; i++)
        {
            var name = GetRandomName();
            categories.Add(new Category
            {
                Title = name,
                CategoryType = (CategoryTypes)4,
                Level = 1,
                Code = name.Replace(" ", random.Next(100).ToString()),
                Slug = name.Replace(" ", "-"),
                CreatedBy = user.UserId,
            });
        }

        return categories;
    }

    private static IEnumerable<Property> GetPreconfiguredProperties(IReadOnlyList<Category> categories)
    {
        var random = new Random();
        var properties = new List<Property>();
        for (var i = 0; i < 10; i++)
        {
            properties.Add(new Property
            {
                CategoryId = categories[random.Next(categories.Count)].Id,
                ControlType = PropertyControlTypes.TextBox,
                Name = GetRandomName(),
                Options = $"{GetRandomName()}|{GetRandomName()}|{GetRandomName()}",
                Required = random.Next() > (int.MaxValue / 2),
            });
        }

        return properties;
    }

    private static IEnumerable<MarketItem> GetPreconfiguredMarketItems(IReadOnlyList<Location> locations,
        IReadOnlyList<Category> categories, User user)
    {
        var random = new Random();
        var items = new List<MarketItem>();
        for (var i = 0; i < 10; i++)
        {
            items.Add(new MarketItem
            {
                Title = GetRandomName(),
                Description = GetRandomName(),
                UserId = user.Id,
                CategoryId = categories[random.Next(categories.Count)].Id,
                LocationId = locations[random.Next(locations.Count)].Id,
                Status = MarketItemStatuses.Approved,
            });
        }

        return items;
    }

    private static IEnumerable<MarketItemProperty> GetPreconfiguredPropertyRelations(
        IReadOnlyList<MarketItem> marketItems,
        IReadOnlyList<Property> properties)
    {
        var random = new Random();
        var items = new List<MarketItemProperty>();
        for (var i = 0; i < 10; i++)
        {
            items.Add(new MarketItemProperty
            {
                MarketItemId = marketItems[random.Next(marketItems.Count)].Id,
                PropertyId = properties[random.Next(properties.Count)].Id,
                Value = GetRandomName()
            });
        }

        return items;
    }

    private static IEnumerable<Tag> GetPreconfiguredTags(User user)
    {
        var tags = new List<Tag>();
        for (var i = 0; i < 10; i++)
            tags.Add(new Tag
            {
                Created = DateTime.Now,
                Title = GetRandomName(),
                UserId = user.Id,
            });
        return tags;
    }
    
    private static IEnumerable<University> GetPreconfiguredUniversities(IReadOnlyList<Location> locations)
    {
        var random = new Random();
        var items = new List<University>();
        for (var i = 0; i < 10; i++)
        {
            items.Add(new University
            {
                Name = GetRandomName() + ' ' + GetRandomName(),
                LocationId = locations[random.Next(locations.Count)].Id,
                Status = true,
            });
        }

        return items;
    }
    
    private static IEnumerable<Post> GetPreconfiguredPosts(IReadOnlyList<Location> locations,
        IReadOnlyList<Category> categories, User user)
    {
        var random = new Random();
        var items = new List<Post>();
        for (var i = 0; i < 10; i++)
        {
            items.Add(new Post
            {
                Title = GetRandomName(),
                Body = GetRandomName() + ' ' + GetRandomName() + ' ' + GetRandomName() + ' ' + GetRandomName(),
                UserId = user.Id,
                CategoryId = categories[random.Next(categories.Count)].Id,
                LocationId = locations[random.Next(locations.Count)].Id,
                Status = PostStatus.Approved,
            });
        }

        return items;
    }

}