using DocumentFormat.OpenXml.Spreadsheet;
using Moq;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Repositories.Users;
using SistemaFinanceiro.Domain.Security.Criptografia;

namespace CommonTestUtilities.Criptografia
{
    public class PasswordCriptografadaBuilder
    {
        private readonly Mock<IPasswordCriptografada> _mock;

        public PasswordCriptografadaBuilder()
        {
            _mock = new Mock<IPasswordCriptografada>();
            _mock.Setup(passwordcriptogr => passwordcriptogr.Criptografar(It.IsAny<string>())).Returns("Cante*123");
        }

        public IPasswordCriptografada Build()
        {
            return _mock.Object;
        }

        public PasswordCriptografadaBuilder VerificaSenha(string? password)
        {
            if (!string.IsNullOrWhiteSpace(password))
                _mock.Setup(passwordCriptografado => passwordCriptografado.VerificaSenha(password, It.IsAny<string>())).Returns(true);

            return this;
        }

    }
}
