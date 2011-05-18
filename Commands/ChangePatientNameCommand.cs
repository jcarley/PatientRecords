using System;

namespace Commands
{
    public class ChangePatientNameCommand : Command
    {
        public ChangePatientNameCommand(Guid id)
            : base(id)
        {
        }

        public string Name { get; set; }
    }
}
