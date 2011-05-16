using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using NUnit.Framework;
using PatientRecords.ViewModels;
using Should;

using Assert = NUnit.Framework.Assert;
using Is = Rhino.Mocks.Constraints.Is;



namespace Tests.ViewModelTests
{
    [TestFixture]
    public class MainViewModel_TestFixture
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            BootStrapper.Run();
        }

        [Test]
        public void Always_get_same_instance()
        {
            var locator = new ViewModelLocator();
            var vm1 = locator.Main;
            var vm2 = locator.Main;

            vm1.ShouldBeSameAs(vm2);
        }

        [Test]
        public void Should_have_proper_welcome_message()
        {
            var locator = new ViewModelLocator();
            var vm1 = locator.Main;

            vm1.Welcome.ShouldEqual("Patient Records v1.0");
        }
    }
}