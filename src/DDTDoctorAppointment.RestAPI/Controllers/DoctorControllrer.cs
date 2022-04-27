using DDTDoctorAppointment.Services.Doctors.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DDTDoctorAppointment.RestAPI.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorControllrer : ControllerBase
    {
        private readonly DoctorService _service;
        public DoctorControllrer(DoctorService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddDoctorDto dto)
        {
            _service.Add(dto);
        }

        [HttpPut("{id}")]
        public void Update(int id,UpdateDoctorDto dto)
        {
            _service.Update(id, dto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }

        [HttpGet]
        public IList<GetDoctorDto> Getall()
        {
            return _service.Getall();
        }

        [HttpGet("{id}")]
        public GetDoctorDto GetDoctor(int id)
        {
            return _service.GetDoctor(id);
        }
    }
}
