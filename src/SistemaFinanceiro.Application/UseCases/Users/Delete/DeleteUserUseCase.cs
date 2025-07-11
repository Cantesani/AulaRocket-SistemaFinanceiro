using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Users;
using SistemaFinanceiro.Domain.Services.LoggerUser;

namespace SistemaFinanceiro.Application.UseCases.Users.Delete
{
    public class DeleteUserUseCase: IDeleteUserUseCase
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly ILoggedUser _loggedUser;
        private readonly IUserWriteOnlyRepository _repository;

        public DeleteUserUseCase(IUnidadeDeTrabalho unidadeDeTrabalho,
                                 ILoggedUser loggedUser,
                                 IUserWriteOnlyRepository repository)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public async Task Execute()
        {
            var usuarioLogado = await _loggedUser.Get();

            await _repository.Delete(usuarioLogado);
            await _unidadeDeTrabalho.Commit();
        }
    }
}
