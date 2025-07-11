using AutoMapper;
using SistemaFinanceiro.Communication.Responses.Despesas;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Domain.Services.LoggerUser;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Despesas.GetByID
{
    public class GetByIdDespesaUseCase : IGetByIdDespesaUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IDespesasReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdDespesaUseCase(ILoggedUser loggedUser,
                                     IDespesasReadOnlyRepository repository
                                    ,IMapper mapper)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseDespesaJson> execute(long id)
        {
            var user = await _loggedUser.Get();
            var result = await _repository.GetById(id, user.Id);

            if (result is null) { 
                throw new NaoExisteException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
            }

            return _mapper.Map<ResponseDespesaJson>(result);

        }
    }
}
