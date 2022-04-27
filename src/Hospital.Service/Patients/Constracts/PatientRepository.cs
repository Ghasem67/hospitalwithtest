using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Patients.Constracts
{
    public interface PatientRepository
    {
        void Add(Patient patient);
        void Delete(Patient patient);
        Patient GetById(int id);
        HashSet<ShowPatientDTO> GetAll();
    }
}
