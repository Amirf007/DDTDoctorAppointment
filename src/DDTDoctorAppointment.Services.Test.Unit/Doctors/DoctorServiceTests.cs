using DDDoctorAppointment.Infrastructure.Application;
using DDDoctorAppointment.Infrastructure.Test;
using DDTDoctorAppointment.Entities;
using DDTDoctorAppointment.Persistence.EF;
using DDTDoctorAppointment.Persistence.EF.Doctors;
using DDTDoctorAppointment.Services.Doctors;
using DDTDoctorAppointment.Services.Doctors.Contracts;
using DDTDoctorAppointment.Test.Tools.Doctors;
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
            Doctor doctor = DoctorFactory.CreateDoctor();
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
            var doctor = DoctorFactory.CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));

            var doctorwithduplicatenationalcode = DoctorFactory.CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctorwithduplicatenationalcode));

            UpdateDoctorDto dto = GenerateUpdareDoctorDto();
            dto.NationalCode = doctor.NationalCode;

            Action Expected = () => _sut.Update(doctor.Id, dto);
            Expected.Should().ThrowExactly<DoctorIsAlreadyExistException>();
        }

        [Fact]
        public void Delete_delete_doctor_properly()
        {
            Doctor doctor = DoctorFactory.CreateDoctor();
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

        [Fact]
        public void Getall_return_all_doctors_properly()
        {
            CreateDoctorsInDataBase();

            var Expected = _sut.Getall();
            Expected.Should().HaveCount(2);
            Expected.Should().Contain(_ => _.NationalCode == "125");
            Expected.Should().Contain(_ => _.NationalCode == "458");
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

        [Fact]
        public void GetDoctor_throw_DoctorNotFoundException_when_doctor_that_you_want_return_given_id_that_not_exist()
        {
            var dummyid = 102;

            Action Expected = () => _sut.GetDoctor(dummyid);
            Expected.Should().ThrowExactly<DoctorNotFoundException>();
        }

        private void CreateDoctorsInDataBase()
        {
            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    Name = "amir",
                    LastName = "zare",
                    NationalCode = "125",
                    Specialty = "heart",
                },
                new Doctor
                {
                    Name = "abolfazl",
                    LastName = "bahme",
                    NationalCode = "458",
                    Specialty = "heart",
                }
            };
            _dataContext.Manipulate(_ => _.Doctors.AddRange(doctors));
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
