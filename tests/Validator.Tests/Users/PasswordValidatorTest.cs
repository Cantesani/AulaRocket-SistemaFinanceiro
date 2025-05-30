using CommonTestUtilities.Request;
using FluentAssertions;
using FluentValidation;
using SistemaFinanceiro.Application.UseCases.Users;
using SistemaFinanceiro.Application.UseCases.Users.Registrar;
using SistemaFinanceiro.Communication.Requests.Users;

namespace Validator.Tests.Users
{
    public class PasswordValidatorTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        [InlineData("aaaaa")]
        [InlineData("aaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaaaaa")]
        [InlineData("AAAAAAAA")]
        [InlineData("Aaaaaaaa")]
        [InlineData("Aaaaaaa1")]
        public void Error_Password_Invalid(string password)
        {
            //Arrange
            var validator = new PasswordValidator<RequestRegistraUserJson>();

            //Act
            var result = validator
                .IsValid(new ValidationContext<RequestRegistraUserJson>(new RequestRegistraUserJson()), password);

            //Assert
            result.Should().BeFalse();
        }
    }
}
