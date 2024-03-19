using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ProjectsTask.Models;

namespace ProjectsTask
{
    public class Startup
    {
        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();
            services.AddTransient<ITempDataProvider, CookieTempDataProvider>();
            services.AddDbContext<ProjectManagmentContext>();
            services.AddControllers();
            services.AddScoped<ProjectRepository>();
            services.AddScoped<EmployeeRepository>();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "projects view",
                    pattern: "{controller=Projects}/{action=ProjectsView}/{id?}");
                endpoints.MapControllerRoute(
                    name: "employees view",
                    pattern: "{controller=Employees}/{action=EmployeesView}/{id?}");
                endpoints.MapControllerRoute(
                    name: "create project view",
                    pattern: "{controller=Projects}/{action=CreateProject}/{id?}");
                endpoints.MapControllerRoute(
                    name: "add employee view",
                    pattern: "{controller=Employees}/{action=AddEmployee}/{id?}");
            });
        }
    }
}
