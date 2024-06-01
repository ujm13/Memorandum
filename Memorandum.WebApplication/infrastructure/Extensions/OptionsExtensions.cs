using Memorandum.Common.Options;

namespace Memorandum.WebApplication.infrastructure.Extensions
{
    /// <summary>
    /// OptionsExtensions
    /// </summary>
    public static class OptionsExtensions
    {
        /// <summary>
        /// AddOptionsDependency
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
