using AutoMapper;
using SistemaFinanceiro.Communication.Requests.Despesas;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Communication.Responses.Despesas;
using SistemaFinanceiro.Communication.Responses.Users;
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
            CreateMap<RequestRegistraUserJson, User>()
                .ForMember(dest => dest.Password, config => config.Ignore());
        }

        private void EntityToResponse()
        {
            CreateMap<Despesa, ResponseLstDespesasJson>();
            CreateMap<Despesa, ResponseShortDespesaJson>();
            CreateMap<Despesa, ResponseDespesaJson>();
        }
    }
}
