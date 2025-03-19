using System.ComponentModel.DataAnnotations.Schema;

namespace SecretaryPatientAppointmentSolution.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan? SlotTime { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? SlotStatus { get; set; }

        [NotMapped]
        public Doctor Doctor { get; set; }
    }
}
