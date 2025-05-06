using System.Globalization;

namespace SistemaFinanceiro.Api.MiddleWare
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var idiomasSuportados = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            var idiomaRequerido = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("pt-BR");

            if (string.IsNullOrWhiteSpace(idiomaRequerido) == false
                && idiomasSuportados.Exists(x => x.Name.Equals(idiomaRequerido)))
            {
                cultureInfo = new CultureInfo(idiomaRequerido);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
