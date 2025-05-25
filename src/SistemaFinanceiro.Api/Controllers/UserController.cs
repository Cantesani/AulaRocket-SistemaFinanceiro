using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Application.UseCases.Users.Registrar;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Communication.Responses;
using SistemaFinanceiro.Communication.Responses.Users;

namespace SistemaFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistraUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registra([FromBody] RequestRegistraUserJson request
                                                 ,[FromServices] IRegistraUserUseCase useCase)
        {
            var response = await useCase.Execute(request);



            return Created(string.Empty, response); 
        }
    }
}
