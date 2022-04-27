using DDDoctorAppointment.Infrastructure.Application;
using DDDoctorAppointment.Infrastructure.Test;
using DDTDoctorAppointment.Entities;
using DDTDoctorAppointment.Persistence.EF;
using DDTDoctorAppointment.Persistence.EF.Doctors;
using DDTDoctorAppointment.Services.Doctors;
using DDTDoctorAppointment.Services.Doctors.Contracts;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DDTDoctorAppointment.Services.Test.Unit.Doctors
{
    public class DoctorServiceTests
    {
        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly DoctorService _sut;
        private readonly DoctorRepository _repository;

        public DoctorServiceTests()
        {
            _dataContext =
                new EFInMemoryDataBase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new EFDoctorRepository(_dataContext);
            _sut = new DoctorAppService(_repository, _unitOfWork);
        }

        [Fact]
        public void Add_adds_doctor_properly()
        {
            AddDoctorDto dto = GenerateAddDoctorDto();

            _sut.Add(dto);

            _dataContext.Doctors.Should()
                .Contain(_ => _.NationalCode == dto.NationalCode);
        }

        [Fact]
        public void Add_throw_DoctorIsAlreadyExistException_When_doctor_registered_with_duplicate_nationalcode()
        {
            AddDoctorDto dto = GenerateAddDoctorDto();

            _sut.Add(dto);

            Action Expected = () => _sut.Add(dto);
            Expected.Should().ThrowExactly<DoctorIsAlreadyExistException>();
        }

        [Fact]
        public void Update_update_doctor_properly()
        {
            Doctor doctor = GenerateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));

            UpdateDoctorDto dto = GenerateUpdareDoctorDto();

            _sut.Update(doctor.Id, dto);

            var Expected = _dataContext.Doctors
                .FirstOrDefault(_ => _.Id == doctor.Id);
            Expected.Name.Should().Be(dto.Name);
            Expected.LastName.Should().Be(dto.LastName);
            Expected.NationalCode.Should().Be(dto.NationalCode);
            Expected.Specialty.Should().Be(dto.Specialty);
        }

        [Fact]
        public void Update_throw_DoctorNotFoundException_when_doctor_with_given_id_that_not_exist()
        {
            var dummyid = 100;

            UpdateDoctorDto dto = GenerateUpdareDoctorDto();

            Action Expected = () => _sut.Update(dummyid,dto);
            Expected.Should().ThrowExactly<DoctorNotFoundException>();
        }

        [Fact]
        public void Update_throw_DoctorIsAlreadyExistException_When_doctor_update_with_duplicate_nationalcode_with_different_id()
        {
            var doctor = GenerateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));

            UpdateDoctorDto dto = GenerateUpdareDoctorDto();

            Action Expected = () => _sut.Update(doctor.Id, dto);
            Expected.Should().ThrowExactly<DoctorIsAlreadyExistException>();
        }

        [Fact]
        public void Delete_delete_doctor_properly()
        {
            Doctor doctor = GenerateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));

            _sut.Delete(doctor.Id);

            _dataContext.Doctors.Should().HaveCount(0);
        }

        [Fact]
        public void Delete_throw_DoctorNotFoundException_when_doctor_with_given_id_that_not_exist()
        {
           var dummyid = 100;

            Action Expected = () => _sut.Delete(dummyid);
            Expected.Should().ThrowExactly<DoctorNotFoundException>();
        }

        private static UpdateDoctorDto GenerateUpdareDoctorDto()
        {
            return new UpdateDoctorDto
            {
                NationalCode = "245",
                Name = "ali",
                LastName = "zare",
                Specialty = "heart"
            };
        }

        private static Doctor GenerateDoctor()
        {
            return new Doctor
            {
                NationalCode = "123",
                Name = "amir",
                LastName = "bahme",
                Specialty = "heart"
            };
        }

        private static AddDoctorDto GenerateAddDoctorDto()
        {
            return new AddDoctorDto
            {
                NationalCode = "123",
                Name = "amir",
                LastName = "bahme",
                Specialty = "heart"
            };
        }
    }
}
