﻿namespace SistemaFinanceiro.Communication.Requests.Users
{
    public class RequestRegistraUserJson
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
