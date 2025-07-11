using AutoMapper;
using SistemaFinanceiro.Communication.Responses.Despesas;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Domain.Services.LoggerUser;

namespace SistemaFinanceiro.Application.UseCases.Despesas.GetAll
{
    public class GetAllDespesasUseCase : IGetAllDespesasUseCase
    {
        private readonly ILoggedUser _loggerUser;
        private readonly IDespesasReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDespesasUseCase(ILoggedUser loggerUser,
                                    IDespesasReadOnlyRepository repository,
                                    IMapper mapper)
        {
            _loggerUser = loggerUser;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseLstDespesasJson> Execute()
        {
            var user = await _loggerUser.Get();

            var result = await _repository.GetAll(user.Id);

            return new ResponseLstDespesasJson
            {
                Despesas = _mapper.Map<List<ResponseShortDespesaJson>>(result)
            };
        }
    }
}
