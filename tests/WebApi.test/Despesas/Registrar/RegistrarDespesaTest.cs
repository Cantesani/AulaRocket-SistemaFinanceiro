using CommonTestUtilities.Request;
using FluentAssertions;
using SistemaFinanceiro.Exception;
using System.Net;
using System.Text.Json;

namespace WebApi.test.Despesas.Registrar
{
    public class RegistrarDespesaTest : SistemaFinanceiroClassFixture
    {
        private readonly HttpClient _httpClient;
        private const string METHOD = "api/Despesas";
        private readonly string _token;

        public RegistrarDespesaTest(CustomWebApplicationFactory WebApplicationFactory) : base(WebApplicationFactory)
        {
            _httpClient = WebApplicationFactory.CreateClient();
            _token = WebApplicationFactory.User_Team_Member.GetToken();
        }


        [Fact]
        public async Task Success()
        {
            var request = RequestRegistrarDespesaJsonBuilder.Builder();

            var result = await DoPost(requestUri: METHOD, request: request, token: _token);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("titulo").GetString().Should().Be(request.Titulo);
        }

        [Fact]
        public async Task Error_Titulo_Empty()
        {
            var request = RequestRegistrarDespesaJsonBuilder.Builder();
            request.Titulo = string.Empty;

            var result = await DoPost(requestUri: METHOD, request: request, token: _token);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
            errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Contains(ResourceErrorMessages.TITULO_OBRIGATORIO));
        }



    }
}
