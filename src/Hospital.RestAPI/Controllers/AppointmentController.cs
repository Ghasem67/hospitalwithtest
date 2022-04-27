using Hospital.Services.Appointments.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hospital.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpPost]
        public void Add(AddAppointmentDTO addAppointmentDTO)
        {
            _appointmentService.Add(addAppointmentDTO);
        }
        [HttpPost("{id}")]
        public void Update(UpdateAppointmentDTO updateAppointmentDTO, int id)
        {
            _appointmentService.Update(updateAppointmentDTO, id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _appointmentService.Delete(id);
        }
        [HttpGet]
        public HashSet<ShowAppointmentDTO> GetAll()
        {
            return _appointmentService.GetAll();
        }
    }
}
