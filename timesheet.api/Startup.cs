using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
