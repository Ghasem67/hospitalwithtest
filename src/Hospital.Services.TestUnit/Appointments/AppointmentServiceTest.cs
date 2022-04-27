using FluentAssertions;
using Hospital.Entities;
using Hospital.Infrastructure.Test;
using Hospital.Persistence.EF;
using Hospital.Persistence.EF.Appointments;
using Hospital.Services.Appointments;
using Hospital.Services.Appointments.Contracts;
using Hospital.Services.Doctors.Contracts;
using Hospital.Services.Patients.Constracts;
using Hospital.Test.Tools.Appointments;
using Hospital.Test.Tools.Doctors;
using Hospital.Test.Tools.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hospital.Services.TestUnit.Appointment
{
    public class AppointmentServiceTest
    {
        private readonly EFDbContext _dbContext;
        private readonly EFUnitOfWork _unitOfWork;
        private readonly AppointmentService _sut;
        private readonly AppointmentRepository _repository;
        public AppointmentServiceTest()
        {
            _dbContext=new EFInMemoryDataBase()
                .CreateDataContext<EFDbContext>();
            _repository=new EFAppointmentRepository(_dbContext);
            _unitOfWork=new EFUnitOfWork(_dbContext);
            _sut=new AppointmentAppService(_repository, _unitOfWork);
        }
        [Fact]
        public void Add_adds_Appointment_Properly()
        {
           
            var Patient = GeneratePatientDTO();
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));
            var doctor = GenerateDoctorDTO();
            _dbContext.Manipulate(_ => _.Doctors.Add(doctor));
            AddAppointmentDTO dto = new AddAppointmentDTO()
            {
                Date= DateTime.Now,
                DoctorId=doctor.Id,
                PatientId=Patient.Id
            };
            
            _sut.Add(dto);
            _dbContext.Appointments.Should().Contain(_ => _.Date == dto.Date);
            _dbContext.Appointments.Should().Contain(_ => _.DoctorId == dto.DoctorId);
            _dbContext.Appointments.Should().Contain(_ => _.PatientId == dto.PatientId);

        }
        [Fact]
        public void Delete_delets_One_Appointment()
        {
            var Patient = GeneratePatientDTO();
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));
            var doctor = GenerateDoctorDTO();
            _dbContext.Manipulate(_ => _.Doctors.Add(doctor));
            var addAppointmentDTO = new AddAppointmentDTO()
            {
                Date=DateTime.Now.Date,
                DoctorId= doctor.Id,
                PatientId= Patient.Id
            };
            var appointment=AppointmentFactory.CreateAppointment(addAppointmentDTO);
            _dbContext.Manipulate(_ => _.Appointments.Add(appointment));


            _sut.Delete(appointment.Id);

            _dbContext.Appointments.Should().HaveCount(0);
        }

        private static Patient GeneratePatientDTO()
        {
            var addPatient = new AddPatientDTO()
            {
                FirstName = "Ali",
                LastName = "abbasi",
                NationalCode = "12345",
            };
            return PatientFactory.CreatPatient(addPatient);
        }
        private static Doctor GenerateDoctorDTO()
        {
            var adddoctor = new AddDoctorDTO()
            {
                FirstName = "Ali",
                LastName = "abbasi",
                NationalCode = "12345",
                Field = "alaki",
            };
            return DoctorFactory.CreatDoctor(adddoctor);
        }
            private static AddAppointmentDTO GenerateAddAppointmentDTO()
        {
            return new AddAppointmentDTO()
            {
                Date = DateTime.Now,
            };
        }
    }
}
