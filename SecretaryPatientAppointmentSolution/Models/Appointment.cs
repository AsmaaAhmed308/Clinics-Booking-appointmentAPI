namespace SecretaryPatientAppointmentSolution.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int Duration { get; set; }   
        public string? PatientName { get; set; }   
        public string? DocName { get; set; }    
        public DateTime? BirthDate { get; set; }   
        public string? Gender { get; set; }   
        public string? ContactInfo { get; set; }  

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
