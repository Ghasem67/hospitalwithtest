using Hospital.Entities;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Application;
using Hospital.Services.Doctors.Contracts;
using Hospital.Services.Doctors.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Doctors
{
    public class DoctorAppService : DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly UnitOfWork _unitOfWork;
        public DoctorAppService(UnitOfWork unitOfWork, DoctorRepository doctorRepository)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
        }

        public void Add(AddDoctorDTO addDoctorDTO)
        {
            Doctor doctor = new Doctor
            {
                FirstName = addDoctorDTO.FirstName,
                LastName= addDoctorDTO.LastName,
                Field= addDoctorDTO.Field,
                NationalCode= addDoctorDTO.NationalCode,
                
            };
            _doctorRepository.Add(doctor);
            _unitOfWork.commit();
        }

        public void Delete(int id)
        {
           var doctor= _doctorRepository.GetById(id);
            if (doctor==null)
            {

            }
            _doctorRepository.Delete(doctor);
            _unitOfWork.commit();
        }

        public HashSet<ShowDoctorDTO> GetAll()
        {
           return _doctorRepository.GetAll();
        }

        public void Update(UpdateDoctorDTO updateDoctorDTO,int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                throw new DoctorNotFoundException();
            }
            doctor.FirstName = updateDoctorDTO.FirstName;
            doctor.LastName = updateDoctorDTO.LastName;
            doctor.Field= updateDoctorDTO.Field;
            doctor.NationalCode= updateDoctorDTO.NationalCode;
            _unitOfWork.commit();
        }
    }
}
