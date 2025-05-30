using CommonTestUtilities.Request;
using FluentAssertions;
using FluentValidation;
using SistemaFinanceiro.Application.UseCases.Users.Registrar;
using SistemaFinanceiro.Exception;

namespace Validator.Tests.Users.Registrar
{
    public class RegistrarUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            //Arrange
            var validator = new RegistrarUserValidator();
            var request = RequestRegistrarUserJsonBuilder.Build();

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Error_Nome_Empty(string nome)
        {
            //Arrange
            var validator = new RegistrarUserValidator();
            var request = RequestRegistrarUserJsonBuilder.Build();
            request.Nome = nome;
            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.NOME_USUARIO_OBRIGATORIO));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            //Arrange
            var validator = new RegistrarUserValidator();
            var request = RequestRegistrarUserJsonBuilder.Build();
            request.Email = "gabriel.com";
            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO));
        }

        [Fact]
        public void Error_Password_Empty()
        {
            //Arrange
            var validator = new RegistrarUserValidator();
            var request = RequestRegistrarUserJsonBuilder.Build();
            request.Password = string.Empty;
            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.SENHA_USER_INVALIDO));
        }

    }
}
