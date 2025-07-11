using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using SistemaFinanceiro.Application.UseCases.Despesas.GetAll;
using SistemaFinanceiro.Domain.Entities;

namespace UseCases.Test.Despesas.GetAll
{
    public class GetAllDespesaUseCaseTest
    {
        [Fact]
        public async Task Success() 
        {
            var loggedUser = UserBuilder.Build();
            var despesas = DespesaBuilder.Collection(loggedUser);

            var useCase = CreateUseCase(loggedUser, despesas);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Despesas.Should().NotBeNullOrEmpty().And.AllSatisfy(x =>
            {
                x.Id.Should().BeGreaterThan(0);
                x.Titulo.Should().NotBeNullOrEmpty();
                x.Valor.Should().BeGreaterThan(0);
            });
        }

        private GetAllDespesasUseCase CreateUseCase(User user, List<Despesa> despesas)
        {
            var repository = new DespesasReadOnlyRepositoryBuilder().GetAll(user, despesas).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GetAllDespesasUseCase(loggedUser, repository, mapper);
        }
    }


}
