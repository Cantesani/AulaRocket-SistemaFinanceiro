using Bogus;
using SistemaFinanceiro.Communication.Requests.Users;

namespace CommonTestUtilities.Request
{
    public class RequestRegistrarUserJsonBuilder
    {
        public static RequestRegistraUserJson Build()
        {
            return new Faker<RequestRegistraUserJson>()
                .RuleFor(user => user.Nome, faker => faker.Person.FirstName)
                .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Nome))
                .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
        }
    }
}
