using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Infrastructure;
using NUnit.Framework;
using PatientRecords;
using PatientRecords.ViewModels;
using Reporting;
using Rhino.Mocks;
using Should;

using Assert = NUnit.Framework.Assert;
using Is = Rhino.Mocks.Constraints.Is;

namespace Tests.ViewModelTests
{
    [TestFixture]
    public class PatientViewModel_TestFixture
    {
        [Test]
        public void Should_return_correct_search_results()
        {
            PatientDto expectedDto = new PatientDto()
            {
                Id = Guid.NewGuid(),
                Name = "Jeff Carley",
                Status = "Active",
                Street = "123 Main St",
                City = "Madison",
                State = "WI",
                Zip = "53571"
            };

            var query = new EnumerableQuery<PatientDto>(new List<PatientDto>() { expectedDto });

            IReportingRepository<PatientDto> repository = MockRepository.GenerateMock<IReportingRepository<PatientDto>>();
            repository.Stub(repo => repo.GetAll()).Return(query);

            PatientSearchViewModel viewModel = new PatientSearchViewModel(repository);
            viewModel.SearchText = "Jeff Carley";
            viewModel.Search.Execute(null);

            viewModel.PatientResults.ShouldContain(new PatientListViewModel(expectedDto));
        }

        [Test]
        public void Should_return_zero_search_results()
        {
            PatientDto expectedDto = new PatientDto()
            {
                Id = Guid.NewGuid(),
                Name = "Jeff Carley",
                Status = "Active",
                Street = "123 Main St",
                City = "Madison",
                State = "WI",
                Zip = "53571"
            };

            var query = new EnumerableQuery<PatientDto>(new List<PatientDto>() { expectedDto });

            IReportingRepository<PatientDto> repository = MockRepository.GenerateMock<IReportingRepository<PatientDto>>();
            repository.Stub(repo => repo.GetAll()).Return(query);

            PatientSearchViewModel viewModel = new PatientSearchViewModel(repository);

            // searchs are exact only
            viewModel.SearchText = "Jeff";
            viewModel.Search.Execute(null);

            viewModel.PatientResults.ShouldBeEmpty();
        }
    }
}
