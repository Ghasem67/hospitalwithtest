using Hospital.Infrastructure;
using Hospital.Persistence.EF;
using Hospital.Persistence.EF.Doctors;
using Hospital.Service.Doctors;
using Hospital.Service.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Hospital.Entities;
using Hospital.Test.Tools.Doctors;
using Hospital.Service.Doctors.Exceptions;

namespace Hospital.Services.TestUnit.Doctors
{
    public class DoctorServiceTest
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly EFDbContext _context;
        private readonly DoctorService _sut;
        private readonly DoctorRepository _repository;

        public DoctorServiceTest()
        {
            _repository = new EFDoctorRepository(_context);
            _sut = new DoctorAppService(_unitOfWork, _repository);
            _context = new EFInMemoryDatabase()
                .CreateDataContext<EFDbContext>();
            _unitOfWork = new EFUnitOfWork(_context);
        }

        [Fact]
        public void Add_Adds_Doctor_properly()
        {
            AddDoctorDTO dto = GenerateAddDoctorDTO();


            _sut.Add(dto);


            _context.Doctors.Should().Contain(_ => _.NationalCode == dto.NationalCode);
            _context.Doctors.Should().Contain(_ => _.FirstName == dto.FirstName);
            _context.Doctors.Should().Contain(_ => _.LastName == dto.LastName);
            _context.Doctors.Should().Contain(_ => _.Field == dto.Field);
        }
        [Fact]
        public void GetAll_returns_all_Doctor()
        {
            CreateDoctorInDataBase();

            var expected = _sut.GetAll();

            expected.Should().HaveCount(3);
            expected.Should().Contain(_ => _.FirstName == "name1");
            expected.Should().Contain(_ => _.LastName == "lastname1");
            expected.Should().Contain(_ => _.NationalCode == "nationalcode1");
            expected.Should().Contain(_ => _.Field == "field2");
            expected.Should().Contain(_ => _.FirstName == "name2");
            expected.Should().Contain(_ => _.LastName == "lastname2");
            expected.Should().Contain(_ => _.NationalCode == "nationalcode2");
            expected.Should().Contain(_ => _.Field == "field2");
            expected.Should().Contain(_ => _.FirstName == "name3");
            expected.Should().Contain(_ => _.LastName == "lastname3");
            expected.Should().Contain(_ => _.NationalCode == "nationalcode3");
            expected.Should().Contain(_ => _.Field == "field3");
        }

        [Fact]
        private void Update_updates_Doctor_properly()
        {
            var adddoctor = new AddDoctorDTO()
            {
                FirstName = "Ali",
                LastName = "abbasi",
                NationalCode = "12345",
                Field = "alaki",
            };
            var doctor   = DoctorFactory.CreatDoctor(adddoctor);

            _context.Manipulate(_ => _.Doctors.Add(doctor));
            var dto = GenerateUpdateCategoryDto("Ali","mohammadi","123454321","omumi");
            _sut.Update( dto, doctor.Id);
            var expected = _context.Doctors
              .FirstOrDefault(_ => _.Id == doctor.Id);
            expected.FirstName.Should().Be(dto.FirstName);
            expected.LastName.Should().Be(dto.LastName);
            expected.NationalCode.Should().Be(dto.NationalCode);
            expected.Field.Should().Be(dto.Field);
        }

        [Fact]
        public void Update_throw_CategoryNotFoundException_when_category_with_given_id_is_not_exist()
        {
            var dummyCategoryId = 1000;
            var dto = GenerateUpdateCategoryDto("abbas","amiri","1234321123","dakheli");

            Action expected = () => _sut.Update(dto, dummyCategoryId);

            expected.Should().ThrowExactly<DoctorNotFoundException>();
        }
        private static AddDoctorDTO GenerateAddDoctorDTO()
        {
            return new AddDoctorDTO
            {
                FirstName = "Ahmad",
                LastName = "Behrouzi",
                Field = "alaki",
                NationalCode = "2300987654"
            };
        }
        private void CreateDoctorInDataBase()
        {
            var categories = new List<Doctor>
            {
                new Doctor { FirstName = "name1",LastName="lastname1",NationalCode="nationalcode1",Field="field1"},
                new Doctor { FirstName = "name2",LastName="lastname1",NationalCode="nationalcode2",Field="field2"},
                new Doctor { FirstName = "name3",LastName="lastname1",NationalCode="nationalcode3",Field="field3"}
            };
            _context.Manipulate(_ =>
            _.Doctors.AddRange(categories));
        }
        private static UpdateDoctorDTO GenerateUpdateCategoryDto(string firstName,
            string lastName,
            string NationalCode,string Field)
        {
            return new UpdateDoctorDTO
            {
               Field = Field,
               FirstName= firstName,
               LastName= lastName,
               NationalCode= NationalCode
            };
        }
    }
}
