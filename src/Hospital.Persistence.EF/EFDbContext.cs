using Hospital.Entities;
using Hospital.Persistence.EF.Doctors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Persistence.EF
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(string connectionString) :
              this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        { }

        public EFDbContext( DbContextOptions options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(DoctorEntityMap).Assembly);
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}
