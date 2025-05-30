using Moq;
using SistemaFinanceiro.Domain.Repositories;

namespace CommonTestUtilities.Repositories
{
    public class UnidadeDeTrabalhoBuilder
    {
        public static IUnidadeDeTrabalho Build()
        {
            var mock = new Mock<IUnidadeDeTrabalho>();
            return mock.Object;
        }

    }
}
