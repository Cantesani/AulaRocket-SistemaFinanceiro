using CommonTestUtilities.Criptografia;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using CommonTestUtilities.Token;
using FluentAssertions;
using SistemaFinanceiro.Application.UseCases.Users.Registrar;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace UseCases.Test.Users.Registrar
{
    public class RegistrarUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegistrarUserJsonBuilder.Build();
            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Nome.Should().Be(request.Nome);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegistrarUserJsonBuilder.Build();
            request.Nome = string.Empty;

            var useCase = CreateUseCase();

            var act = async() => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErroValidacaoException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NOME_USUARIO_OBRIGATORIO));
        }

        [Fact]
        public async Task Error_Email_Exist()
        {
            var request = RequestRegistrarUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErroValidacaoException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_JA_EXISTE));
        }

        private RegistrarUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var passwordCriptografia = new PasswordCriptografadaBuilder().Build();
            var tokenGenerator = JwtTokenGeneratorBuilder.Build();
            var readRepository = new UserReadOnlyRepositoryBuilder();

            if (!string.IsNullOrWhiteSpace(email))
                readRepository.ExisteUserComEsseEmail(email);

            return new RegistrarUserUseCase(mapper, passwordCriptografia, readRepository.Build(), writeRepository, tokenGenerator, unidadeDeTrabalho);
        }



    }
}
