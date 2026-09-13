using OSTech.Domain.Common.Base;
using OSTech.Domain.Exceptions;
using System.Net.Mail;

namespace OSTech.Domain.ValueObjects;

public sealed class EmailAddress : ValueObject
{
    public string Address { get; }

    public EmailAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new DomainException("Endereço de e-mail inválido.");

        address = address.Trim().ToLowerInvariant();

        if (!MailAddress.TryCreate(address, out var mailAddress) ||
            mailAddress.Address != address)
        {
            throw new DomainException("Endereço de e-mail inválido.");
        }

        Address = address;
    }

    public static EmailAddress Create(string address)
    {
        return new EmailAddress(address);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}