using OSTech.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OSTech.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Document {  get; private set; }

        public ICollection<WorkOrder> WorkOrders { get; private set; } = new List<WorkOrder>();

        private Customer()
        {
            
        }
        public Customer
        (
           string name,
           string email,
           string phone,
           string document
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
        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("O email é obrigatório.");

            Email = email;
        }
        public void SetPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("O telefone é obrigatório.");

            Phone = phone;
        }
        public void SetDocument(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
                throw new DomainException("O documento é obrigatório.");

            Document = document;
        }
    }
}
