using CommonTestUtilities.Request;
using FluentAssertions;
using SistemaFinanceiro.Communication.Requests.Login;
using SistemaFinanceiro.Exception;
using System.Net;
using System.Text.Json;

namespace WebApi.test.Login
{
    public class LoginTest : SistemaFinanceiroClassFixture
    {
        private const string METHOD = "api/Login";
        private readonly string _nome;
        private readonly string _email;
        private readonly string _password;
        private readonly string _token;

        public LoginTest(CustomWebApplicationFactory WebApplicationFactory) : base(WebApplicationFactory)
        {
            _nome = WebApplicationFactory.User_Team_Member.GetName();
            _email = WebApplicationFactory.User_Team_Member.GetEmail();
            _password = WebApplicationFactory.User_Team_Member.GetPassword();
            _token = WebApplicationFactory.User_Team_Member.GetToken();
        }

        //[Fact]
        //public async Task Success()
        //{
        //    var request = new RequestLoginJson
        //    {
        //        Email = _email,
        //        Password = _password
        //    };


        //    var result = await DoPost(requestUri: METHOD, request: request, token: _token);
        //    result.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var body = await result.Content.ReadAsStreamAsync();
        //    var response = await JsonDocument.ParseAsync(body);

        //    response.RootElement.GetProperty("nome").GetString().Should().Be(_nome);
        //    response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
        //}

        [Fact]
        public async Task Error_login_invalido()
        {
            var request = RequestLoginJsonBuilder.Build();

            var result = await DoPost(requestUri: METHOD, request: request);
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Contains(ResourceErrorMessages.EMAIL_OU_SENHA_INVALIDOS));
        }
    }
}

