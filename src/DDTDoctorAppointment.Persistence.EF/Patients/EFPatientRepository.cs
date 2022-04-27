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
        private EFDataContext dataContext;

        public EFPatientRepository(EFDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
