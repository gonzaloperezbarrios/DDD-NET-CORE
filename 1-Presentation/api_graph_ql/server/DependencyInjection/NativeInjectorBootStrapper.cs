namespace server
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using DoaminService=Application.application.services.car;
    using ApplicationService=Domain.domain.services.car;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Issues.Services;
    using Issues.Schemas;
    using GraphQL;
    using GraphQL.Server.Transports.AspNetCore;
    using GraphQL.Server.Transports.WebSockets;

    public class NativeInjectorBootStrapper
    {
        public void RegisterServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            //services.AddSingleton(configuration);
            // GraphQL
            services.AddSingleton<IIssueService, IssueService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IssueType>();
            services.AddSingleton<UserType>();
            services.AddSingleton<CarType>();
            services.AddSingleton<IssueStatusesEnum>();
            services.AddSingleton<IssuesQuery>();
            services.AddSingleton<IssuesSchema>();
            services.AddSingleton<IDependencyResolver>(c => new FuncDependencyResolver(type => c.GetRequiredService(type)));
            services.AddGraphQLHttp();
            services.AddGraphQLWebSocket<IssuesSchema>();
            // DDD
            // Application
            services.AddSingleton<Issues.Services.ICarService, Issues.Services.CarService>();
            //services.AddScoped<DoaminService.ICarService, DoaminService.CarService>();
            // Domain
            //services.AddScoped<ApplicationService.ICarService, ApplicationService.CarService>();
            // Infrastructure
            //services.AddScoped<Domain.repositories.contracts.car.ICarRepository, Infrastructure.repositories.car.CarRepository>();
            // Context Infrastructure
            //services.AddDbContext<CarContext>(options => options.UseSqlServer(configuration.GetConnectionString("CarConnection")));
        }
    }
}