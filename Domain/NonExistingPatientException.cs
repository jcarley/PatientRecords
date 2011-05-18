using System;

namespace Domain
{
    public class NonExistingPatientException : ApplicationException
    {
        public NonExistingPatientException(string message)
            : base(message)
        {
        }
    }
}
