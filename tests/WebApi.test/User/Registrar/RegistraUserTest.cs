using CommonTestUtilities.Request;
using FluentAssertions;
using SistemaFinanceiro.Exception;
using System.Net;
using System.Text.Json;

namespace WebApi.test.User.Registrar
{
    public class RegistraUserTest : SistemaFinanceiroClassFixture
    {
        private const string METHOD = "api/User";

        public RegistraUserTest(CustomWebApplicationFactory WebApplicationFactory) : base(WebApplicationFactory) { }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegistrarUserJsonBuilder.Build();

            var result = await DoPost(requestUri: METHOD, request: request);
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("nome").GetString().Should().Be(request.Nome);
            response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegistrarUserJsonBuilder.Build();
            request.Nome = string.Empty;

            var result = await DoPost(requestUri: METHOD, request: request);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
            errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Contains(ResourceErrorMessages.NOME_USUARIO_OBRIGATORIO));
        }

    }
}
