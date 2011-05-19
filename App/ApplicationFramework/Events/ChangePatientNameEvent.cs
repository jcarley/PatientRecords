using System;

namespace PatientRecords.ApplicationFramework.Events
{
    public class ChangePatientNameEvent
    {
        public Guid PatientId { get; set; }

        public ChangePatientNameEvent(Guid id)
        {
            PatientId = id;
        }
    }
}