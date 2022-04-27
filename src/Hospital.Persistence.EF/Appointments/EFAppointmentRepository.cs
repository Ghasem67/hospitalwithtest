
using Hospital.Entities;
using Hospital.Services.Appointments.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Persistence.EF.Appointments
{
    public class EFAppointmentRepository : AppointmentRepository
    {
        private readonly DbSet<Appointment> _appointments;

        public EFAppointmentRepository(EFDbContext context)
        {
            _appointments = context.Set<Appointment>();
        }

        public void Add(Appointment appointment)
        {
            _appointments.Add(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _appointments.Remove(appointment);
        }

        public HashSet<ShowAppointmentDTO> GetAll()
        {
           return _appointments.Select(_ => new ShowAppointmentDTO {Date=_.Date,DoctorName=_.Doctor.FirstName+" "+_.Doctor.LastName,PatientName=_.Patient.FirstName+" "+_.Patient.LastName }).ToHashSet();
        }

        public Appointment GetById(int id)
        {
            return _appointments.FirstOrDefault();
        }
    }
}
