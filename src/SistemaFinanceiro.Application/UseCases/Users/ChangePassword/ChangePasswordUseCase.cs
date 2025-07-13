using FluentValidation.Results;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Users;
using SistemaFinanceiro.Domain.Security.Criptografia;
using SistemaFinanceiro.Domain.Services.LoggerUser;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Users.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IPasswordCriptografada _passwordCriptografada;

    public ChangePasswordUseCase(IUserUpdateOnlyRepository repository
        , IUnidadeDeTrabalho unidadeDeTrabalho
        , IPasswordCriptografada passwordCriptografada,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _passwordCriptografada = passwordCriptografada;
        _loggedUser = loggedUser;
    }
    
    public async Task Execute(RequestChangePasswordUserJson request)
    {
        var userLogado = await _loggedUser.Get();
        Validate(request, userLogado);

        var user = await _repository.GetById(userLogado.Id);
        user.Password = _passwordCriptografada.Criptografar(request.NewPassword);

        _repository.Update(user);
        await _unidadeDeTrabalho.Commit();
    }
    
    private void Validate(RequestChangePasswordUserJson request, User loggedUser)
    {
        var validator = new ChangePasswordValidator();
        var result = validator.Validate(request);

        var passwordMatch = _passwordCriptografada.VerificaSenha(request.OldPassword, loggedUser.Password);

        if (passwordMatch == false)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.SENHA_DIFERENTE_DA_ATUAL));

        if (result.IsValid == false)
        {
            var erros = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErroValidacaoException(erros);
        }
    }
}