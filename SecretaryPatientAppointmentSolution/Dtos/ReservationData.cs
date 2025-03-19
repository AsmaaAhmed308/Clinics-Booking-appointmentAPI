using SecretaryPatientAppointmentSolution.Models;

namespace SecretaryPatientAppointmentSolution.Dtos
{
    public class ReservationData
    {
        public PatientData PatientData { get; set; }
        public Slots SelectedSlots { get; set; }
    }
}
