using Microsoft.Extensions.DependencyInjection;
using timesheet.data.Repository;
using timesheet.model.Interfaces;

namespace timesheet.data
{
    public static class RegisterDependency
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ITimeSheetRepository, TimesheetRepository>();
            return services;
        }
    }
}
