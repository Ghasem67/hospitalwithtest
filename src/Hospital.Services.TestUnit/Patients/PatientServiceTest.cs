using FluentAssertions;
using Hospital.Entities;
using Hospital.Infrastructure.Application;
using Hospital.Infrastructure.Test;
using Hospital.Persistence.EF;
using Hospital.Persistence.EF.Patients;
using Hospital.Services.Doctors.Contracts;
using Hospital.Services.Patients;
using Hospital.Services.Patients.Constracts;
using Hospital.Test.Tools.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hospital.Services.TestUnit.Patients
{
    public class PatientServiceTest
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly EFDbContext _context;
        private readonly PatientService _sut;
        private readonly PatientRepository _repository;

        public PatientServiceTest()
        {
            _context = new EFInMemoryDataBase()
                   .CreateDataContext<EFDbContext>();
            _repository = new EFPatientRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new PatientAppService(_repository, _unitOfWork);

        }
        [Fact]
        public void Add_Adds_Patient_properly()
        {
            AddPatientDTO dto = GenerateAddPatientDTO();


            _sut.Add(dto);



            _context.Patients.Should().Contain(_ => _.FirstName == dto.FirstName);
            _context.Patients.Should().Contain(_ => _.LastName == dto.LastName);
            _context.Patients.Should().Contain(_ => _.NationalCode == dto.NationalCode);
        }
        [Fact]
        private void Update_updates_Patient_properly()
        {
            var addPatient = new AddPatientDTO()
            {
                FirstName = "Ali",
                LastName = "abbasi",
                NationalCode = "12345",
            };
            var Patient = PatientFactory.CreatPatient(addPatient);
            _context.Manipulate(_ => _.Patients.Add(Patient));
            var dto = GenerateUpdateCategoryDto("abbas", "mohammadi", "123454321");


            _sut.Update(dto, Patient.Id);


            var expected = _context.Patients
              .FirstOrDefault(_ => _.Id == Patient.Id);
            expected.FirstName.Should().Be(dto.FirstName);
            expected.LastName.Should().Be(dto.LastName);
            expected.NationalCode.Should().Be(dto.NationalCode);
        }
        [Fact]
        public void Delete_Delete_One_Patient()
        {
            var adddoctor = new AddPatientDTO()
            {
                FirstName = "Ali",
                LastName = "abbasi",
                NationalCode = "12345",

            };
            var patient = PatientFactory.CreatPatient(adddoctor);
            _context.Manipulate(_ => _.Patients.Add(patient));


            _sut.Delete(patient.Id);


            var expected = _context.Doctors.Should().HaveCount(0);
        }

        [Fact]
        private void GetAll_getalls_returns_all_Patient()
        {
            CreatePatientInDataBase();


            var expected = _sut.GetAll();


            expected.Should().HaveCount(3);

           // expected.Should().Contain(_ => _.FirstName == "Ali");
            //expected.Should().Contain(_ => _.LastName == "abbasi");
            //expected.Should().Contain(_ => _.FirstName == "abbas");
            //expected.Should().Contain(_ => _.LastName == "navabi");
        }

        private void CreatePatientInDataBase()
        {
           var patients = new HashSet<Patient>
            {
                new Patient{FirstName="Ali",LastName="abbasi",NationalCode="1234567890"},
                new Patient{FirstName="abbas",LastName="navabi",NationalCode="1234565432" },
                new Patient{FirstName="saeed",LastName="amiri",NationalCode="2134567890" }
            };
            _context.Manipulate(_ =>
          _.Patients.AddRange(patients));
        }

        private static AddPatientDTO GenerateAddPatientDTO()
        {
            return new AddPatientDTO
            {
                FirstName = "Ahmad",
                LastName = "Behrouzi",
                NationalCode = "2300987654"
            };
        }
        private static UpdatePatientDTO GenerateUpdateCategoryDto(string firstName,
          string lastName,
          string NationalCode)
        {
            return new UpdatePatientDTO
            {
                FirstName = firstName,
                LastName = lastName,
                NationalCode = NationalCode
            };
        }
    }
}
