using Moq;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories.Despesas;

namespace CommonTestUtilities.Repositories
{
    public class DespesasReadOnlyRepositoryBuilder
    {

        private readonly Mock<IDespesasReadOnlyRepository> _repository;

        public DespesasReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IDespesasReadOnlyRepository>();
        }

        public DespesasReadOnlyRepositoryBuilder GetAll(User user, List<Despesa> despesas)
        {
            _repository.Setup(x => x.GetAll(user.Id)).ReturnsAsync(despesas);
            return this;
        }

        public DespesasReadOnlyRepositoryBuilder GetById(User user, Despesa? despesa)
        {
            if (despesa is not null)
                _repository.Setup(x => x.GetById(despesa.Id, user.Id)).ReturnsAsync(despesa);

            return this;
        }

        public IDespesasReadOnlyRepository Build() => _repository.Object;
    }
}
