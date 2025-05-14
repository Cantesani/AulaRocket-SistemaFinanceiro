using AutoMapper;
using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Domain.Repositories;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Update
{
    public class UpdateDespesaUseCase: IUpdateDespesaUseCase
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IMapper _mapper;
        private readonly IDespesaUpdateOnlyRepository _repository;
        public UpdateDespesaUseCase(IUnidadeDeTrabalho unidadeDeTrabalho,
                                    IMapper mapper,
                                    IDespesaUpdateOnlyRepository repository)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Execute(long id, RequestDespesaJson request)
        {
            Validate(request);

            var despesa = await _repository.GetById(id);

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
