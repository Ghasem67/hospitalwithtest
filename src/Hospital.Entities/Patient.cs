using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities
{
    public class Patient:Person
    {
        public HashSet<Appointment> Appointments { get; set; }
    }
}
