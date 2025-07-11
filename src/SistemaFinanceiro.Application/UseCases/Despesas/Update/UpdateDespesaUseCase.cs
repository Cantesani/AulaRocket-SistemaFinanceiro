using AutoMapper;
using SistemaFinanceiro.Application.UseCases.Despesas.Registrar;
using SistemaFinanceiro.Communication.Requests.Despesas;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Domain.Services.LoggerUser;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Update
{
    public class UpdateDespesaUseCase: IUpdateDespesaUseCase
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IMapper _mapper;
        private readonly IDespesaUpdateOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        public UpdateDespesaUseCase(IUnidadeDeTrabalho unidadeDeTrabalho,
                                    IMapper mapper,
                                    IDespesaUpdateOnlyRepository repository,
                                    ILoggedUser loggedUser)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _mapper = mapper;
            _repository = repository;
            _loggedUser = loggedUser;
        }
        public async Task Execute(long id, RequestDespesaJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.Get();

            var despesa = await _repository.GetById(id, loggedUser.Id);


            if (despesa is null)
            {
                throw new NaoExisteException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
            }

            _mapper.Map(request, despesa);

            _repository.Update(despesa);

            await _unidadeDeTrabalho.Commit();
        }


        private void Validate(RequestDespesaJson request)
        {
            var validator = new DespesaValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false) { 
            var errorMessages = result.Errors.Select(x=>x.ErrorMessage).ToList();

                throw new ErroValidacaoException(errorMessages);
            }
        }
    }
}
