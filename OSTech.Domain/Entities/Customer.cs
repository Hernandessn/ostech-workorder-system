using OSTech.Domain.Exceptions;
using OSTech.Domain.ValueObjects;

namespace OSTech.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; private set; }
        public string Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public string Phone { get; private set; }
        public Document Document {  get; private set; }

        public ICollection<WorkOrder> WorkOrders { get; private set; } = new List<WorkOrder>();

        private Customer()
        {
            
        }
        public Customer
        (
           string name,
           EmailAddress email,
           string phone,
           Document document
        )
        {
            SetName(name);
            SetEmail(email);
            SetPhone(phone);
            SetDocument(document);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("O nome é obrigatório.");

            Name = name;
        }
        public void SetEmail(EmailAddress email)
        {
            if (email == null)
                throw new DomainException("Email inválido.");
            Email = email;
        }
        public void SetPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("O telefone é obrigatório.");

            Phone = phone;
        }
        public void SetDocument(Document document)
        {
            if (document == null)
                throw new DomainException("O documento é obrigatório.");

            Document = document;
        }
    }
}
