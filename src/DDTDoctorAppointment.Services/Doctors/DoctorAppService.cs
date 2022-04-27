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

        public void Update(int id, UpdateDoctorDto dto)
        {
            var doctor = _repository.Getbyid(id);
            if (doctor == null)
            {
                throw new DoctorNotFoundException();
            }

            var IsExist = _repository
                .IsExistNationalCode(dto.NationalCode, doctor.Id);
            if (IsExist)
            {
                throw new DoctorIsAlreadyExistException();
            }
           
                doctor.Name = dto.Name;
                doctor.LastName = dto.LastName;
                doctor.NationalCode = dto.NationalCode;
                doctor.Specialty = dto.Specialty;

                _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var doctor = _repository.Getbyid(id);
            if (doctor==null)
            {
                throw new DoctorNotFoundException();
            }

            _repository.Remove(doctor);

            _unitOfWork.Commit();
        }

        public IList<GetDoctorDto> Getall()
        {
           return _repository.Getall();
        }

        public GetDoctorDto GetDoctor(int id)
        {
           var doctor = _repository.GetDoctor(id);
            if (doctor==null)
            {
                throw new DoctorNotFoundException();
            }

            return doctor;
        }
    }
}
