using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.test.Despesas.GetAll
{
    public class GetAllDespesasTest : SistemaFinanceiroClassFixture
    {
        private const string METHOD = "api/Despesas";
        private readonly string _token;

        public GetAllDespesasTest(CustomWebApplicationFactory WebApplicationFactory) : base(WebApplicationFactory)
        {
            _token = WebApplicationFactory.User_Team_Member.GetToken();
        }

        [Fact]
        public async Task Success()
        {

            var result = await DoGet(requestUri: METHOD, token: _token);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("despesas").EnumerateArray().Should().NotBeNullOrEmpty();
        }


    }
}
