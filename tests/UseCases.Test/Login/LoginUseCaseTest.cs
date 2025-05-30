using CommonTestUtilities.Criptografia;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using CommonTestUtilities.Token;
using FluentAssertions;
using Microsoft.Extensions.Primitives;
using SistemaFinanceiro.Application.UseCases.Login;
using SistemaFinanceiro.Communication.Requests.Login;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace UseCases.Test.Login
{
    public class LoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var request = RequestLoginJsonBuilder.Build();
            request.Email = user.Email;

            var useCase = CreateUseCase(user, request.Password);
            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Nome.Should().Be(user.Nome);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_User_NaoExiste()
        {
            var user = UserBuilder.Build();
            var request = RequestLoginJsonBuilder.Build();

            var useCase = CreateUseCase(user, request.Password);
            var act = async () => await useCase.Execute(request);
            var result = await act.Should().ThrowAsync<LoginInvalidoException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS));
        }

        [Fact]
        public async Task Error_Password_not_Match()
        {
            var user = UserBuilder.Build();
            var request = RequestLoginJsonBuilder.Build();
            request.Email = user.Email;

            var useCase = CreateUseCase(user);
            var act = async () => await useCase.Execute(request);
            var result = await act.Should().ThrowAsync<LoginInvalidoException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS));
        }


        private LoginUseCase CreateUseCase(User user, string? password = null)
        {
            var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
            var passwordCriptografia = new PasswordCriptografadaBuilder().VerificaSenha(password).Build();
            var tokenGenerator = JwtTokenGeneratorBuilder.Build();

            return new LoginUseCase(readRepository, passwordCriptografia, tokenGenerator);
        }
    }
}
