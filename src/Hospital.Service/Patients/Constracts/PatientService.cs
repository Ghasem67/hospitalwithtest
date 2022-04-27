using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Patients.Constracts
{
    public interface PatientService
    {
        void Add(AddPatientDTO addDoctorDTO);
        void Update(UpdatePatientDTO updateDoctorDTO, int id);
        void Delete(int id);
        HashSet<ShowPatientDTO> GetAll();
    }
}
