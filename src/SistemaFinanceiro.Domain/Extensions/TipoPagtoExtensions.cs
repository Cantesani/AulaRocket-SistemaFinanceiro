using SistemaFinanceiro.Domain.Enums;
using SistemaFinanceiro.Domain.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiro.Domain.Extensions
{
    public static class TipoPagtoExtensions
    {
        public static string TipoPagtoToString(this TipoPagto tipoPagto)
        {
            return tipoPagto switch
            {
                TipoPagto.Dinheiro => ResourceReportGenerationMessages.DINHEIRO,
                TipoPagto.Debito => ResourceReportGenerationMessages.DEBITO,
                TipoPagto.Credito => ResourceReportGenerationMessages.CREDITO,
                TipoPagto.Pix => ResourceReportGenerationMessages.PIX,
                _ => string.Empty
            };
        }
    }
}
