using System;
using OSTech.Domain.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace OSTech.Domain.Entities
{
    public class Technician
    {
        public int TechnicianId { get; private set; }
        public string Name { get; private set; }
        public string Specialty { get; private set; }
        public string Contact {  get; private set; }
        public bool Availability { get; private set; }
        public DateOnly HiringDate { get; private set; }

        public ICollection<WorkOrder> WorkOrders { get; private set; } = new List<WorkOrder>();

        private Technician()
        {
        }
        public Technician
        (
            string name,
            string specialty,
            string contact,
            bool availability,
            DateOnly hiringDate
        )
        {
            SetName(name);
            SetSpecialty(specialty);
            SetContact(contact);
            SetAvailability(availability);
            SetHiringDate(hiringDate);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("O nome é obrigatório.");

            Name = name;
        }
        public void SetSpecialty(string specialty)
        {
            if (string.IsNullOrWhiteSpace(specialty))
                throw new DomainException("A especialidade é obrigatório.");

            Specialty = specialty;
        }
        public void SetContact(string contact)
        {
            if (string.IsNullOrWhiteSpace(contact))
                throw new DomainException("O contato é obrigatório.");

            Contact = contact;
        }
        public void SetHiringDate(DateOnly hiringDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (hiringDate > today)
                throw new DomainException("A data de contratação não pode ser maior que a de hoje.");
            HiringDate = hiringDate;
        }
        public void SetAvailability(bool availability)
        {
            Availability = availability;
        }
    }
}
