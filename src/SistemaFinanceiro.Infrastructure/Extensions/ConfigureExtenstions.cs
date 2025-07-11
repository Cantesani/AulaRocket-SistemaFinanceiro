using Microsoft.Extensions.Configuration;

namespace SistemaFinanceiro.Infrastructure.Extensions
{
    public static class ConfigureExtenstions
    {
        public static bool IsTestEnvironment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }
    }
}
