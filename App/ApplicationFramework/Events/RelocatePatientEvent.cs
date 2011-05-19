using System;

namespace PatientRecords.ApplicationFramework.Events
{
    public class RelocatePatientEvent
    {
        public Guid PatientId { get; set; }

        public RelocatePatientEvent(Guid id)
        {
            PatientId = id;
        }

    }
}
