using Hospital.Entities;
using Hospital.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Test.Tools.Doctors
{
    public static class DoctorFactory
    {
        public static Doctor CreatDoctor(AddDoctorDTO addDoctorDTO)
        {
            return new Doctor
            {
                Field = addDoctorDTO.Field,
                NationalCode = addDoctorDTO.NationalCode,
                LastName=addDoctorDTO.LastName,
                FirstName=addDoctorDTO.FirstName
            };
        }
    }
}
