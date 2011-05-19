using System;

namespace Commands
{
    public class ChangePatientNameCommand : Command
    {
        public ChangePatientNameCommand(Guid id, Guid patientId)
            : base(id)
        {
            PatientId = patientId;
        }

        public Guid PatientId { get; set; }
        public string Name { get; set; }
    }
}