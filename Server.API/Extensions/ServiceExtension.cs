
using Server.Core.Interfaces.IRepositories;
using Server.Core.Interfaces.IServices;
using Server.Core.Services;
using Server.Infrastructure.Repositories;

namespace Server.API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            #region Services
            //services.AddSingleton<IUserContext, UserContext>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IScopingService, ScopingService>();

            #endregion

            #region Repositories
            services.AddTransient<ICaseRepository, CaseRepository>();

            #endregion

            return services;
        }
    }
}
