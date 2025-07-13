using Bogus;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Enums;
using Tag = SistemaFinanceiro.Domain.Enums.Tag;

namespace CommonTestUtilities.Entities
{
    public class DespesaBuilder
    {
        public static List<Despesa> Collection(User user, uint count = 2)
        {
            var list = new List<Despesa>();

            if (count == 0)
                count = 1;

            var despesaId = 1;

            for (int i = 0; i < count; i++)
            {
                var despesa = Build(user);
                despesa.Id = despesaId++;
                list.Add(despesa);
            }

            return list;
        }

        public static Despesa Build(User user)
        {
            return new Faker<Despesa>()
                .RuleFor(u => u.Id, _ => 1)
                .RuleFor(u => u.Titulo, faker => faker.Commerce.ProductName())
                .RuleFor(r => r.Descricao, faker => faker.Commerce.ProductDescription())
                .RuleFor(r => r.Data, faker => faker.Date.Past())
                .RuleFor(r => r.Valor, faker => faker.Random.Decimal(min: 1, max: 1000))
                .RuleFor(r => r.TipoPagto, faker => faker.PickRandom<TipoPagto>())
                .RuleFor(r => r.UserId, _ => user.Id)
                .RuleFor(r => r.Tags, faker => faker.Make(3, () => new SistemaFinanceiro.Domain.Entities.Tag
                {
                    Id = 1,
                    Valor = faker.PickRandom<SistemaFinanceiro.Domain.Enums.Tag>(),
                    DespesaId = 1
                }));
        }

    }
}
