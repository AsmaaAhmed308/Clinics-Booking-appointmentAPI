using System;
using System.IO;
using System.Threading.Tasks;

namespace SecretaryPatientAppointmentSolution.Helpers
{
    public class logStordProcedureExcution
    {
        private static readonly object lockObject = new object();

        public static async Task logStordProcedureAsync(string filePath, string message)
        {
            // Ensure the directory exists  
            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);

            // Write the message to the file  
            lock (lockObject)
            {
                using (var streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine($"{DateTime.Now}: {message}");
                }
            }
        }
    }
}
