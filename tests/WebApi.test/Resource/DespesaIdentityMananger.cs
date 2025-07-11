using SistemaFinanceiro.Domain.Entities;

namespace WebApi.test.Resource
{
    public class DespesaIdentityMananger
    {
        private readonly Despesa _despesa;

        public DespesaIdentityMananger(Despesa despesa)
        {
            _despesa = despesa;
        }

        public long GetId() => _despesa.Id;

    }
}
