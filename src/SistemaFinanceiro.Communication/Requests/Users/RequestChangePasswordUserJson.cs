namespace SistemaFinanceiro.Communication.Requests.Users;

public class RequestChangePasswordUserJson
{
    public string OldPassword { get; set; } = string.Empty;
    
    public string NewPassword { get; set; } = string.Empty;
}