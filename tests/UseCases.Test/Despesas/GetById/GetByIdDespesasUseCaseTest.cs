using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using SistemaFinanceiro.Application.UseCases.Despesas.GetByID;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace UseCases.Test.Despesas.GetById
{
    public class GetByIdDespesasUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var despesa = DespesaBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, despesa);

            var result = await useCase.execute(despesa.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(despesa.Id);
            result.Titulo.Should().Be(despesa.Titulo);
            result.Descricao.Should().Be(despesa.Descricao);
            result.Data.Should().Be(despesa.Data);
            result.Valor.Should().Be(despesa.Valor);
            result.TipoPagto.Should().Be((SistemaFinanceiro.Communication.Enums.TipoPagto)despesa.TipoPagto);
        }


        [Fact]
        public async Task Error_Despesa_Nao_Existe()
        {
            var loggedUser = UserBuilder.Build();
            var useCase = CreateUseCase(loggedUser);
            var act = async () => await useCase.execute(id: 1000);

            var result = await act.Should().ThrowAsync<NaoExisteException>();

            result.Where(x => x.GetErrors().Count == 1 && x.GetErrors().Contains(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA));
        }



        private GetByIdDespesaUseCase CreateUseCase(User user, Despesa? despesa = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new DespesasReadOnlyRepositoryBuilder().GetById(user, despesa).Build();
            var mapper = MapperBuilder.Build();

            return new GetByIdDespesaUseCase(loggedUser, repository, mapper);
        } 
    }
}
