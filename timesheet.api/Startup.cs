using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using timesheet.api.Middleware;
using timesheet.business;
using timesheet.data;
//using Microsoft.AspNetCore.swa

namespace timesheet.api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            services.AddSwaggerGen();
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", options =>
                {
                    var key = Encoding.UTF8.GetBytes(Configuration["Authentication:Key"]!);
                    var issuer = Configuration["Authentication:Issuer"];
                    var audience = Configuration["Authentication:Audience"];
                    var expirationMinutes = int.Parse(Configuration["Authentication:DurationInMinutes"]!);
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        //ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,
                    };

                    options.Authority = Configuration["Authentication:Authority"];
                    options.Audience = Configuration["Authentication:Audience"];
                });

            services.AddDbContext<TimesheetDb>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TimesheetDbConnection")));

            services.AddData();
            services.AddBusiness();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<GlobalExceptionHandler>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                endpoints.MapControllers();
            });
        }
    }
}
