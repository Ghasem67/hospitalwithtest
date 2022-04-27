using Hospital.Entities;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Application;
using Hospital.Services.Patients.Constracts;
using Hospital.Services.Patients.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Patients
{
    public class PatientAppService: PatientService
    {
        private readonly PatientRepository _patientRepository;
        private readonly UnitOfWork _unitOfWork;
        public PatientAppService(PatientRepository patientRepository, UnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }
        public void Add(AddPatientDTO addPatientDTO)
        {
            Patient patient = new Patient
            {
                FirstName = addPatientDTO.FirstName,
                LastName = addPatientDTO.LastName,
                NationalCode = addPatientDTO.NationalCode,

            };
            _patientRepository.Add(patient);
            _unitOfWork.commit();
        }
        public void Delete(int id)
        {
            var patient = _patientRepository.GetById(id);
            if (patient == null)
            {
                throw new PatientNotFoundException();
            }
            _patientRepository.Delete(patient);
            _unitOfWork.commit();
        }
        public HashSet<ShowPatientDTO> GetAll()
        {
            return _patientRepository.GetAll();
        }

        public void Update(UpdatePatientDTO updatePatientDTO, int id)
        {
            var patient = _patientRepository.GetById(id);
            if (patient == null)
            {
                throw new PatientNotFoundException();
            }
            patient.FirstName = updatePatientDTO.FirstName;
            patient.LastName = updatePatientDTO.LastName;
            patient.NationalCode = updatePatientDTO.NationalCode;
            _unitOfWork.commit();
        }
    }
}
