using Hospital.Entities;
using Hospital.Infrastructure.Application;
using Hospital.Services.Appointments.Contracts;
using Hospital.Services.Appointments.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Appointments
{
    public class AppointmentAppService : AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly UnitOfWork _unitOfWork;
        public AppointmentAppService(AppointmentRepository appointmentRepository, UnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }


        public void Add(AddAppointmentDTO addAppointmentDTO)
        {
            Appointment appointment = new Appointment
            {
                Date = addAppointmentDTO.Date,
                DoctorId = addAppointmentDTO.DoctorId,
                PatientId = addAppointmentDTO.PatientId
            };
            _appointmentRepository.Add(appointment);
            _unitOfWork.commit();
        }

        public void Delete(int id)
        {
            var appointment=_appointmentRepository.GetById(id);
            if (appointment==null)
            {
                throw new AppointmentNotFoundException();
            }
            _appointmentRepository.Delete(appointment);
            _unitOfWork.commit();

        }

        public HashSet<ShowAppointmentDTO> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public void Update(UpdateAppointmentDTO updateAppointmentDTO, int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                throw new AppointmentNotFoundException();
            }
            appointment.Date = updateAppointmentDTO.Date;
            appointment.DoctorId = updateAppointmentDTO.DoctorId;
            appointment.PatientId=updateAppointmentDTO.PatientId;
            _unitOfWork.commit();
        }
    }
}
