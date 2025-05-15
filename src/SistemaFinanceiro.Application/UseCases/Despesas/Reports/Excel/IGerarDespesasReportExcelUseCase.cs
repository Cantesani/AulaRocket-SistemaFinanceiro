namespace SistemaFinanceiro.Application.UseCases.Despesas.Reports.Excel
{
    public interface IGerarDespesasReportExcelUseCase
    {
        public Task<byte[]> Execute(DateOnly mes);
    }
}
