using DDDoctorAppointment.Infrastructure.Application;
using DDTDoctorAppointment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Services.Doctors.Contracts
{
    public interface DoctorRepository : Repository
    {
        void Add(Doctor doctor);
        bool IsExistNationalCode(string nationalCode, int id);
        Doctor Getbyid(int id);
        void Remove(Doctor doctor);
        IList<GetDoctorDto> Getall();
    }
}
