using FluentValidation.Results;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Users;
using SistemaFinanceiro.Domain.Services.LoggerUser;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Users.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

        public UpdateUserUseCase(IUserUpdateOnlyRepository repository,
                                 IUserReadOnlyRepository userReadOnlyRepository,
                                 ILoggedUser loggedUser, 
                                 IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            _repository = repository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _loggedUser = loggedUser;
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }

        public UpdateUserUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository updateRepository, IUserReadOnlyRepository userReadOnlyRepository, IUnidadeDeTrabalho unidadeDeTrabalho)
        {
        }

        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _loggedUser.Get();

            await Validate(request, loggedUser.Email);

            var user = await _repository.GetById(loggedUser.Id);

            user.Nome = request.Nome;
            user.Email = request.Email;

            _repository.Update(user);

            await _unidadeDeTrabalho.Commit();
        }








        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result = validator.Validate(request);

            if(currentEmail.Equals(request.Email) == false)
            {
                var userExists = await _userReadOnlyRepository.ExisteUserComEsseEmail(request.Email);
                
                if (userExists)
                    result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_JA_EXISTE));
            }

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErroValidacaoException(errorMessages);
            }
        }
    }
}
