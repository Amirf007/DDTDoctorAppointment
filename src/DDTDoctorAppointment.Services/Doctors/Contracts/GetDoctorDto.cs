using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Services.Doctors.Contracts
{
    public class GetDoctorDto
    {
        public int Id { get; set; }
        public string NationalCode { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
    }
}
