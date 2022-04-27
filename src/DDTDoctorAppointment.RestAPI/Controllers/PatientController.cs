using DDTDoctorAppointment.Services.Patients.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DDTDoctorAppointment.RestAPI.Controllers
{

    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _service;
        public PatientController(PatientService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddPatientDto dto)
        {
            _service.Add(dto);
        }

        [HttpGet("{id}")]
        public GetPatientDto GetPatient(int id)
        {
           return _service.GetPatient(id);
        }

        [HttpGet]
        public IList<GetPatientDto> Getall()
        {
            return _service.Getall();
        }

        [HttpPut("{id}")]
        public void Update(int id,UpdatePatientDto dto)
        {
            _service.Update(id,dto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
