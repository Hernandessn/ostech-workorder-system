using OSTech.Domain.Entities.Enums;
using OSTech.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.Domain.Entities
{
    public class WorkOrder
    {
        public int WorkOrderId { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public decimal Amount { get; private set; }
        public DateOnly Deadline { get; private set; }
        public DateOnly OpeningDate { get; private set; }
        public StatusWorkOrder Status { get; private set; }
        public int TechnicianId { get; private set; } // FK
        public Technician? Technician { get; private set; }
        public int CustomerId { get; private set; }
        public Customer? Customer { get; private set; }
        public int CategoryId { get; private set; }
        public Category? Category { get; private set; }
        public int EquipmentId { get; private set; }
        public Equipment? Equipment { get; private set; }
        private WorkOrder()
        {

        }
        public WorkOrder
        (
            string client,
            string description,
            string title,
            decimal amount,
            DateOnly deadline,
            DateOnly openingDate,
            int technicianId
            )
        {
            SetDescription(description);
            SetTitle(title);
            SetAmount(amount);

            OpeningDate = openingDate;

            Status = StatusWorkOrder.Open;
            ChangeDeadline(deadline); 
            AssignTechnician(technicianId);
        }
        public void AssignTechnician(int technicianId)
        {
            if (technicianId <= 0)
            {
                throw new DomainException("Técnico inválido.");
            }
            TechnicianId = technicianId;
        }
        public void ChangeDeadline(DateOnly deadline)
        {
            if (deadline < OpeningDate)
            {
                throw new DomainException("A data de término não pode ser menor que a data de abertura.");
            }
            Deadline = deadline;
        }
        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new DomainException("Descrição inválida.");
            }
            Description = description;
        }
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new DomainException("Título inválido.");
            }
            Title = title;
        }
        public void SetAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new DomainException("O valor deve ser maior que zero.");
            }
            Amount = amount;
        }
        public void Start()
        {
            if (Status != StatusWorkOrder.Open)
                throw new DomainException(
                    "Somente ordens abertas podem ser iniciadas.");

            Status = StatusWorkOrder.InProgress;
        }
        public void Complete()
        {
            if (Status != StatusWorkOrder.InProgress)
                throw new DomainException(
                    "Somente ordens em andamento podem ser concluídas.");

            Status = StatusWorkOrder.Completed;
        }

        public void Cancel()
        {
            if (Status == StatusWorkOrder.Completed)
                throw new DomainException(
                    "Uma ordem concluída não pode ser cancelada.");

            if (Status == StatusWorkOrder.Canceled)
                throw new DomainException(
                    "A ordem já está cancelada.");

            Status = StatusWorkOrder.Canceled;
        }
    }
}
