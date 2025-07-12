using Microsoft.Extensions.DependencyInjection;
using SistemaFinanceiro.Application.AutoMapper;
using SistemaFinanceiro.Application.UseCases.Despesas.Delete;
using SistemaFinanceiro.Application.UseCases.Despesas.GetAll;
using SistemaFinanceiro.Application.UseCases.Despesas.GetByID;
using SistemaFinanceiro.Application.UseCases.Despesas.Registrar;
using SistemaFinanceiro.Application.UseCases.Despesas.Reports.Excel;
using SistemaFinanceiro.Application.UseCases.Despesas.Reports.Pdf;
using SistemaFinanceiro.Application.UseCases.Despesas.Update;
using SistemaFinanceiro.Application.UseCases.Login;
using SistemaFinanceiro.Application.UseCases.Users.ChangePassword;
using SistemaFinanceiro.Application.UseCases.Users.Delete;
using SistemaFinanceiro.Application.UseCases.Users.Profile;
using SistemaFinanceiro.Application.UseCases.Users.Registrar;
using SistemaFinanceiro.Application.UseCases.Users.Update;

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
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegistrarDespesaUseCase, RegistrarDespesaUseCase>();
            services.AddScoped<IGetAllDespesasUseCase, GetAllDespesasUseCase>();
            services.AddScoped<IGetByIdDespesaUseCase, GetByIdDespesaUseCase>();
            services.AddScoped<IDeleteDespesaUseCase, DeleteDespesaUseCase>();
            services.AddScoped<IUpdateDespesaUseCase, UpdateDespesaUseCase>();
            services.AddScoped<IGerarDespesasReportExcelUseCase, GerarDespesasReportExcelUseCase>();
            services.AddScoped<IGerarDespesasReportPdfUseCase, GerarDespesasReportPdfUseCase>();
            services.AddScoped<IRegistraUserUseCase, RegistrarUserUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();

        }

    }
}
