using FluentAssertions;
using OSTech.Domain.Exceptions;
using OSTech.Domain.ValueObjects;
using Xunit;

namespace OSTech.Tests.UnitTests.Domain.ValueObjects
{
    public class EmailAddressTests
    {
        [Theory]
        [InlineData("teste@teste.com")]
        [InlineData("outro.email@dominio.com.br")]
        public void Create_Succeeds_WithValidFormat(string email)
        {
            var result = EmailAddress.Create(email);

            result.Address.Should().Be(email.Trim().ToLowerInvariant());
        }

        [Theory]
        [InlineData("")]
        [InlineData("emailinvalido")]
        [InlineData("@semusuario.com")]
        [InlineData(null)]
        public void Create_ThrowsDomainException_WithInvalidFormat(string? email)
        {
            Action act = () => EmailAddress.Create(email!);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Create_NormalizesToLowercaseAndTrimmed()
        {
            var result = EmailAddress.Create("  Teste@Teste.COM  ");

            result.Address.Should().Be("teste@teste.com");
        }

        [Fact]
        public void Equals_ReturnsTrue_ForSameAddress()
        {
            var email1 = EmailAddress.Create("teste@teste.com");
            var email2 = EmailAddress.Create("TESTE@teste.com");

            email1.Should().Be(email2);
        }
    }
}