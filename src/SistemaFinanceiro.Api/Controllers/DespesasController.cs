using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Application.UseCases.Despesas;
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
            try
            {
                var useCase = new RegistraDespesaUseCase();
                var response = useCase.Execute(request);
                return Created(string.Empty, response);
            }
            catch (Exception ex)
            {
                var erroResponse = new ResponseErrorJson(ex.Message);
                return BadRequest(erroResponse);
            }
            catch
            {
                var erroResponse = new ResponseErrorJson("Erro desconhecido");

                return StatusCode(StatusCodes.Status500InternalServerError, erroResponse);
            }

        }
    }
}
