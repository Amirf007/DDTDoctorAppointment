using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Migrations
{
    [Migration(202204261326)]
    public class _202204261326_initalTables : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey("FK_Appointments_Patients");
            Delete.ForeignKey("FK_Appointments_Doctors");
            Delete.Table("Doctors");
            Delete.Table("Patients");
            Delete.Table("Appointments");
        }

        public override void Up()
        {
            CreateDoctorsTable();
            CreatePatientsTable();
            CreateAppointmentsTable();

            CreateRelationships();
        }

        private void CreateRelationships()
        {
            Create.ForeignKey("FK_Appointments_Patients")
                    .FromTable("Appointments").ForeignColumns("PatientId")
                    .ToTable("Patients").PrimaryColumn("Id")
                    .OnDeleteOrUpdate(System.Data.Rule.Cascade);

            Create.ForeignKey("FK_Appointments_Doctors")
                    .FromTable("Appointments").ForeignColumns("DoctorId")
                    .ToTable("Doctors").PrimaryColumn("Id")
                    .OnDeleteOrUpdate(System.Data.Rule.Cascade);
        }

        private void CreateAppointmentsTable()
        {
            Create.Table("Appointments")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("DoctorId").AsInt32().NotNullable()
                    .WithColumn("PatientId").AsInt32().NotNullable()
                    .WithColumn("AppointmentTime").AsDateTime().NotNullable();
        }

        private void CreatePatientsTable()
        {
            Create.Table("Patients")
                            .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                                .WithColumn("NationalCode").AsString(50).Unique().NotNullable()
                                .WithColumn("Name").AsString(50).NotNullable()
                                .WithColumn("LastName").AsString(50).NotNullable();
        }

        private void CreateDoctorsTable()
        {
            Create.Table("Doctors")
                 .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("NationalCode").AsString(50).Unique().NotNullable()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("Specialty").AsString(50).NotNullable();
        }
    }
}
