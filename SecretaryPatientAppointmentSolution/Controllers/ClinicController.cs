using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SecretaryPatientAppointmentSolution.Data;
using SecretaryPatientAppointmentSolution.Models;
using System.Xml.Serialization;
using System.Xml;
using SecretaryPatientAppointmentSolution.Dtos;
using SecretaryPatientAppointmentSolution.Helpers;
using System.Text.Json;
using Newtonsoft.Json;


namespace SecretaryPatientAppointmentSolution.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : Controller
    {

        //-------------------------------------//
        private readonly ClinicAppDbContext dbContext;
        private readonly string logFilePath = "C:\\Logs\\StoredProcedureLogs.txt";  

        public ClinicController(ClinicAppDbContext dbContext)
        { 
            this.dbContext = dbContext;
        }
        //-------------------------------------//
        [HttpGet]
        [Route("doctors")]
        public IActionResult GetAllDoctors()
        {
            var Doctors  = dbContext.Doctors.ToList();
            return Ok(Doctors);
        }
        //--------------------------------------//
        [HttpGet("{docId}/{dayNo}")]
        public async Task<ActionResult<List<DoctorSlotsByDay>>> GetSchedule(int docId, int dayNo)
        {
            // Validate dayNo (0=Sunday, 6=Saturday)  
            if (dayNo < 0 || dayNo > 6)
            {
                return BadRequest("Invalid day number. Must be between 0 (Sunday) and 6 (Saturday).");
            }
            try
            {
                // Assuming Schedule is the entity that maps to the result of the stored procedure  
                var schedules = await dbContext.DoctorSlotsByDay
                    .FromSqlRaw("EXEC LoadDoctorSlots @DocId = {0}, @DayNo = {1}", docId, dayNo)
                    .ToListAsync();

                return Ok(schedules); // Return the found schedules  
            }
            catch   {
                return StatusCode(500, "An error occurred while retrieving the schedule.");
            }
        }
        //--------------------------------------//
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationData reservationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // This returns the validation errors  
            }

            if (reservationData == null || reservationData.PatientData == null || reservationData.SelectedSlots == null)
            {
                return BadRequest("Invalid reservation data.");
            }

            //if (reservationData != null && reservationData.SelectedSlots.Time != null)
            //{

            //    // If you sent `slotTime` as a string, convert it to TimeSpan  
            //    TimeSpan timeSlot = TimeSpan.Parse(reservationData.SelectedSlots.Time.ToString());
            //    reservationData.SelectedSlots.Time = timeSlot;
            //}

            string patientXml = ConvertToXml(reservationData.PatientData);
            string slotsXml = ConvertToXml(reservationData.SelectedSlots);
            // Clean the XML strings to remove unwanted namespaces  
            string cleanedPatientXml = CleanXml(patientXml);
            string cleanedSlotsXml = CleanXml(slotsXml);

            //----------------------------//
            // Create the log XML structure  
            string logXml = $@"  Declare @PatientDataXml XML,@SelectedSlotsXml XML 
SET @PatientDataXml = '{cleanedPatientXml}'
SET @SelectedSlotsXml = '{cleanedSlotsXml}'
EXEC  CreateReservation  @PatientDataXml , @SelectedSlotsXml";

            // Log the XML structure to the file  
            await logStordProcedureExcution.logStordProcedureAsync(logFilePath, logXml);
            //----------------------------//
            try { 
            // Call the stored procedure with XML parameters  
            await dbContext.Database.ExecuteSqlRawAsync(
                "EXEC CreateReservation @PatientDataXml, @SelectedSlotsXml",
                new SqlParameter("@PatientDataXml", cleanedPatientXml),
                new SqlParameter("@SelectedSlotsXml", cleanedSlotsXml)
            );
            await logStordProcedureExcution.logStordProcedureAsync(logFilePath, "Reservation created successfully.");

//                // Fetch the created reservation's details right after inserting it  
//                var savedReservation = await dbContext.Appointments
//                    .OrderByDescending(r => r.AppointmentDate) // Assuming your model has a CreatedAt column  
//                    .FirstOrDefaultAsync();
//                if (savedReservation != null)
//                { 
//                    string savedReservationJson = JsonConvert.SerializeObject(savedReservation);

//                    // Create a log entry for the saved reservation  
//                    string savedLogJson = $@"  
//{{  
//    ""SavedReservation"": {savedReservationJson}  
//}}";

//                    // Log the saved data to the file  
//                    await logStordProcedureExcution.logStordProcedureAsync(logFilePath, savedLogJson);
//                }
            

            return Ok();
            }

            catch (Exception ex)
            {
                await logStordProcedureExcution.logStordProcedureAsync(logFilePath, $"An error occurred while creating the reservation: {ex.Message}");
                return BadRequest("Invalid Process.");
            }
        }


        //-------------------------------------//
        [HttpGet]
        [Route("appointments")]
        public async Task<ActionResult<List<Appointment>>> GetAllAppointments()
        {
            
                // Assuming Schedule is the entity that maps to the result of the stored procedure  
                var Appointment = await dbContext.Appointments
                    .FromSqlRaw("EXEC LoadPatientAppointment")
                    .ToListAsync();

                return Ok(Appointment); // Return the found Appointment  

        }
        //===========================Handel XML Area==================================//
        private string ConvertToXml<T>(T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
                {
                    serializer.Serialize(xmlWriter, data);
                    return stringWriter.ToString();
                }
            }
        }

        private string CleanXml(string xml)
        {
            // Remove unwanted namespace declarations  
            return xml
                .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", string.Empty)
                .Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", string.Empty)
                .Trim();  // Trim whitespace  
        }
        //===========================================================================//



    }
}
