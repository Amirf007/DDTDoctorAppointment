using DDDoctorAppointment.Infrastructure.Application;
using DDDoctorAppointment.Infrastructure.Test;
using DDTDoctorAppointment.Entities;
using DDTDoctorAppointment.Persistence.EF;
using DDTDoctorAppointment.Persistence.EF.Patients;
using DDTDoctorAppointment.Services.Patients;
using DDTDoctorAppointment.Services.Patients.Contracts;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DDTDoctorAppointment.Services.Test.Unit.Patients
{
    public class PatientServiceTests
    {
        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly PatientService _sut;
        private readonly PatientRepository _repository;

        public PatientServiceTests()
        {
            _dataContext =
                new EFInMemoryDataBase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new EFPatientRepository(_dataContext);
            _sut = new PatientAppService(_repository, _unitOfWork);
        }

        [Fact]
        public void Add_adds_patient_properly()
        {
            AddPatientDto dto = GenerateAddPatientDto();

            _sut.Add(dto);

            _dataContext.Patients.Should()
                .Contain(_ => _.NationalCode == dto.NationalCode);
        }

        [Fact]
        public void Add_throw_PatientIsAlreadyExistException_When_patient_registered_with_duplicate_nationalcode()
        {
            AddPatientDto dto = GenerateAddPatientDto();

            var patientwithduplicatenationalcode = new Patient
            {
                NationalCode = dto.NationalCode,
                Name = "hgfd",
                LastName = "vfdd"
            };
            patientwithduplicatenationalcode.NationalCode = dto.NationalCode;
            _dataContext.Manipulate(_ => _.Patients.Add(patientwithduplicatenationalcode));

            Action Expected = () => _sut.Add(dto);

            Expected.Should().ThrowExactly<PatientIsAlreadyExistException>();
        }

        [Fact]
        public void GetDoctor_return_doctor_with_Id()
        {
            var doctor = DoctorFactory.CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));

            var Expected = _sut.GetDoctor(doctor.Id);
            Expected.NationalCode.Should().Be(doctor.NationalCode);
            Expected.Name.Should().Be(doctor.Name);
            Expected.LastName.Should().Be(doctor.LastName);
            Expected.Specialty.Should().Be(doctor.Specialty);
        }

        private static AddPatientDto GenerateAddPatientDto()
        {
            return new AddPatientDto
            {
                NationalCode = "456",
                Name = "erfan",
                LastName = "bahme"
            };
        }
    }
}
