using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Reports.Pdf
{
    public interface IGerarDespesasReportPdfUseCase
    {
        public Task<Byte[]> Execute(DateOnly mes);
    }
}
