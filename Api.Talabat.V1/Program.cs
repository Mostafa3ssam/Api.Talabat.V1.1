using Api.Talabat.V1.Error;
using Api.Talabat.V1.Extentions;
using Api.Talabat.V1.Helper;
using Api.Talabat.V1.MiddleWare;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.Core.Entity.Identity;
using Talabat.Core.Interfaces;
using Talabat.Core.Interfaces.Services;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Api.Talabat.V1
{
    public class Program
    {
        public static async Task Main(string[] args)

        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
                var Connetion = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(Connetion);
            });
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentity<AppUser, IdentityRole>(). // Kda ana 3mlt add le interfaces ale ana 3yzhom
                AddEntityFrameworkStores<AppIdentityDbContext>();// kda ana 3mlt imp lehom el param dah ek data base bt3tiii bt3t identity 
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

                };
            }); // kda ana 3mlt Register le UserManger w Role Manger w Signin manger 

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                                            var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                                                             .SelectMany(P => P.Value.Errors)
                                                                                             .Select(E => E.ErrorMessage)
                                                                                             .ToList();
                    var ValidationResponseErrors = new ApiVaildationResponse()
                    {

                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationResponseErrors);
                };
            });

            var app = builder.Build();

            // Kda ana mask kol el services ale sh8ala scope
           using var Services = app.Services.CreateScope();

            var LoggerFactory = Services.ServiceProvider.GetService<ILoggerFactory>();
            //mskt al service bt3t db

            try
            {
                var Dbcontext = Services.ServiceProvider.GetRequiredService<StoreContext>();

                await Dbcontext.Database.MigrateAsync();
                var IdentityDbcontext = Services.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbcontext.Database.MigrateAsync();
                var  userManger =   Services.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                await AppIdentityDbContextSeed.SeedUserAsync(userManger);
                await StoreContextSeed.SeedAsync(Dbcontext);
            }
            catch (Exception ex)
            {

                var Logger = LoggerFactory?.CreateLogger<Program>();
                Logger?.LogError(ex, "An Error Occured During Appling The Migration");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization(); 
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
