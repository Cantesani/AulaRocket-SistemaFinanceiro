using AutoMapper;
using SistemaFinanceiro.Communication.Responses.Users;
using SistemaFinanceiro.Domain.Services.LoggerUser;

namespace SistemaFinanceiro.Application.UseCases.Users.Profile
{
    public class GetUserProfileUseCase: IGetUserProfileUseCase
    {
        private readonly ILoggedUser _loggerUser;
        private readonly IMapper _mapper;

        public GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
        {
            _loggerUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _loggerUser.Get();
            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
