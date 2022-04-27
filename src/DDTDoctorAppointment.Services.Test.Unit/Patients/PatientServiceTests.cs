using DDDoctorAppointment.Infrastructure.Application;
using DDDoctorAppointment.Infrastructure.Test;
using DDTDoctorAppointment.Entities;
using DDTDoctorAppointment.Persistence.EF;
using DDTDoctorAppointment.Persistence.EF.Patients;
using DDTDoctorAppointment.Services.Patients;
using DDTDoctorAppointment.Services.Patients.Contracts;
using DDTDoctorAppointment.Test.Tools.Patients;
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

            var patientwithduplicatenationalcode = PatientFactory.CreatePatient();
            patientwithduplicatenationalcode.NationalCode = dto.NationalCode;
            _dataContext.Manipulate(_ => _.Patients.Add(patientwithduplicatenationalcode));

            Action Expected = () => _sut.Add(dto);

            Expected.Should().ThrowExactly<PatientIsAlreadyExistException>();
        }

        [Fact]
        public void GetPatient_return_patient_with_Id()
        {
            var patient = PatientFactory.CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));

            var Expected = _sut.GetPatient(patient.Id);
            Expected.NationalCode.Should().Be(patient.NationalCode);
            Expected.Name.Should().Be(patient.Name);
            Expected.LastName.Should().Be(patient.LastName);
        }

        [Fact]
        public void GetPatient_throw_PatientNotFoundException_when_patient_that_you_want_return_given_id_that_not_exist()
        {
            var dummyid = 102;

            Action Expected = () => _sut.GetPatient(dummyid);
            Expected.Should().ThrowExactly<PatientNotFoundException>();
        }

        [Fact]
        public void Getall_return_all_doctors_properly()
        {
            GeneratePatientsInDataBase();

            var Expected = _sut.Getall();
            Expected.Should().HaveCount(2);
            Expected.Should().Contain(_ => _.NationalCode == "548");
            Expected.Should().Contain(_ => _.NationalCode == "231");
        }

        [Fact]
        public void Update_update_patient_properly()
        {
            Patient patient = PatientFactory.CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            UpdatePatientDto dto = GenerateUpdatePatientDto();

            _sut.Update(patient.Id, dto);

            var Expected = _dataContext.Patients
                .FirstOrDefault(_ => _.Id == patient.Id);
            Expected.Name.Should().Be(dto.Name);
            Expected.LastName.Should().Be(dto.LastName);
            Expected.NationalCode.Should().Be(dto.NationalCode);
        }

        private static UpdatePatientDto GenerateUpdatePatientDto()
        {
            return new UpdatePatientDto
            {
                NationalCode = "123",
                Name = "amir",
                LastName = "bahme"
            };
        }

        [Fact]
        public void Update_throw_PatientNotFoundException_when_patient_with_given_id_that_not_exist()
        {
            var dummyid = 100;

            UpdatePatientDto dto = GenerateUpdatePatientDto();

            Action Expected = () => _sut.Update(dummyid, dto);
            Expected.Should().ThrowExactly<PatientNotFoundException>();
        }

        [Fact]
        public void Update_throw_PatientIsAlreadyExistException_When_patient_update_with_duplicate_nationalcode_with_different_id()
        {
            var patient = PatientFactory.CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));

            var patientwithduplicatenationalcode = PatientFactory.CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patientwithduplicatenationalcode));

            UpdatePatientDto dto = GenerateUpdatePatientDto();
            dto.NationalCode = patient.NationalCode;

            Action Expected = () => _sut.Update(patient.Id, dto);
            Expected.Should().ThrowExactly<PatientIsAlreadyExistException>();
        }

        private void GeneratePatientsInDataBase()
        {
            IList<Patient> patients = new List<Patient>
            {
                new Patient
                {
                   NationalCode = "548",
                   Name = "fdt",
                   LastName = "rsrd"
                },
                new Patient
                {
                   NationalCode = "231",
                   Name = "asz",
                   LastName = "wwe"
                }
            };
            _dataContext.Manipulate(_ => _.Patients.AddRange(patients));
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
