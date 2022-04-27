using Hospital.Entities;
using Hospital.Services.Appointments.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Test.Tools.Appointments
{
    public static class AppointmentFactory
    {
        public static Appointment CreateAppointment(AddAppointmentDTO addAppointmentDTO)
        {
            return new Appointment
            {
                Date = addAppointmentDTO.Date,
                DoctorId = addAppointmentDTO.DoctorId,
                PatientId = addAppointmentDTO.PatientId
            };
        }
    }
}
