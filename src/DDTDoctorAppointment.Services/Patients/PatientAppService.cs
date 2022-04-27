using DDDoctorAppointment.Infrastructure.Application;
using DDTDoctorAppointment.Entities;
using DDTDoctorAppointment.Services.Patients.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Services.Patients
{
    public class PatientAppService : PatientService
    {
        private PatientRepository _repository;
        private UnitOfWork _unitOfWork;

        public PatientAppService(PatientRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddPatientDto dto)
        {
            Patient patient = GeneratePatient(dto);
            var isPatientExist = _repository
               .IsExistNationalCode(patient.NationalCode, patient.Id);
            if (isPatientExist)
            {
                throw new PatientIsAlreadyExistException();
            }

            _repository.Add(patient);

            _unitOfWork.Commit();

        }

        private static Patient GeneratePatient(AddPatientDto dto)
        {
            return new Patient
            {
                NationalCode = dto.NationalCode,
                Name = dto.Name,
                LastName = dto.LastName,
            };
        }

        public GetPatientDto GetPatient(int id)
        {
            var patient = _repository.GetPatient(id);
            if (patient==null)
            {
                throw new PatientNotFoundException();
            }

            return patient;
        }

        public IList<GetPatientDto> Getall()
        {
           return _repository.Getall();
        }

        public void Update(int id, UpdatePatientDto dto)
        {
            var patient = _repository.Getbyid(id);
            if (patient==null)
            {
                throw new PatientNotFoundException();
            }

            var IsExist = _repository
                .IsExistNationalCode(dto.NationalCode, patient.Id);
            if (IsExist)
            {
                throw new PatientIsAlreadyExistException();
            }

            patient.Name = dto.Name;
            patient.LastName = dto.LastName;
            patient.NationalCode = dto.NationalCode;

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
           var patient = _repository.Getbyid(id);
            if (patient==null)
            {
                throw new PatientNotFoundException();
            }

            _repository.Remove(patient);

            _unitOfWork.Commit();
        }
    }
}
