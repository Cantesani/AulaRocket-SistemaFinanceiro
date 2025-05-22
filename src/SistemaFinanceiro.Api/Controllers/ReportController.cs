using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Application.UseCases.Despesas.Reports.Excel;
using SistemaFinanceiro.Application.UseCases.Despesas.Reports.Pdf;
using System.Net.Mime;

namespace SistemaFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel([FromHeader] DateOnly mes,
                                                  [FromServices] IGerarDespesasReportExcelUseCase useCase)
        {
            byte[] file = await useCase.Execute(mes);

            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
            }

            return NoContent();
        }



        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdf([FromHeader] DateOnly mes,
                                            [FromServices] IGerarDespesasReportPdfUseCase useCase)
        {
            byte[] file = await useCase.Execute(mes);

            if(file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

            }

            return NoContent();
        }


    }


}
