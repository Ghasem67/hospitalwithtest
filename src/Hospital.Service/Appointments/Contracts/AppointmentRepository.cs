using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Appointments.Contracts
{
    public interface AppointmentRepository
    {
        void Add(Appointment appointment);
        void Delete(Appointment appointment);
        Appointment GetById(int id);
        HashSet<ShowAppointmentDTO> GetAll(int doctorid, DateTime date);
        int Get(int doctorid, DateTime date, int PatientId);
    }
}
