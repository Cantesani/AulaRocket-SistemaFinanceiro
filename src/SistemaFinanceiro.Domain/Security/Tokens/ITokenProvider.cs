﻿namespace SistemaFinanceiro.Domain.Security.Tokens
{
    public interface ITokenProvider
    {
        public string TokenOnRequest();
    }
}
