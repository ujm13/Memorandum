using Memorandum.Repository.Implement;
using Memorandum.Repository.Interfaces;
using Memorandum.Service.Implement;
using Memorandum.Service.infrastructure.Helpers;
using Memorandum.Service.Interfaces;

namespace Memorandum.WebApplication.infrastructure.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// AddDependencyInjection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemorandumRepository, MemorandumRepository>();
            services.AddScoped<IMemorandumService, MemorandumService>();
            services.AddScoped<IEncryptHelper, Sha256EncryptHelper>();
            
            return services;
        }
    }
}
