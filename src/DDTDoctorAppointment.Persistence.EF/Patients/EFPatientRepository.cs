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

        public IList<GetPatientDto> Getall()
        {
            return _dataContext.Patients
                  .Select(_ => new GetPatientDto
                  {
                      NationalCode = _.NationalCode,
                      Name = _.Name,
                      LastName = _.LastName,

                  }).ToList();
        }

        public GetPatientDto GetPatient(int id)
        {
            return _dataContext.Patients.Where(_ => _.Id == id).Select(_ => new GetPatientDto
            {
                NationalCode = _.NationalCode,
                Name = _.Name,
                LastName = _.LastName,

            }).SingleOrDefault();
        }

        public bool IsExistNationalCode(string nationalCode, int id)
        {
            return _dataContext.Patients
                .Any(_ => _.NationalCode == nationalCode && _.Id != id);
        }
    }
}
