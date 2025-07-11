using SistemaFinanceiro.Domain.Security.Tokens;

namespace SistemaFinanceiro.Api.Token
{
    public class HttpContextTokenValue : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public HttpContextTokenValue(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string TokenOnRequest()
        {
            var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization. ToString();

            //parecido com um replace, pega toda a string a partir do final doa palavra Bearer
            return authorization["Bearer ".Length..].Trim();
        }
    }
}
