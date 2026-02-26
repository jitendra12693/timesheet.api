using Microsoft.Extensions.DependencyInjection;
using timesheet.data.Repository;
using timesheet.model.Interfaces;

namespace timesheet.data
{
    public static class RegisterDependency
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<ITimeSheetRepository, TimesheetRepository>();
            return services;
        }
    }
}
