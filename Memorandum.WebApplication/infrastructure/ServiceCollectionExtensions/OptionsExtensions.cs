using Memorandum.Common.Options;

namespace Memorandum.WebApplication.infrastructure.ServiceCollectionExtensions
{
    public static class OptionsExtensions
    {

        public static IServiceCollection AddOptionsDependency(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddOptions<DbConnectionOptions>().BindConfiguration("DbConnection");
            return serviceCollection;
        }
    }
}
