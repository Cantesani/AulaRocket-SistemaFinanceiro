using FluentAssertions;
using SistemaFinanceiro.Communication.Enums;
using System.Net;
using System.Text.Json;

namespace WebApi.test.Despesas.GetById
{
    public class GetByIdDespesasTest : SistemaFinanceiroClassFixture
    {
        private const string METHOD = "api/Despesas";
        private readonly string _token;
        private readonly long _despesaId;

        public GetByIdDespesasTest(CustomWebApplicationFactory WebApplicationFactory) : base(WebApplicationFactory)
        {
            _token = WebApplicationFactory.User_Team_Member.GetToken();
            // _despesaId = WebApplicationFactory.Despesa.GetId();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet(requestUri: $"{METHOD}/{_despesaId}", token: _token);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("id").GetInt64().Should().Be(_despesaId);
            response.RootElement.GetProperty("titulo").GetString().Should().NotBeNullOrWhiteSpace();
            response.RootElement.GetProperty("descricao").GetString().Should().NotBeNullOrWhiteSpace();
            response.RootElement.GetProperty("data").GetDateTime().Should().NotBeAfter(DateTime.Today);
            response.RootElement.GetProperty("valor").GetDecimal().Should().BeGreaterThan(0);

            var tipoPagto = response.RootElement.GetProperty("tipoPagto").GetInt32();
            Enum.IsDefined(typeof(TipoPagto), tipoPagto).Should().BeTrue();
        }
    }
}
