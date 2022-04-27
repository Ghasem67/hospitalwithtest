using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Appointments.Contracts
{
    public class ShowAppointmentDTO
    {
        public DateTime Date { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
    }
}
