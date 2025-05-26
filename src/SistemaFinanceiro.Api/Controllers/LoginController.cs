using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Application.UseCases.Login;
using SistemaFinanceiro.Communication.Requests.Login;
using SistemaFinanceiro.Communication.Requests.Users;
using SistemaFinanceiro.Communication.Responses;

namespace SistemaFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RequestRegistraUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] RequestLoginJson request
                                              , [FromServices] ILoginUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Ok(response);

        }

    }
}
