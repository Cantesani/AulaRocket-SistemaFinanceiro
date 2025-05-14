using AutoMapper;
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
        private readonly IDespesasWriteOnlyRepository _repository;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IMapper _mapper;
        public RegistrarDespesaUseCase(IDespesasWriteOnlyRepository repository,
                                       IUnidadeDeTrabalho unidadeDeTrabalho,
                                       IMapper mapper)
        {
            _repository = repository;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _mapper = mapper;
        }

        public async Task<ResponseDespesaJson> Execute(RequestDespesaJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Despesa>(request);

            await _repository.Add(entity);
            await _unidadeDeTrabalho.Commit();

            return _mapper.Map<ResponseDespesaJson>(entity);
        }

        public void Validate(RequestDespesaJson request)
        {
            var validator = new DespesaValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErroValidacaoException(errorMessage);
            }
        }
    }
}
