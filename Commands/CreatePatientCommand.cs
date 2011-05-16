using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public class CreatePatientCommand : Command
    {
        public CreatePatientCommand(Guid id)
            : base(id)
        {
        }

        public string Name { get; set; }
        public string Status { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
