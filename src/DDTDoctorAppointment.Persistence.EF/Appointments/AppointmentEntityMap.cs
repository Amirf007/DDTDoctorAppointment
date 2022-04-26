﻿using DDTDoctorAppointment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTDoctorAppointment.Persistence.EF.Appointments
{
    internal class AppointmentEntityMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> _)
        {
            _.ToTable("Appointments");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            _.Property(_ => _.AppointmentTime)
                .IsRequired();

            _.Property(_ => _.DoctorId)
                .IsRequired();

            _.Property(_ => _.PatientId)
                .IsRequired();

            _.HasOne(_ => _.Doctor)
                .WithMany(_ => _.Appointments)
                .HasForeignKey(_ => _.DoctorId);

            _.HasOne(_ => _.Patient)
                .WithMany(_ => _.Appointments)
                .HasForeignKey(_ => _.PatientId);
        }
    }
}