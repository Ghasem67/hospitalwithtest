using Hospital.Entities;
using Hospital.Infrastructure;
using Hospital.Service.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Doctors
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

            }
            doctor.FirstName = doctor.FirstName;
            doctor.LastName = doctor.LastName;
            doctor.Field= doctor.Field;
            doctor.NationalCode= doctor.NationalCode;
            _unitOfWork.commit();
        }
    }
}
