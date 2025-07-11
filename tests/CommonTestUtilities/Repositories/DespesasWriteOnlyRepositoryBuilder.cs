using DocumentFormat.OpenXml.Drawing;
using Microsoft.VisualBasic;
using Moq;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class DespesasWriteOnlyRepositoryBuilder
    {
        public static IDespesasWriteOnlyRepository Build()
        {
            var mock = new Mock<IDespesasWriteOnlyRepository>();
            return mock.Object;
        }
    }
}
