using FluentAssertions;
using Hospital.Entities;
using Hospital.Infrastructure.Test;
using Hospital.Persistence.EF;
using Hospital.Persistence.EF.Appointments;
using Hospital.Services.Appointments;
using Hospital.Services.Appointments.Contracts;
using Hospital.Services.Appointments.Exceptions;
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

namespace Hospital.Services.TestUnit.Appointments
{
    public class AppointmentServiceTest
    {
        private readonly EFDbContext _dbContext;
        private readonly EFUnitOfWork _unitOfWork;
        private readonly AppointmentService _sut;
        private readonly AppointmentRepository _repository;
        public AppointmentServiceTest()
        {
            _dbContext = new EFInMemoryDataBase()
                .CreateDataContext<EFDbContext>();
            _repository = new EFAppointmentRepository(_dbContext);
            _unitOfWork = new EFUnitOfWork(_dbContext);
            _sut = new AppointmentAppService(_repository, _unitOfWork);
        }
        [Fact]
        public void Add_adds_Appointment_Properly()
        {

            var Patient = GeneratePatientDTO("abbas","abbasi","12345667890");
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));
            var doctor = GenerateDoctorDTO();
            _dbContext.Manipulate(_ => _.Doctors.Add(doctor));
            AddAppointmentDTO dto = new AddAppointmentDTO()
            {
                Date = DateTime.Now,
                DoctorId = doctor.Id,
                PatientId = Patient.Id
            };

            _sut.Add(dto);


            _dbContext.Appointments.Should().Contain(_ => _.Date == dto.Date);
            _dbContext.Appointments.Should().Contain(_ => _.DoctorId == dto.DoctorId);
            _dbContext.Appointments.Should().Contain(_ => _.PatientId == dto.PatientId);

        }
        [Fact]
        public void Add_adds_appointment_DoNotDuplicate_exception()
        {
            var Patient = GeneratePatientDTO("abbas", "abbasi", "12345667890");
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));
            var doctor = GenerateDoctorDTO();
            _dbContext.Manipulate(_ => _.Doctors.Add(doctor));
            DateTime date = DateTime.Now.Date;
            Appointment dto = new Appointment()
            {
                Date = date,
                DoctorId = doctor.Id,
                PatientId = Patient.Id
            };
            _dbContext.Manipulate(_ => _.Appointments.Add(dto));
            AddAppointmentDTO appointmentdto = new AddAppointmentDTO()
            {
                Date = date,
                DoctorId = doctor.Id,
                PatientId = Patient.Id
            };


          Action except=()=>  _sut.Add(appointmentdto);


            except.Should().Throw<ExistenceOfDuplicateRecordsException>();
        }
        [Fact]
        public void Delete_delets_One_Appointment()
        {
            var Patient = GeneratePatientDTO("ali","amiri","1234567890");
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));
            var doctor = GenerateDoctorDTO();
            _dbContext.Manipulate(_ => _.Doctors.Add(doctor));
            var addAppointmentDTO = new AddAppointmentDTO()
            {
                Date = DateTime.Now.Date,
                DoctorId = doctor.Id,
                PatientId = Patient.Id
            };
            var appointment = AppointmentFactory.CreateAppointment(addAppointmentDTO);
            _dbContext.Manipulate(_ => _.Appointments.Add(appointment));


            _sut.Delete(appointment.Id);

            _dbContext.Appointments.Should().HaveCount(0);
        }
        [Fact]
        private void GetAll_getalls_returns_all_Patient()
        {
            var doctor = GenerateDoctorDTO();
            _dbContext.Manipulate(_ => _.Doctors.Add(doctor));

           generate4appointment(doctor.Id);

            var expected = _sut.GetAll(doctor.Id,DateTime.Now.Date);


            expected.Should().HaveCount(4);
        }
        [Fact]
        public void Appointment_check_under5()
        {
            var doctor = GenerateDoctorDTO();
            _dbContext.Manipulate(_ => _.Doctors.Add(doctor));

            generate4appointment(doctor.Id);
            var Patient = GeneratePatientDTO("ali", "abbasi", "1234557890");
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));
            var addAppointmentDTO = new AddAppointmentDTO()
            {
                Date = DateTime.Now.Date,
                DoctorId = doctor.Id,
                PatientId = Patient.Id
            };

            Action excpt=()=> _sut.Add(addAppointmentDTO);

            
            excpt.Should().ThrowExactly<NotInofSpaceException>();
        }
        private void generate4appointment(int doctorid)
        {
            List<Appointment> appointments = new List<Appointment>();
            var Patient = GeneratePatientDTO("ali", "amiri", "1234567890");
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));

            appointments.Add(  new Appointment { Date = DateTime.Now.Date, DoctorId = doctorid, PatientId = Patient.Id });
             Patient = GeneratePatientDTO("abbas", "amiri", "1234567990");
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));

            appointments.Add(new Appointment { Date = DateTime.Now.Date, DoctorId = doctorid, PatientId = Patient.Id });
            Patient = GeneratePatientDTO("ali", "amiri", "2234567990");
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));

            appointments.Add(new Appointment { Date = DateTime.Now.Date, DoctorId = doctorid, PatientId = Patient.Id });
            Patient = GeneratePatientDTO("reza", "amiri", "3234567990");
            _dbContext.Manipulate(_ => _.Patients.Add(Patient));

            appointments.Add(new Appointment { Date = DateTime.Now.Date, DoctorId = doctorid, PatientId = Patient.Id });
            _dbContext.Manipulate(_ => _.Appointments.AddRange(appointments));

           

        }


        private static Patient GeneratePatientDTO(string firstName,string lastname,string NationalCode)
        {
            var addPatient = new AddPatientDTO()
            {
                FirstName = firstName,
                LastName = lastname,
                NationalCode = NationalCode,
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

    }
}
