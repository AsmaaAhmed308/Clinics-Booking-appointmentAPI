using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using SecretaryPatientAppointmentSolution.Models;

namespace SecretaryPatientAppointmentSolution.Data
{
    public class ClinicAppDbContext(DbContextOptions<ClinicAppDbContext> options) : DbContext(options)
    {
        public DbSet<Clinic> Clinic {  get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorSlotsByDay> DoctorSlotsByDay { get; set; }
        
    }
}
