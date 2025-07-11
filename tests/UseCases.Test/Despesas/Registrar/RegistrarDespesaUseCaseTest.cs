using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using FluentAssertions;
using SistemaFinanceiro.Application.UseCases.Despesas.Registrar;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace UseCases.Test.Despesas.Registrar
{
    public class RegistrarDespesaUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var request = RequestRegistrarDespesaJsonBuilder.Builder();
            var useCase = CreateUseCase(loggedUser);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(request.Titulo);
        }

        [Fact]
        public async Task Error_Titulo_Empty()
        {
            var loggedUser = UserBuilder.Build();
            var request = RequestRegistrarDespesaJsonBuilder.Builder();
            request.Titulo = string.Empty;

            var useCase = CreateUseCase(loggedUser);

            var act = async() => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErroValidacaoException>();
            result.Where(x => x.GetErrors().Count == 1 && x.GetErrors().Contains(ResourceErrorMessages.TITULO_OBRIGATORIO));
        }

        private RegistrarDespesaUseCase CreateUseCase(User user)
        {
            var repository = DespesasWriteOnlyRepositoryBuilder.Build();
            var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);


            return new RegistrarDespesaUseCase(repository, unidadeDeTrabalho, mapper, loggedUser);
        }
    }
}
