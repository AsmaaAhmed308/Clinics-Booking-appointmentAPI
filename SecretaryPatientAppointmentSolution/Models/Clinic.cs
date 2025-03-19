namespace SecretaryPatientAppointmentSolution.Models
{
    public class Clinic
    {
         public int Id { get; set; }    
        public string? ClinicName { get; set; }  
        public string? ClinicLocation { get; set; }
        public ICollection<Doctor> Doctors { get; set; }

    }
}
