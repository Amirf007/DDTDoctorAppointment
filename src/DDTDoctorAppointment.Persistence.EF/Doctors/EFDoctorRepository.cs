using DDTDoctorAppointment.Entities;
using DDTDoctorAppointment.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Persistence.EF.Doctors
{
    public class EFDoctorRepository : DoctorRepository
    {
        private EFDataContext _dataContext;

        public EFDoctorRepository(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Doctor doctor)
        {
            _dataContext.Doctors.Add(doctor);
        }

        public IList<GetDoctorDto> Getall()
        {
            return _dataContext.Doctors
                 .Select(_ => new GetDoctorDto
                 {
                     NationalCode = _.NationalCode,
                     Name = _.Name,
                     LastName = _.LastName,
                     Specialty = _.Specialty,
                 }).ToList();
        }

        public Doctor Getbyid(int id)
        {
           return _dataContext.Doctors.Find(id);
        }

        public GetDoctorDto GetDoctor(int id)
        {
            return _dataContext.Doctors.Where(_ => _.Id == id).Select(_ => new GetDoctorDto
            {
                NationalCode = _.NationalCode,
                Name = _.Name,
                LastName = _.LastName,
                Specialty = _.Specialty,

            }).SingleOrDefault();
        }

        public bool IsExistNationalCode(string nationalCode, int id)
        {
            return _dataContext.Doctors
                .Any(_ => _.NationalCode == nationalCode && _.Id != id);
        }

        public void Remove(Doctor doctor)
        {
            _dataContext.Doctors.Remove(doctor);
        }
    }
}
