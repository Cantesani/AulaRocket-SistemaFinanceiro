using Bogus;
using CommonTestUtilities.Criptografia;
using SistemaFinanceiro.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class UserBuilder
    {
        public static User Build()
        {
            var passwordCriptografada = new PasswordCriptografadaBuilder().Build();

            return new Faker<User>()
                .RuleFor(u => u.Id, _ => 1)
                .RuleFor(u => u.Nome, faker => faker.Person.FirstName)
                .RuleFor(u => u.Email, (faker, User) => faker.Internet.Email(User.Nome))
                .RuleFor(u => u.Password, (_, User) => passwordCriptografada.Criptografar(User.Password))
                .RuleFor(u => u.UserIdentifier, _ => Guid.NewGuid());
        }
    }
}
