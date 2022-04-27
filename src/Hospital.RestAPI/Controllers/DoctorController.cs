using Hospital.Services.Doctors.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hospital.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorservice;

        public DoctorController(DoctorService doctorService)
        {
            _doctorservice = doctorService;
        }
        [HttpPost]
        public void Add(AddDoctorDTO addDoctorDTO)
        {
            _doctorservice.Add(addDoctorDTO);
        }
        [HttpPost("{id}")]
        public void Update(UpdateDoctorDTO updateDoctorDTO,int id)
        {
            _doctorservice.Update(updateDoctorDTO,id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _doctorservice.Delete(id);
        }
        [HttpGet]
        public HashSet<ShowDoctorDTO> GetAll()
        {
            return _doctorservice.GetAll();
        } 
    }
}
