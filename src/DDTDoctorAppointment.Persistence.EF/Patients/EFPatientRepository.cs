using DDTDoctorAppointment.Entities;
using DDTDoctorAppointment.Services.Patients.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Persistence.EF.Patients
{
    public class EFPatientRepository : PatientRepository
    {
        private EFDataContext _dataContext;

        public EFPatientRepository(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Patient patient)
        {
            _dataContext.Patients.Add(patient);
        }

        public bool IsExistNationalCode(string nationalCode, int id)
        {
            return _dataContext.Patients
                .Any(_ => _.NationalCode == nationalCode && _.Id != id);
        }
    }
}
