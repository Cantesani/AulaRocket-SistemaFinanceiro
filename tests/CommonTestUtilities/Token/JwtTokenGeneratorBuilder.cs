using Moq;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Security.Tokens;

namespace CommonTestUtilities.Token
{
    public class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build()
        {
            var mock = new Mock<IAccessTokenGenerator>();

            mock.Setup(tokenGeneration => tokenGeneration.Generate(It.IsAny<User>())).Returns("aaaaa");

            return mock.Object;
        }

    }
}
