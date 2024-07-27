using Business.Services;
using Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P4P_DataAccess.Repositories;

namespace P4P_Infrastructure;

public static class DependencyInjectionRegister
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .ConfigureInfrastructure(configuration);


    private static IServiceCollection ConfigureInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        try
        {
            if (configuration["RepositoryType"].ToUpper() == "HASH")
            {
                services.AddSingleton<ITaskRepository, TaskInHashRepository>();
            }
            
            if (configuration["RepositoryType"].ToUpper() == "EF")
            {
                services.AddDbContext<TaskInMemoryEfContext>(options =>
                    options.UseInMemoryDatabase("TasksDatabase"));
                services.AddScoped<ITaskRepository, TaskInMemoryEFRepository>();
            }
        }
        catch (Exception)
        {
            throw new Exception("Invalid repository type configured.");
        }

        services.AddScoped<ITaskService, TaskService>();
        return services;

    }
}
