using System;
using PatientRecords.ApplicationFramework.Events;
using GalaSoft.MvvmLight.Messaging;

namespace PatientRecords.ApplicationFramework
{
    public static class Notifications
    {
        public static readonly string SearchForPatientToken = Guid.NewGuid().ToString();
        public static readonly string ShowPatientDetailsToken = Guid.NewGuid().ToString();
        public static readonly string CreateNewPatientToken = Guid.NewGuid().ToString();
        public static readonly string ChangePatientNameToken = Guid.NewGuid().ToString();
        public static readonly string RelocatePatientToken = Guid.NewGuid().ToString();

        public static class SearchForPatientMessage
        {
            public static void Send(SearchForPatientEvent evt)
            {
                Messenger.Default.Send<SearchForPatientEvent>(evt, SearchForPatientToken);
            }

            public static void Register(object recipient, Action<SearchForPatientEvent> action)
            {
                Messenger.Default.Register<SearchForPatientEvent>(recipient, SearchForPatientToken, action);
            }
        }

        public static class ShowPatientDetailsMessage
        {
            public static void Send(ShowPatientDetailsEvent evt)
            {
                Messenger.Default.Send<ShowPatientDetailsEvent>(evt, ShowPatientDetailsToken);
            }

            public static void Register(object recipient, Action<ShowPatientDetailsEvent> action)
            {
                Messenger.Default.Register<ShowPatientDetailsEvent>(recipient, ShowPatientDetailsToken, action);
            }
        }

        public static class CreateNewPatientMessage
        {
            public static void Send(CreateNewPatientEvent evt)
            {
                Messenger.Default.Send<CreateNewPatientEvent>(evt, CreateNewPatientToken);
            }

            public static void Register(object recipient, Action<CreateNewPatientEvent> action)
            {
                Messenger.Default.Register<CreateNewPatientEvent>(recipient, CreateNewPatientToken, action);
            }
        }

        public static class ChangePatientNameMessage
        {
            public static void Send(ChangePatientNameEvent evt)
            {
                Messenger.Default.Send<ChangePatientNameEvent>(evt, ChangePatientNameToken);
            }

            public static void Register(object recipient, Action<ChangePatientNameEvent> action)
            {
                Messenger.Default.Register<ChangePatientNameEvent>(recipient, ChangePatientNameToken, action);
            }
        }
    }
}
