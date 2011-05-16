using System;

namespace PatientRecords.ApplicationFramework.Events
{
    public class ShowPatientDetailsEvent
    {
        public string PatientId { get; set; }

        public ShowPatientDetailsEvent(string id)
        {
            PatientId = id;
        }
    }
}