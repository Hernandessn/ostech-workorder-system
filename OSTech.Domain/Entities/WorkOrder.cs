using OSTech.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.Domain.Entities
{
    public class WorkOrder
    {
        public int WorkOrderId { get; set; }
        public string Client {  get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Deadline { get; set; }
        public DateOnly OpeningDate { get; set; }
        public StatusWorkOrder Status { get; set; }
        public int TechnicianId { get; set; } // FK
        public Technician? Technician { get; set; }

    }
}
