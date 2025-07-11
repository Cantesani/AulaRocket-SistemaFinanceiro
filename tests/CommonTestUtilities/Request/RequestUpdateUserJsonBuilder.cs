using Bogus;
using SistemaFinanceiro.Communication.Requests.Users;

namespace CommonTestUtilities.Request
{
    public class RequestUpdateUserJsonBuilder
    {
        public static RequestUpdateUserJson Builder()
        {
            return new Faker<RequestUpdateUserJson>()
                .RuleFor(x => x.Nome, faker => faker.Person.FirstName)
                .RuleFor(x => x.Email, (faker, user) => faker.Internet.Email(user.Email));
        }
    }
}
