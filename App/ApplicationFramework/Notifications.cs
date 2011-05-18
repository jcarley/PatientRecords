using System;
using PatientRecords.ApplicationFramework.Events;
using GalaSoft.MvvmLight.Messaging;

namespace PatientRecords.ApplicationFramework
{
    public static class Notifications
    {
        public static readonly string SearchForPatient = Guid.NewGuid().ToString();
        public static readonly string ShowPatientDetails = Guid.NewGuid().ToString();
        public static readonly string CreateNewPatient = Guid.NewGuid().ToString();

        public static class SearchForPatientMessage
        {
            public static void Send(SearchForPatientEvent evt)
            {
                Messenger.Default.Send<SearchForPatientEvent>(evt, SearchForPatient);
            }

            public static void Register(object recipient, Action<SearchForPatientEvent> action)
            {
                Messenger.Default.Register<SearchForPatientEvent>(recipient, SearchForPatient, action);
            }
        }

        public static class ShowPatientDetailsMessage
        {
            public static void Send(ShowPatientDetailsEvent evt)
            {
                Messenger.Default.Send<ShowPatientDetailsEvent>(evt, ShowPatientDetails);
            }

            public static void Register(object recipient, Action<ShowPatientDetailsEvent> action)
            {
                Messenger.Default.Register<ShowPatientDetailsEvent>(recipient, ShowPatientDetails, action);
            }
        }

        public static class CreateNewPatientMessage
        {
            public static void Send(CreateNewPatientEvent evt)
            {
                Messenger.Default.Send<CreateNewPatientEvent>(evt, CreateNewPatient);
            }

            public static void Register(object recipient, Action<CreateNewPatientEvent> action)
            {
                Messenger.Default.Register<CreateNewPatientEvent>(recipient, CreateNewPatient, action);
            }
        }
    }
}
