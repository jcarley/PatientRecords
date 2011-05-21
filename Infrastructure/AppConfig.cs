
namespace Infrastructure
{
    public static class AppConfig
    {
        public static string RavenDBConnectionStringName
        {
            get
            {
                return "RavenDB";
            }
        }

        public static string SqlDBConnectionStringName
        {
            get
            {
                return "PatientDBEventStore";
            }
        }

        public static string PatientDBEventStoreEntities
        {
            get
            {
                return "PatientDBEventStoreEntities";
            }
        }
    }
}
