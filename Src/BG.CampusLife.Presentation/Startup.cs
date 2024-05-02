
namespace BG.CampusLife.Presentation;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        WebHostEnvironment = webHostEnvironment;
    }

    public IConfiguration Configuration { get; }
    public readonly IWebHostEnvironment WebHostEnvironment;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructure(Configuration);
        services.AddPersistence(Configuration);
        services.AddApplication();
        services.AddHttpContextAccessor();

        services.AddCors();
        
        services.AddScoped<ICurrentUserService, CurrentUserService>();


        services.AddControllers()
            .AddNewtonsoftJson(options => { options.SerializerSettings.Formatting = Formatting.Indented; })
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ICampusContext>());

        services.AddSignalR().AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.WriteIndented = true;
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CampusLife", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    System.Array.Empty<string>()
                }
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BG.CampusLife.Presentation v1"));
        }

        app.UseReDoc(c =>
        {
            c.DocumentTitle = "CampusLife API";
            c.SpecUrl = "/swagger/v1/swagger.json";
        });

        
        app.UseCors(option =>
        {
            option.AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });

        app.UseCustomExceptionHandler();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseStaticFiles();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<NotificationHub>("/notification");
        });
    }
}