using DDTDoctorAppointment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Test.Tools.Doctors
{
    public static class DoctorFactory
    {
        public static Doctor CreateDoctor()
        {
            return new Doctor
            {
                NationalCode = "123",
                Name = "amir",
                LastName = "bahme",
                Specialty = "heart"
            };
        }
    }
}
