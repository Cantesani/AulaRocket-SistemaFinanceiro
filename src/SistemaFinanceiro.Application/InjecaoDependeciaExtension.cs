using Microsoft.Extensions.DependencyInjection;
using SistemaFinanceiro.Application.UseCases.Despesas.Registrar;

namespace SistemaFinanceiro.Application
{
    public static class InjecaoDependeciaExtension
    {
        //para criacao do metodo de extensao (onde as funcoes abaixo implementam ao codigo builder.services.addInfrastructure no Program.cs)
        //necessario 3 observacoes:
        // 1 - Classe ser STATIC
        // 2 - Funcao ser STATIC
        // 3 - Colocar modificador 'THIS', como parametro

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegistrarDespesaUseCase, RegistrarDespesaUseCase>();
        }

    }
}
