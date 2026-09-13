using FluentAssertions;
using OSTech.Domain.Exceptions;
using OSTech.Domain.ValueObjects;
using Xunit;

namespace OSTech.Tests.UnitTests.Domain.ValueObjects
{
    public class DocumentTests
    {
        [Fact]
        public void Create_Succeeds_WithValidCpf()
        {
            var document = Document.Create("52998224725"); // CPF válido conhecido

            document.Type.Should().Be(DocumentType.CPF);
        }

        [Fact]
        public void Create_ThrowsDomainException_WithInvalidCpf()
        {
            Action act = () => Document.Create("11111111111");

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Create_ThrowsDomainException_WithWrongLength()
        {
            Action act = () => Document.Create("123");

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Create_ThrowsDomainException_WhenNullOrEmpty()
        {
            Action act = () => Document.Create("");

            act.Should().Throw<DomainException>();
        }
    }
}