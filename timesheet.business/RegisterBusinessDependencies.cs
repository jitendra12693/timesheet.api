using Microsoft.Extensions.DependencyInjection;
using timesheet.business.Interfaces;
using timesheet.business.Services;

namespace timesheet.business
{
    public static class RegisterBusinessDependencies
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITimesheetService, TimesheetServices>();
            return services;
        }
    }
}
