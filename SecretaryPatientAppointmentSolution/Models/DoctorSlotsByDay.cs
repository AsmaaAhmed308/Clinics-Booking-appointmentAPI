namespace SecretaryPatientAppointmentSolution.Models
{
    public class DoctorSlotsByDay
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public TimeSpan? SlotTime { get; set; }
        public string? SlotStatus { get; set; }
        public DayOfWeek Day { get; set; } 
    }
}
