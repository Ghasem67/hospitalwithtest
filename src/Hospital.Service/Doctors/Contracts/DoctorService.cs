using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Doctors.Contracts
{
    public interface DoctorService
    {
        void Add(AddDoctorDTO addDoctorDTO);
        void Update(UpdateDoctorDTO updateDoctorDTO, int id);
        void Delete(int id);
        HashSet<ShowDoctorDTO> GetAll();
    }
}
