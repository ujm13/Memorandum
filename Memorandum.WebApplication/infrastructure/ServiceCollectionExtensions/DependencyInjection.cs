using Memorandum.Repository.Implement;
using Memorandum.Repository.Interfaces;
using Memorandum.Service.Implement;
using Memorandum.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Memorandum.WebApplication.infrastructure.ServiceCollectionExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services) 
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberService, MemberService>();
            return services;
        }
    }
}
