using DDDoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Services.Doctors.Contracts
{
    public interface DoctorService : Service
    {
        void Add(AddDoctorDto dto);
        void Update(int id, UpdateDoctorDto dto);
        void Delete(int id);
        IList<GetDoctorDto> Getall();
        GetDoctorDto GetDoctor(int id);
    }
}
