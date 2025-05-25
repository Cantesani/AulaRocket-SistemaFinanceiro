using AutoMapper;
using FluentValidation.Results;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Communication.Responses.Users;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Users;
using SistemaFinanceiro.Domain.Security.Criptografia;
using SistemaFinanceiro.Domain.Security.Tokens;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Users.Registrar
{
    public class RegistrarUserUseCase: IRegistraUserUseCase
    {
        private readonly IMapper _imapper;
        private readonly IPasswordCriptografada _passwordCriptografada;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IAccessTokenGenerator _tokenGenerator;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

        public RegistrarUserUseCase(IMapper imapper
                                   ,IPasswordCriptografada passwordCriptografada
                                   ,IUserReadOnlyRepository userReadOnlyRepository
                                   ,IUserWriteOnlyRepository userWriteOnlyRepository
                                   ,IAccessTokenGenerator tokenGenerator
                                   , IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            _imapper = imapper;
            _passwordCriptografada = passwordCriptografada;
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _tokenGenerator = tokenGenerator;
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }

        public async Task<ResponseRegistraUserJson> Execute(RequestRegistraUserJson request)
        {
            await Validate(request);

            var user = _imapper.Map<User>(request);
            user.Password = _passwordCriptografada.Criptografar(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _userWriteOnlyRepository.Add(user);
            await _unidadeDeTrabalho.Commit();

            return new ResponseRegistraUserJson
            {
                Nome = user.Nome,
                Token = _tokenGenerator.Generate(user)
            };
        }


        public async Task Validate(RequestRegistraUserJson request)
        {
            var result = new UserValidator().Validate(request);
            var existeEmail =  await _userReadOnlyRepository.ExisteUserComEsseEmail(request.Email);

            if (existeEmail)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_JA_EXISTE));
            }

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErroValidacaoException(errorMessage);
            }
        }
    }
}
