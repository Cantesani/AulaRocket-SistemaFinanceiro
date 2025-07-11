using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using SistemaFinanceiro.Application.UseCases.Users.Profile;
using SistemaFinanceiro.Domain.Entities;

namespace UseCases.Test.Users.Profile
{
    public class GetUserProfileUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var useCase = CreateUseCase(user);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Nome.Should().Be(user.Nome);
            result.Email.Should().Be(user.Email);
        }

        private GetUserProfileUseCase CreateUseCase(User user)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GetUserProfileUseCase(loggedUser, mapper);
        }

    }
}
