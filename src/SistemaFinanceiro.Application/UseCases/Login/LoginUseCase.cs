using DocumentFormat.OpenXml.Wordprocessing;
using SistemaFinanceiro.Communication.Requests.Login;
using SistemaFinanceiro.Communication.Responses.Users;
using SistemaFinanceiro.Domain.Repositories.Users;
using SistemaFinanceiro.Domain.Security.Criptografia;
using SistemaFinanceiro.Domain.Security.Tokens;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordCriptografada _passwordCriptografada;
        private readonly IAccessTokenGenerator _tokenGenerator;


        public LoginUseCase(IUserReadOnlyRepository repository,
                            IPasswordCriptografada passwordCriptografada,
                            IAccessTokenGenerator tokenGenerator)
        {
            _repository = repository;
            _passwordCriptografada = passwordCriptografada;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<ResponseRegistraUserJson> Execute(RequestLoginJson request)
        {
            var user = await _repository.GetUserByEmail(request.Email);

            if (user is null)
                throw new LoginInvalidoException();

            var passwordMath = _passwordCriptografada.VerificaSenha(request.Password, user.Password);

            if (!passwordMath)
                throw new LoginInvalidoException();

            return new ResponseRegistraUserJson
            {
                Nome = user.Nome,
                Token = _tokenGenerator.Generate(user)
            };
        }
    }
}
