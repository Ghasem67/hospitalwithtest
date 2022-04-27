using Hospital.Entities;
using Hospital.Services.Patients.Constracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Persistence.EF.Patients
{
    public class EFPatientRepository:PatientRepository
    {
        private readonly DbSet<Patient> _patients;

        public EFPatientRepository(EFDbContext dbContext)
        {
            _patients = dbContext.Set<Patient>();
        }

        public void Add(Patient patient)
        {
            _patients.Add(patient);
        }

        public void Delete(Patient patient)
        {
            _patients.Remove(patient);
        }

        public HashSet<ShowPatientDTO> GetAll()
        {
            return _patients.Select(_ => new ShowPatientDTO
            {
                Id = _.Id,
                LastName = _.LastName,
                NationalCode = _.LastName
            }).ToHashSet();
        }

        public Patient GetById(int id)
        {
            return _patients.Find(id);
        }
    }
}
