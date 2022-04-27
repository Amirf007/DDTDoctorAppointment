﻿using DDTDoctorAppointment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Test.Tools.Patients
{
    public static class PatientFactory
    {
        public static Patient CreatePatient()
        {
            return new Patient
            {
                NationalCode = "123",
                Name = "amir",
                LastName = "feyzipoor",
            };
        }
    }
}
