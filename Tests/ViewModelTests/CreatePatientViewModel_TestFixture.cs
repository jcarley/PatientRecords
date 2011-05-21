using System.Linq;
using Infrastructure;
using NUnit.Framework;
using PatientRecords.ViewModels;
using Reporting;
using Should;
using StructureMap;

namespace Tests.ViewModelTests
{
    [TestFixture]
    public class CreatePatientViewModel_TestFixture
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            BootStrapper.Run();
        }

        [Test]
        public void Should_successfully_create_a_patient()
        {
            IBus bus = ObjectFactory.GetInstance<IBus>();
            
            var viewModel = new CreatePatientViewModel(bus);

            viewModel.Command.Name = "Jeff Carley";
            viewModel.Command.Status = "Active";
            viewModel.Command.Street = "123 Main St";
            viewModel.Command.City = "Madison";
            viewModel.Command.State = "WI";
            viewModel.Command.Zip = "53598";

            viewModel.CreateCustomer.Execute(null);

            IReportingRepository<PatientDto> repository = ObjectFactory.GetInstance<IReportingRepository<PatientDto>>();

            var patient = repository.GetAll(x => x.Name == "Jeff Carley").FirstOrDefault();

            patient.ShouldNotBeNull();
            patient.Name.ShouldEqual("Jeff Carley");

        }
    }
}
