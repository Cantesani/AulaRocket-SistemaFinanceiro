using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Application.UseCases.Despesas;
using SistemaFinanceiro.Application.UseCases.Despesas.Delete;
using SistemaFinanceiro.Application.UseCases.Despesas.GetAll;
using SistemaFinanceiro.Application.UseCases.Despesas.GetByID;
using SistemaFinanceiro.Application.UseCases.Despesas.Registrar;
using SistemaFinanceiro.Application.UseCases.Despesas.Update;
using SistemaFinanceiro.Communication.Requests;
using SistemaFinanceiro.Communication.Responses;

namespace SistemaFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DespesasController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistraDespesaJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registra(
                                [FromServices] IRegistrarDespesaUseCase useCase
                               , [FromBody] RequestDespesaJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseLstDespesasJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllDespesas([FromServices] IGetAllDespesasUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Despesas.Count() != 0)
                return Ok(response);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseDespesaJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
                                            [FromRoute] long id,
                                            [FromServices] IGetByIdDespesaUseCase useCase)
        {
            var response = await useCase.execute(id);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id,
                                                [FromServices] IDeleteDespesaUseCase useCase)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id,
                                                [FromBody] RequestDespesaJson request,
                                                [FromServices] IUpdateDespesaUseCase useCase
                                                    )
        {

            await useCase.Execute(id, request);


            return Ok();
        }


    }
}
