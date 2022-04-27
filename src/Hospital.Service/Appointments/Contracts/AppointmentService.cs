using Hospital.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.Appointments.Contracts
{
    public interface AppointmentService: Service
    {
        void Add(AddAppointmentDTO addAppointmentDTO);
        void Update(UpdateAppointmentDTO updateAppointmentDTO, int id);
        void Delete(int id);
        HashSet<ShowAppointmentDTO> GetAll();
    }
}
