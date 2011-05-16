using System;

namespace PatientRecords.ApplicationFramework
{
    public static class Notifications
    {
        public static readonly string ShowPatientDetails = Guid.NewGuid().ToString();
        public static readonly string CreateNewPatient = Guid.NewGuid().ToString();
    }
}
