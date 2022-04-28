
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

        public int Get(int doctorid, DateTime date,int PatientId)
        {
            return _appointments.Where(x => x.DoctorId.Equals(doctorid) && 
            x.Date.Date.Equals(date.Date) && x.PatientId.Equals(PatientId)).Count();
        }

        public HashSet<ShowAppointmentDTO> GetAll(int doctorid,DateTime date)
        {
           return _appointments.Where(x=>x.DoctorId.Equals(doctorid)&&x.Date.Date.Equals(date.Date))
                .Select(_ => new ShowAppointmentDTO {Date=_.Date,DoctorName=_.Doctor.FirstName+" "+_.Doctor.LastName,
                    PatientName=_.Patient.FirstName+" "+_.Patient.LastName }).ToHashSet();
        }

        public Appointment GetById(int id)
        {
            return _appointments.FirstOrDefault();
        }
    }
}
