using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaFinanceiro.Domain.Security.Criptografia;
using SistemaFinanceiro.Domain.Security.Tokens;
using SistemaFinanceiro.Infrastructure.DataAccess;
using WebApi.test.Resource;

namespace WebApi.test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public UserIdentityMananger User_Team_Member { get; set; } = default!;
        public UserIdentityMananger User_Admin { get; set; } = default!;
        
        public DespesaIdentityMananger Despesa_Team_Member { get; set; } = default!;


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<SistemaFinanceiroDbContext>(config =>
                    {
                        config.UseInMemoryDatabase("InMemoryDbForTesting");
                        config.UseInternalServiceProvider(provider);
                    });

                    var scope = services.BuildServiceProvider().CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<SistemaFinanceiroDbContext>();
                    var passwordCript = scope.ServiceProvider.GetRequiredService<IPasswordCriptografada>();
                    var acessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                    StartDatabase(dbContext, passwordCript, acessTokenGenerator);
                });
        }

        private void StartDatabase(SistemaFinanceiroDbContext dbContext,
            IPasswordCriptografada passwordCriptografada,
            IAccessTokenGenerator acessTokenGenerator)
        {
            var userTeamMember = AddUsersTeamMember(dbContext, passwordCriptografada, acessTokenGenerator);
            // var despesaTeamMember = AddDespesas(dbContext, userTeamMember, despesaId: 1, tagId: 1);
            
            
            
            dbContext.SaveChanges();
        }

        private SistemaFinanceiro.Domain.Entities.User AddUsersTeamMember(SistemaFinanceiroDbContext dbContext,
            IPasswordCriptografada passwordCriptografada,
            IAccessTokenGenerator accessTokenGenerator)
        {
            var user = UserBuilder.Build();
            var password = user.Password;

            user.Password = passwordCriptografada.Criptografar(user.Password);

            dbContext.Users.Add(user);

            var token = accessTokenGenerator.Generate(user);

            User_Team_Member = new UserIdentityMananger(user, password, token);

            return user;
        }

        // private AddDespesas(SistemaFinanceiroDbContext dbContext, SistemaFinanceiro.Domain.Entities.User user,
        //     long despesaId, long tagId)
        // {
        //     var despesa = DespesaBuilder.Build(user);
        //     dbContext.Despesas.Add(despesa);
        //
        //     foreach (var tag in despesa.Tags)
        //     {
        //         tag.Id = tagId;
        //         tag.DespesaId = despesaId;
        //         ;
        //     }
        //
        //     Despesa_Team_Member = new DespesaIdentityMananger(despesa);
        // }
    }
}