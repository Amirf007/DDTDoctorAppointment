using DDDoctorAppointment.Infrastructure.Application;
using DDTDoctorAppointment.Entities;
using DDTDoctorAppointment.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Services.Doctors
{
    public class DoctorAppService : DoctorService
    {
        private DoctorRepository _repository;
        private UnitOfWork _unitOfWork;

        public DoctorAppService(DoctorRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddDoctorDto dto)
        {
            Doctor doctor = GenerateDoctor(dto);

            var isDoctorExist = _repository
               .IsExistNationalCode(doctor.NationalCode, doctor.Id);
            if (isDoctorExist)
            {
                throw new DoctorIsAlreadyExistException();
            }

            _repository.Add(doctor);

            _unitOfWork.Commit();
        }

        private static Doctor GenerateDoctor(AddDoctorDto dto)
        {
            return new Doctor
            {
                NationalCode = dto.NationalCode,
                Name = dto.Name,
                LastName = dto.LastName,
                Specialty = dto.Specialty,
            };
        }
    }
}
