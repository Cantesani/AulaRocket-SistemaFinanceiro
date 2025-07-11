using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using FluentAssertions;
using Microsoft.VisualBasic;
using SistemaFinanceiro.Application.UseCases.Users.Update;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace UseCases.Test.Users.Update
{
    public class UpdateUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Builder();

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            await act.Should().NotThrowAsync();

            user.Nome.Should().Be(request.Nome);
            user.Email.Should().Be(request.Email);
        }


        [Fact]
        public async Task Error_Name_Empty()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Builder();
            user.Nome = string.Empty;

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErroValidacaoException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NOME_USUARIO_OBRIGATORIO));
        }


        [Fact]
        public async Task Error_Email_Ja_Existe()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Builder();

            var useCase = CreateUseCase(user, request.Email);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErroValidacaoException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_JA_EXISTE));
        }


        private UpdateUserUseCase CreateUseCase(User user, string? email = null)
        {
            var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Build();
            var updateRepository = UserUpdateOnlyRepositoryBuilder.Build(user);
            var loggedUser = LoggedUserBuilder.Build(user);
            var readRepository = new UserReadOnlyRepositoryBuilder();

            if(string.IsNullOrWhiteSpace(email) == false)
            {
                readRepository.ExisteUserComEsseEmail(email);
            }

            return new UpdateUserUseCase(loggedUser, updateRepository, readRepository.Build(), unidadeDeTrabalho);


        }



    }
}
