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
            CreateMap<RequestRegistraUserJson, User>()
                .ForMember(dest => dest.Password, config => config.Ignore());
            
            CreateMap<RequestDespesaJson, Despesa>()
                .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));
            
            CreateMap<Communication.Enums.Tag, Tag>()
                .ForMember(x=>x.Valor, config => config.MapFrom(source => source));
            
        }

        private void EntityToResponse()
        {
            CreateMap<Despesa, ResponseDespesaJson>()
                .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Valor)));
            
            CreateMap<Despesa, ResponseLstDespesasJson>();
            CreateMap<Despesa, ResponseShortDespesaJson>();
            CreateMap<User, ResponseUserProfileJson>();
        }
    }
}
