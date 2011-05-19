using System;

namespace Commands
{
    public class RelocatePatientCommand : Command
    {
        public RelocatePatientCommand(Guid id, Guid patientId)
            : base(id)
        {
            PatientId = patientId;
        }

        public Guid PatientId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
