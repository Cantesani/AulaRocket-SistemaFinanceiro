namespace WebApi.test.Resource
{
    public class UserIdentityMananger
    {
        private readonly SistemaFinanceiro.Domain.Entities.User _user;
        private readonly string _password;
        private readonly string _token;

        public UserIdentityMananger(SistemaFinanceiro.Domain.Entities.User user, string password, string token)
        {
            _user = user;
            _password = password; 
            _token = token;
        }

        public string GetName() => _user.Nome;
        public string GetEmail() => _user.Email;
        public string GetPassword() => _user.Password;
        public string GetToken () => _token;



    }
}
