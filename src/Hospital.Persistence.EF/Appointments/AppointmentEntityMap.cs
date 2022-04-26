using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.EF.Appointments
{
    public class AppointmentEntityMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
           
          builder.HasKey(x=>x.Id);
            builder.Property(_ => _.Id)
                  .ValueGeneratedOnAdd();

            builder.Property(_ => _.Date)
                .IsRequired();

            builder.HasOne(_ => _.Doctor)
                .WithMany(_ => _.Appointments)
                .HasForeignKey(_ => _.DoctorId);

            builder.HasOne(_=>_.Patient)
                .WithMany(_=>_.Appointments)
                .HasForeignKey(_=>_.PatientId);
        }
    }
}
