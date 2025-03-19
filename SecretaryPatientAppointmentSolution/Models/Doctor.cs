namespace SecretaryPatientAppointmentSolution.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string? Name { get; set; }
        public string? Specialty { get; set; }
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
        public ICollection<Schedule> Schedules { get; set; }  
        public ICollection<Appointment> Appointments { get; set; }
    }
}
