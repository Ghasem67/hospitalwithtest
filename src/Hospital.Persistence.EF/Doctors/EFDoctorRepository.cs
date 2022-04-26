using Hospital.Entities;
using Hospital.Service.Doctors.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Persistence.EF.Doctors
{
    public class EFDoctorRepository : DoctorRepository
    {
        private readonly DbSet<Doctor> _doctors;

        public EFDoctorRepository(EFDbContext dbContext)
        {
            _doctors = dbContext.Set<Doctor>();
        }

        public void Add(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public void Delete(Doctor doctor)
        {
          _doctors.Remove(doctor);
        }

        public HashSet<ShowDoctorDTO> GetAll()
        {
           return _doctors.Select(_=>new ShowDoctorDTO {
                Id=_.Id,
                Field=_.Field,
                FirstName=_.FirstName,
                LastName=_.LastName,
                NationalCode=_.LastName }).ToHashSet();
        }

        public Doctor GetById(int id)
        {
          return  _doctors.Find(id);
        }
    }
}
