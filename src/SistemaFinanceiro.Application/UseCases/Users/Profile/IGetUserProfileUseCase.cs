using SistemaFinanceiro.Communication.Responses.Users;

namespace SistemaFinanceiro.Application.UseCases.Users.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
