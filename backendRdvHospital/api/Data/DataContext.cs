using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;

namespace api.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Doctor> Doctors { get ; set; }
        public DbSet<Patient> Patients { get ; set; }
        public DbSet<RDV> RDVs { get ; set; }
        public DbSet<MedicalService> MedicalServices { get ; set; }
        public DbSet<MedicalServiceDoctor> MedicalServiceDoctors { get ; set; }
        public DbSet<DoctorSchedule> DoctorScheduels { get;set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships here if necessary

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RDV>().
                HasOne(r => r.Doctor).
                WithMany(d => d.RDVs).
                HasForeignKey(r => r.DoctorId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RDV>().
                HasOne(r => r.Patient).
                WithMany(d => d.RDVs).
                HasForeignKey(r => r.PatientId).
                OnDelete(DeleteBehavior.Restrict);
            
           

            modelBuilder.Entity<MedicalServiceDoctor>().
                HasKey(mds => new {mds.MedicalServiceId , mds.DoctorId});

            modelBuilder.Entity<MedicalServiceDoctor>().
                HasOne(mds => mds.MedicalService).
                WithMany(ms => ms.MedicalServiceDoctors).
                HasForeignKey(mds => mds.MedicalServiceId).
                OnDelete(DeleteBehavior.Cascade);;

            modelBuilder.Entity<MedicalServiceDoctor>().
                HasOne(mds => mds.Doctor).
                WithMany(ms => ms.MedicalServiceDoctors).
                HasForeignKey(mds => mds.DoctorId).
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DoctorSchedule>().
                HasOne(ds => ds.Doctor).
                WithMany(d => d.DoctorSchedules).
                HasForeignKey(ds => ds.DoctorId).
                OnDelete(DeleteBehavior.Cascade);;


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Patient",
                    NormalizedName = "PATIENT"
                },
                new IdentityRole
                {
                    Name = "Doctor",
                    NormalizedName = "DOCTOR"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }

}