using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public string DoctorId { get; set; }
        public Patient Patient { get; set; }
        public string PatientId { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}
