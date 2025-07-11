using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using SistemaFinanceiro.Application.UseCases.Despesas.Delete;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;
using Xunit.Sdk;

namespace UseCases.Test.Despesas.Delete
{
    public class DeleteDespesaUseCaseTest
    {
        //[Fact]
        //public async Task Success()
        //{
        //    var loggedUser = UserBuilder.Build();
        //    var despesa = DespesaBuilder.Build(loggedUser);

        //    var useCase = CreateUseCase(loggedUser, despesa);
        //    var act = async () => await useCase.Execute(despesa.Id);

        //    await act.Should().NotThrowAsync();
        //}

        //[Fact]
        //public async Task Error_Despesas_Not_Found()
        //{
        //    var loggedUser = UserBuilder.Build();
        //    var useCase = CreateUseCase(loggedUser);

        //    var act = async () => await useCase.Execute(id: 1000);

        //    var result = await act.Should().ThrowAsync<NaoExisteException>();

        //    result.Where(x => x.GetErrors().Count == 1 && x.GetErrors().Contains(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA));

        //}

        //private DeleteDespesaUseCase CreateUseCase(User user, Despesa? despesa = null)
        //{
        //    var repositoryWriteOnly = DespesasWriteOnlyRepositoryBuilder.Build();
        //    var repository = new DespesasReadOnlyRepositoryBuilder().GetById(user, despesa).Build();
        //    var unitOfWork = UnidadeDeTrabalhoBuilder.Build();
        //    var loggedUser = LoggedUserBuilder.Build(user);

        //    return new DeleteDespesaUseCase(repositoryWriteOnly, repository, unitOfWork, loggedUser);



        //}
    }
}
