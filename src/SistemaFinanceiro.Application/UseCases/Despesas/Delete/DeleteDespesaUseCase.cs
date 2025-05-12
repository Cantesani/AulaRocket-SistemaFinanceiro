using System.Net.Sockets;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Delete
{
    public class DeleteDespesaUseCase: IDeleteDespesaUseCase
    {
        private readonly IDespesasWriteOnlyRepository _repository;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

        public DeleteDespesaUseCase(IDespesasWriteOnlyRepository repository,
                                    IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            _repository = repository;
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }
        public async Task Execute(long id)
        {
            var result = await _repository.Delete(id);
            if (result == false)
            {
                throw new NaoExisteException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
            }

            await _unidadeDeTrabalho.Commit();
        }
    }
}
