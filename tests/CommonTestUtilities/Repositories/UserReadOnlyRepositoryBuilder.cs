using Moq;
using SistemaFinanceiro.Communication.Requests.Login;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories.Users;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {

        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }

        public void ExisteUserComEsseEmail(string email)
        {
            _repository.Setup(userReadOnly => userReadOnly.ExisteUserComEsseEmail(email)).ReturnsAsync(true);
        }

        public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
        {
            _repository.Setup(userReadOnly => userReadOnly.GetUserByEmail(user.Email)).ReturnsAsync(user);

            return this;
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }

    }
}
