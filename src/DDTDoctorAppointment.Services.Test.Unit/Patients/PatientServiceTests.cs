using DDDoctorAppointment.Infrastructure.Application;
using DDDoctorAppointment.Infrastructure.Test;
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
            AddPatientDto dto = GeneratePatientDto();

            _sut.Add(dto);

            _dataContext.Patients.Should()
                .Contain(_ => _.NationalCode == dto.NationalCode);
        }

        private static AddPatientDto GeneratePatientDto()
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
