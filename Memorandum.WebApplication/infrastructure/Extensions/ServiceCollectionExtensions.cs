using Memorandum.Repository.Implement;
using Memorandum.Repository.Interfaces;
using Memorandum.Service.Implement;
using Memorandum.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Memorandum.WebApplication.infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemorandumRepository, MemorandumRepository>();
            return services;
        }
    }
}
