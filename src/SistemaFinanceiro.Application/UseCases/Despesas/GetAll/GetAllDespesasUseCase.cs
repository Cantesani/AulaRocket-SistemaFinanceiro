using AutoMapper;
using SistemaFinanceiro.Communication.Responses.Despesas;
using SistemaFinanceiro.Domain.Repositories.Despesas;

namespace SistemaFinanceiro.Application.UseCases.Despesas.GetAll
{
    public class GetAllDespesasUseCase : IGetAllDespesasUseCase
    {   
        private readonly IDespesasReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        public GetAllDespesasUseCase(IDespesasReadOnlyRepository repository,
                                    IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseLstDespesasJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseLstDespesasJson
            {
                Despesas = _mapper.Map<List<ResponseShortDespesaJson>>(result)
            };
        }
    }
}
