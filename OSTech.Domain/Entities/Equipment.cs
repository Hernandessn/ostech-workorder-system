using OSTech.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.Domain.Entities
{
    public class Equipment
    {
        public int EquipmentId { get; private set; }
        public string Name { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string SerialNumber { get; private set; }

        public ICollection<WorkOrder> WorkOrders { get; private set; } = new List<WorkOrder>();

        private Equipment()
        {
            
        }
        public Equipment
        (
            string name, 
            string brand,
            string model,
            string serialNumber
        )
        {
            SetName(name);
            SetModel(model);
            SetBrand(brand);
            SetSerialNumber(serialNumber);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("O nome é obrigatório.");
            Name = name;
        }
        public void SetModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new DomainException("O modelo é obrigatório.");

            Model = model;
        }
        public void SetBrand(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
                throw new DomainException("A marca é obrigatória.");

            Brand = brand;
        }
        public void SetSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                throw new DomainException("O número de série deve ser maior que zero.");

            SerialNumber = serialNumber;
        }

    }
}
