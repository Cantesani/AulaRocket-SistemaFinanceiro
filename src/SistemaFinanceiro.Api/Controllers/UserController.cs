using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Application.UseCases.Users.Delete;
using SistemaFinanceiro.Application.UseCases.Users.Profile;
using SistemaFinanceiro.Application.UseCases.Users.Registrar;
using SistemaFinanceiro.Application.UseCases.Users.Update;
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
                                                 , [FromServices] IRegistraUserUseCase useCase)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }


        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromServices] IUpdateUserUseCase useCase,
                                                       [FromBody] RequestUpdateUserJson request)
        {
            await useCase.Execute(request);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> DeleteProfile([FromServices] IDeleteUserUseCase useCase)
        {
            await useCase.Execute();

            return NoContent();
        }


    }
}
