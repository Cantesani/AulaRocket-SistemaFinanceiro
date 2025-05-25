using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Domain.Repositories.Users;
using SistemaFinanceiro.Domain.Security.Criptografia;
using SistemaFinanceiro.Domain.Security.Tokens;
using SistemaFinanceiro.Infrastructure.DataAccess;
using SistemaFinanceiro.Infrastructure.DataAccess.Repositories;
using SistemaFinanceiro.Infrastructure.Security.Tokens;
using System.ComponentModel;

namespace SistemaFinanceiro.Infrastructure
{
    public static class InjecaoDependeciaExtension
    {
        //para criacao do metodo de extensao (onde as funcoes abaixo implementam ao codigo builder.services.addInfrastructure no Program.cs)
        //necessario 3 observacoes:
        // 1 - Classe ser STATIC
        // 2 - Funcao ser STATIC
        // 3 - Colocar modificador 'THIS', como parametro
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            AddToken(services, configuration);

            services.AddScoped<IPasswordCriptografada, Security.Criptografia.BCrypt>();
        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signinKey = configuration.GetValue<string>("Settings:Jwt:SigninKey");

            services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signinKey!));

        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
            services.AddScoped<IDespesasReadOnlyRepository, DespesasRepository>();
            services.AddScoped<IDespesasWriteOnlyRepository, DespesasRepository>();
            services.AddScoped<IDespesaUpdateOnlyRepository, DespesasRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            var Version = new Version(8, 0, 42);
            var serverVersion = new MySqlServerVersion(Version);

            services.AddDbContext<SistemaFinanceiroDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }


    }
}
