using Moq;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Services.LoggerUser;

namespace CommonTestUtilities.Repositories
{
    public class LoggedUserBuilder
    {
        public static ILoggedUser Build(User user)
        {
            var mock = new Mock<ILoggedUser>();
            mock.Setup(x => x.Get()).ReturnsAsync(user);

            return mock.Object;
        }
    }
}
