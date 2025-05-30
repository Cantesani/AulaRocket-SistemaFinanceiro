using Bogus;
using SistemaFinanceiro.Communication.Enums;
using SistemaFinanceiro.Communication.Requests.Despesas;

namespace CommonTestUtilities.Request
{
    public class RequestRegistrarDespesaJsonBuilder
    {
        public static RequestDespesaJson Builder()
        {
            return new Faker<RequestDespesaJson>()
                .RuleFor(x => x.Titulo, faker => faker.Commerce.ProductName())
                .RuleFor(x => x.Descricao, faker => faker.Commerce.ProductDescription())
                .RuleFor(x => x.Data, faker => faker.Date.Past())
                .RuleFor(x => x.Valor, faker => faker.Random.Decimal(min: 1, max: 1000))
                .RuleFor(x => x.TipoPagto, faker => faker.PickRandom<TipoPagto>());
        }
    }
}
