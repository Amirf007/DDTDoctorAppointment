using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Services.Patients.Contracts
{
    public class GetPatientDto
    {
        public int Id { get; set; }
        public string NationalCode { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
