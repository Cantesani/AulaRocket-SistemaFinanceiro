using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Application.UseCases.Despesas;
using SistemaFinanceiro.Application.UseCases.Despesas.Registrar;
using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Communication.Responses;

namespace SistemaFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DespesasController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDespesaJson), StatusCodes.Status201Created)]
        public IActionResult Registra([FromBody] RequestDespesaJson request)
        {
            var useCase = new RegistrarDespesaUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}
