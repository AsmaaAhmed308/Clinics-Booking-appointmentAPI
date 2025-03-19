namespace SecretaryPatientAppointmentSolution.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactInfo { get; set; }
        public string Gender { get; set; }
        public DateTime? RegDate { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
