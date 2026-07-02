using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.Domain.Entities
{
    public class Technician
    {
        public int TechnicianId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Contact {  get; set; }
        public bool Availability { get; set; }
        public DateOnly HiringDate { get; set; }

        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    }
}
