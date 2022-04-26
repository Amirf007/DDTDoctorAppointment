using DDTDoctorAppointment.Services.Doctors.Contracts;
using Microsoft.AspNetCore.Mvc;

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
    }
}
