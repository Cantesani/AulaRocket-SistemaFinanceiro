using AutoMapper;
using SistemaFinanceiro.Communication.Responses;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Despesas.GetByID
{
    public class GetByIdDespesaUseCase : IGetByIdDespesaUseCase
    {
        private readonly IDespesasReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdDespesaUseCase(IDespesasReadOnlyRepository repository
                                    , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseDespesaJson> execute(long id)
        {
            var result = await _repository.GetById(id);

            if (result is null) { 
                throw new NaoExisteException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
            }

            return _mapper.Map<ResponseDespesaJson>(result);

        }
    }
}
