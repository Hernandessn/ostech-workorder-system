using OSTech.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<WorkOrder> WorkOrders { get; private set; } = new List<WorkOrder>();
        private Category()
        {
            
        }
        public Category(string name, string description)
        {
            SetName(name);
            SetDescription(description);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("O nome é obrigatório.");
            Name = name;
        }
        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("A descrição é obrigatória.");
            Description = description;
        }
    }
}
