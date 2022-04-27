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
            AddfullcapacityCheck(addAppointmentDTO);
            AddDuplicateRecordscheck(addAppointmentDTO);
            Appointment appointment = new Appointment
            {
                Date = addAppointmentDTO.Date,
                DoctorId = addAppointmentDTO.DoctorId,
                PatientId = addAppointmentDTO.PatientId
            };
            var duplicaterecord = _appointmentRepository.Get(addAppointmentDTO.DoctorId, addAppointmentDTO.Date, addAppointmentDTO.PatientId);
            if (duplicaterecord > 0)
            {
                throw new ExistenceOfDuplicateRecordsException();
            }
            var appointmentliset = _appointmentRepository.GetAll(addAppointmentDTO.DoctorId, addAppointmentDTO.Date);
            if (appointmentliset.Count() == 4)
            {
                throw new NotInofSpaceException();
            }
            _appointmentRepository.Add(appointment);
            _unitOfWork.commit();
        }

        public void Delete(int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                throw new AppointmentNotFoundException();
            }
            _appointmentRepository.Delete(appointment);
            _unitOfWork.commit();

        }

        public HashSet<ShowAppointmentDTO> GetAll(int doctorid, DateTime date)
        {
            return _appointmentRepository.GetAll(doctorid, date);
        }

        public void Update(UpdateAppointmentDTO updateAppointmentDTO, int id)
        {
            UpdateDuplicateRecordscheck(updateAppointmentDTO);
            UpdatefullcapacityCheck(updateAppointmentDTO);
            var appointment = _appointmentRepository.GetById(id);
            UpdateNullAppointmentCheck(appointment);
           
            appointment.Date = updateAppointmentDTO.Date;
            appointment.DoctorId = updateAppointmentDTO.DoctorId;
            appointment.PatientId = updateAppointmentDTO.PatientId;
            _unitOfWork.commit();
        }
        private void UpdateNullAppointmentCheck(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new AppointmentNotFoundException();
            }
        }
        private void UpdatefullcapacityCheck(UpdateAppointmentDTO updateAppointmentDTO)
        {

            var appointmentliset = _appointmentRepository.GetAll(updateAppointmentDTO.DoctorId, updateAppointmentDTO.Date);
            if (appointmentliset.Count() == 4)
            {
                throw new NotInofSpaceException();
            }
        }
        private void UpdateDuplicateRecordscheck(UpdateAppointmentDTO updateAppointmentDTO)
        {
            var duplicaterecord = _appointmentRepository.Get(updateAppointmentDTO.DoctorId, updateAppointmentDTO.Date, updateAppointmentDTO.PatientId);
            if (duplicaterecord > 0)
            {
                throw new ExistenceOfDuplicateRecordsException();
            }
        }
        private void AddfullcapacityCheck(AddAppointmentDTO updateAppointmentDTO)
        {

            var appointmentliset = _appointmentRepository.GetAll(updateAppointmentDTO.DoctorId, updateAppointmentDTO.Date);
            if (appointmentliset.Count() == 4)
            {
                throw new NotInofSpaceException();
            }
        }
        private void AddDuplicateRecordscheck(AddAppointmentDTO updateAppointmentDTO)
        {
            var duplicaterecord = _appointmentRepository.Get(updateAppointmentDTO.DoctorId, updateAppointmentDTO.Date, updateAppointmentDTO.PatientId);
            if (duplicaterecord > 0)
            {
                throw new ExistenceOfDuplicateRecordsException();
            }
        }
    }
}
