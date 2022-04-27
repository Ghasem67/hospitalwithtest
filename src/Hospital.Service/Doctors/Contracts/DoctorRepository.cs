using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Doctors.Contracts
{
    public interface DoctorRepository
    {
        void Add(Doctor doctor);
        void Delete(Doctor doctor);
        Doctor GetById(int id);
        HashSet<ShowDoctorDTO> GetAll();

    }
}
