using Hospital.Services.Patients.Constracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hospital.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpPost]
        public void Add(AddPatientDTO addPatientDTO)
        {
            _patientService.Add(addPatientDTO);
        }
        [HttpPost("{id}")]
        public void Update(UpdatePatientDTO updatePatientDTO, int id)
        {
            _patientService.Update(updatePatientDTO, id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _patientService.Delete(id);
        }
        [HttpGet]
        public HashSet<ShowPatientDTO> GetAll()
        {
            return _patientService.GetAll();
        }
    }
}
