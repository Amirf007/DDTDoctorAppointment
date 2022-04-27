using DDDoctorAppointment.Infrastructure.Application;
using DDTDoctorAppointment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Services.Patients.Contracts
{
    public interface PatientRepository : Repository
    {
        void Add(Patient patient);
        bool IsExistNationalCode(string nationalCode, int id);
        GetPatientDto GetPatient(int id);
        IList<GetPatientDto> Getall();
        Patient Getbyid(int id);
        void Remove(Patient patient);
    }
}
