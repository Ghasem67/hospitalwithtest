using Hospital.Entities;
using Hospital.Services.Patients.Constracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Test.Tools.Patients
{
    public static class PatientFactory
    {
        public static Patient CreatPatient(AddPatientDTO AddPatientDTO)
        {
            return new Patient
            {
                NationalCode = AddPatientDTO.NationalCode,
                LastName= AddPatientDTO.LastName,
                FirstName= AddPatientDTO.FirstName
            };
        }
    }
}
