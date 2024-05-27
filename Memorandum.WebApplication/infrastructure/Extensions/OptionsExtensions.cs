using Memorandum.Common.Options;

namespace Memorandum.WebApplication.infrastructure.Extensions
{
    public static class OptionsExtensions
    {

        public static IServiceCollection AddOptionsDependency(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddOptions<DbConnectionOptions>()
            .Configure(o =>
            {
                var connection = Environment.GetEnvironmentVariable("Db_Connection");
                if (connection is null)
                {
                    throw new ArgumentException("Env Db_Connection is null");
                }
                o.Member = connection;
            });
             //.BindConfiguration("DbConnection");
            return serviceCollection;
        }
    }
}
