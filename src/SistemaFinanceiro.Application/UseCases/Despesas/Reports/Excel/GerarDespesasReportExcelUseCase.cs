using ClosedXML.Excel;
using SistemaFinanceiro.Domain.Enums;
using SistemaFinanceiro.Domain.Extensions;
using SistemaFinanceiro.Domain.Reports;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using SistemaFinanceiro.Domain.Services.LoggerUser;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Reports.Excel
{
    public class GerarDespesasReportExcelUseCase : IGerarDespesasReportExcelUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IDespesasReadOnlyRepository _repository;
        private const string CURRENCY_SYMBOL = "R$";
        public GerarDespesasReportExcelUseCase(ILoggedUser loggerUser,
                                               IDespesasReadOnlyRepository repository)
        {
            _repository = repository;
            _loggedUser = loggerUser;
        }
        public async Task<byte[]> Execute(DateOnly mes)
        {
            var user = await _loggedUser.Get();

            var despesas = await _repository.GetByMes(user, mes);

            if (despesas.Count == 0)
            {
                return [];
            }

            using var workbook = new XLWorkbook();

            workbook.Author = user.Nome;
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "Times New Roman";

            var worksheet = workbook.Worksheets.Add(mes.ToString("Y"));

            InsertHeader(worksheet);
            
            var contador = 2;
            foreach (var despesa in despesas)
            {
                worksheet.Cell($"A{contador}").Value = despesa.Titulo;
                worksheet.Cell($"B{contador}").Value = despesa.Data;
                worksheet.Cell($"C{contador}").Value = despesa.TipoPagto.TipoPagtoToString();

                worksheet.Cell($"D{contador}").Value = despesa.Valor;
                worksheet.Cell($"D{contador}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";

                worksheet.Cell($"E{contador}").Value = despesa.Descricao;

                contador++;
            }

            worksheet.Columns().AdjustToContents();

            var file = new MemoryStream();
            workbook.SaveAs(file);

            return file.ToArray();

        }

        private void InsertHeader(IXLWorksheet worksheet)
        {
            //TITULOS
            worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITULO;
            worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATA;
            worksheet.Cell("C1").Value = ResourceReportGenerationMessages.TIPO_PAGTO;
            worksheet.Cell("D1").Value = ResourceReportGenerationMessages.VALOR;
            worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIÇÃO;

            //FORMATACAO STYLE
            worksheet.Cells("A1:E1").Style.Font.Bold = true;
            worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");
            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }
    }
}
