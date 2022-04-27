using DDDoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Services.Patients.Contracts
{
    public interface PatientService : Service
    {
        void Add(AddPatientDto dto);
        GetPatientDto GetPatient(int id);
        IList<GetPatientDto> Getall();
        void Update(int id, UpdatePatientDto dto);
        void Delete(int id);
    }
}
