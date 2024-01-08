using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using rpglms.Auth;
using rpglms.src.auth;
using rpglms.src.data;
using rpglms.src.models;
using rpglms.src.shared;
using System.Text;

namespace rpglms
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            string postgresDb = Configuration["POSTGRES_DB"] ?? string.Empty;
            string postgresUser = Configuration["POSTGRES_USER"] ?? string.Empty;
            string postgresPassword = Configuration["POSTGRES_PASSWORD"] ?? string.Empty;
            string jwtSecret = Configuration["JWT_SECRET"] ?? string.Empty;
            string[] corsOrigins = Configuration["CORS_ORIGINS"]?.Split(',') ?? [];

            if (string.IsNullOrEmpty(postgresDb) || string.IsNullOrEmpty(postgresUser) || string.IsNullOrEmpty(postgresPassword))
            {
                throw new Exception("Invalid PostgreSQL configuration.");
            }

            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new Exception("Invalid JWT configuration.");
            }

            if (corsOrigins == null || !corsOrigins.Any())
            {
                throw new Exception("Invalid CORS configuration.");
            }

            // Add DbContext using SQL Server Provider
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql($"Host=db;Database={postgresDb};Username={postgresUser};Password={postgresPassword}"));

            // Auth Services
            services.AddScoped<AuthServices>();

            // JWT Authentication
            services.AddSingleton(new JwtConfig { Secret = jwtSecret });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(corsOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddTransient<IEmailSender>(provider => new EmailSender(
               Configuration["SMTP_SERVER"] ?? string.Empty,
               int.Parse(Configuration["SMTP_PORT"] ?? "0"),
               Configuration["SMTP_USER"] ?? string.Empty,
               Configuration["SMTP_PASS"] ?? string.Empty
            ));
            // And in the Configure method:
            services.AddIdentity<AppUser, IdentityRole>(options =>
             {
                 // User settings
                 options.User.RequireUniqueEmail = true;
                 // Sign-in settings
                 options.SignIn.RequireConfirmedAccount = true; // Depending on your application
                 // Token life-span
                 options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                 options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                 // Password settings
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireNonAlphanumeric = true;
                 options.Password.RequireUppercase = true;
                 options.Password.RequiredLength = 8;
                 options.Password.RequiredUniqueChars = 4;
                 // Lockout settings
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                 options.Lockout.MaxFailedAccessAttempts = 5;
                 options.Lockout.AllowedForNewUsers = true;
             })
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();
            // Add AutoMapper
            MapperConfiguration? mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            // Add other services like MVC, Razor Pages, API Controllers, etc.
            services.AddControllers(); // Or AddRazorPages(), AddControllers(), etc.
            // Additional configurations...
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts(); // HTTP Strict Transport Security
            }
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandler>();
            // Routing, static files, and other middleware configurations
            app.UseCors("AllowSpecificOrigins");
            app.UseRouting();

            app.UseAuthentication(); // Before UseAuthorization
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui
            app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root
        });

            app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Or MapRazorPages(), etc.
        });
            // Additional middleware configurations...
        }
    }
}