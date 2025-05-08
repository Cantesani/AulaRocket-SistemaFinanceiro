using SistemaFinanceiro.Communication.Enums;
using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Communication.Responses;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Registrar
{
    public class RegistrarDespesaUseCase: IRegistrarDespesaUseCase
    {
        private readonly IDespesasRepository _repository;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        public RegistrarDespesaUseCase(IDespesasRepository repository,
                                       IUnidadeDeTrabalho unidadeDeTrabalho )
        {
            _repository = repository;
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }

        public ResponseDespesaJson Execute(RequestDespesaJson request)
        {
            Validate(request);

            var entity = new Despesa
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Data = request.Data,
                Valor = request.Valor,
                TipoPagto = (Domain.Enums.TipoPagto)request.TipoPagto
            };

            _repository.Add(entity);
            _unidadeDeTrabalho.Commit();

            return new ResponseDespesaJson();
        }

        public void Validate(RequestDespesaJson request)
        {
            var validator = new RegistrarDespesaValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErroValidacaoException(errorMessage);
            }

        }
    }
}
