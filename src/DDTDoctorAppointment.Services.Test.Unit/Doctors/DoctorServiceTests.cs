using DDDoctorAppointment.Infrastructure.Application;
using DDDoctorAppointment.Infrastructure.Test;
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
