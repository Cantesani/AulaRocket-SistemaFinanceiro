using AutoMapper;
using SistemaFinanceiro.Communication.Requests.Despesas;
using SistemaFinanceiro.Communication.Responses.Despesas;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Domain.Services.LoggerUser;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Registrar
{
    public class RegistrarDespesaUseCase: IRegistrarDespesaUseCase
    {
        private readonly IDespesasWriteOnlyRepository _repository;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        public RegistrarDespesaUseCase(IDespesasWriteOnlyRepository repository,
                                       IUnidadeDeTrabalho unidadeDeTrabalho,
                                       IMapper mapper,
                                       ILoggedUser loggedUser)
        {
            _repository = repository;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseDespesaJson> Execute(RequestDespesaJson request)
        {
            Validate(request);

            var despesa = _mapper.Map<Despesa>(request);
            var loggedUser = await _loggedUser.Get();

            despesa.UserId = loggedUser.Id;

            await _repository.Add(despesa);
            await _unidadeDeTrabalho.Commit();

            return _mapper.Map<ResponseDespesaJson>(despesa);
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
