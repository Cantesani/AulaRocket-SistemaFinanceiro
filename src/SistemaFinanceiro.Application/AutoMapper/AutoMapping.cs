using AutoMapper;
using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Communication.Responses;
using SistemaFinanceiro.Domain.Entities;

namespace SistemaFinanceiro.Application.AutoMapper
{
    public class AutoMapping : Profile
    {

        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }


        private void RequestToEntity()
        {
            CreateMap<RequestDespesaJson, Despesa>();
        }

        private void EntityToResponse()
        {
            CreateMap<Despesa, ResponseLstDespesasJson>();
            CreateMap<Despesa, ResponseShortDespesaJson>();
            CreateMap<Despesa, ResponseDespesaJson>();
        }
    }
}
