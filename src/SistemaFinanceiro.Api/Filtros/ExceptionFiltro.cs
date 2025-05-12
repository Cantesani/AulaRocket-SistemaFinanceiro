using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using SistemaFinanceiro.Communication.Responses;
using SistemaFinanceiro.Exception;
using SistemaFinanceiro.Exception.ExceptionBase;

namespace SistemaFinanceiro.Api.Filtros
{
    public class ExceptionFiltro : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is SistemaFinanceiroException)
                PegarErroException(context);
            else
                ErroDesconhecido(context);
        }

        private void PegarErroException(ExceptionContext context)
        {
            var SistemaFinanceiroException = (SistemaFinanceiroException)context.Exception;
            var erroResponse = new ResponseErrorJson(SistemaFinanceiroException.GetErrors());

            context.HttpContext.Response.StatusCode = SistemaFinanceiroException.StatusCode;
            context.Result = new ObjectResult(erroResponse);

        }

        private void ErroDesconhecido(ExceptionContext context)
        {
            var erroResponse = new ResponseErrorJson(ResourceErrorMessages.ERRO_DESCONHECIDO);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(erroResponse);
        }
    }
}
