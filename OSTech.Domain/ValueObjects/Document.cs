using OSTech.Domain.Common.Base;
using OSTech.Domain.Exceptions;

namespace OSTech.Domain.ValueObjects;

public sealed class Document : ValueObject
{
    public string Number { get; }
    public DocumentType Type { get; }

    private Document(string number, DocumentType type)
    {
        Number = number;
        Type = type;
    }

    public static Document Create(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new DomainException("Documento não informado.");

        var number = new string(document.Where(char.IsDigit).ToArray());

        return number.Length switch
        {
            11 when IsValidCpf(number) =>
                new Document(number, DocumentType.CPF),

            14 when IsValidCnpj(number) =>
                new Document(number, DocumentType.CNPJ),

            11 => throw new DomainException("CPF inválido."),

            14 => throw new DomainException("CNPJ inválido."),

            _ => throw new DomainException("Documento deve ser um CPF ou CNPJ.")
        };
    }

    private static bool IsValidCpf(string cpf)
    {
        if (cpf.Distinct().Count() == 1)
            return false;

        var firstDigit = CalculateCpfDigit(cpf[..9]);
        var secondDigit = CalculateCpfDigit(cpf[..10]);

        return cpf[9] - '0' == firstDigit &&
               cpf[10] - '0' == secondDigit;
    }

    private static int CalculateCpfDigit(string value)
    {
        var sum = 0;
        var weight = value.Length + 1;

        foreach (var digit in value)
        {
            sum += (digit - '0') * weight;
            weight--;
        }

        var remainder = sum % 11;

        return remainder < 2 ? 0 : 11 - remainder;
    }

    private static bool IsValidCnpj(string cnpj)
    {
        if (cnpj.Distinct().Count() == 1)
            return false;

        var firstDigit = CalculateCnpjDigit(cnpj[..12]);
        var secondDigit = CalculateCnpjDigit(cnpj[..13]);

        return cnpj[12] - '0' == firstDigit &&
               cnpj[13] - '0' == secondDigit;
    }

    private static int CalculateCnpjDigit(string value)
    {
        int[] weights = value.Length == 12
            ? [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]
            : [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        var sum = 0;

        for (var i = 0; i < value.Length; i++)
        {
            sum += (value[i] - '0') * weights[i];
        }

        var remainder = sum % 11;

        return remainder < 2 ? 0 : 11 - remainder;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}

public enum DocumentType
{
    CPF,
    CNPJ
}