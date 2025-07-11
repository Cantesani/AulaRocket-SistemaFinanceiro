using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Domain.Services.LoggerUser;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;
using System.Net.Sockets;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Delete
{
    public class DeleteDespesaUseCase : IDeleteDespesaUseCase
    {
        private readonly IDespesasReadOnlyRepository _despesaReadOnly;
        private readonly IDespesasWriteOnlyRepository _repository;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly ILoggedUser _loggedUser;

        public DeleteDespesaUseCase(IDespesasReadOnlyRepository despesaReadOnly,
                                    IDespesasWriteOnlyRepository repository,
                                    IUnidadeDeTrabalho unidadeDeTrabalho,
                                    ILoggedUser loggedUser)
        {
            _despesaReadOnly = despesaReadOnly;
            _repository = repository;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _loggedUser = loggedUser;
        }
        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.Get();
            
            var despesa = await _despesaReadOnly.GetById(id, loggedUser.Id);

            if (despesa is null)
            {
                throw new NaoExisteException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
            }

            await _repository.Delete(id);

            await _unidadeDeTrabalho.Commit();
        }
    }
}
