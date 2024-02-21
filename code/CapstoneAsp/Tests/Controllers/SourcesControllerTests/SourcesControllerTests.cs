using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Controllers.SourcesControllerTests
{
    public class SourcesControllerTests
    {
        private ISourceService sourceService;

        private ISourceRepository sourceRepository;

        private SourcesController sourceController;



        [SetUp]
        public void SetUp()
        {
            var context = new MockDataContext();
            this.sourceRepository = new SourceRepository(context);
            this.sourceService = new SourceService(this.sourceRepository);
            this.sourceController = new SourcesController(null, this.sourceService);
        }

        [Test]
        public void TestNotNull()
        {
            var sourceController = new SourcesController(null, this.sourceService);

            Assert.IsNotNull(sourceController);
        }

        [Test]
        public void CreateSourceValid()
        {
            var source = new Source()
            {
                Source_Id = 5,
                Content = "",
                Created_By = "User 1",
                Description = "nothing",
                Meta_Data = "",
                Name = "Source 5",
                Source_Type_Id = 1,
                Tags = ""
            };

            Assert.IsInstanceOfType<OkObjectResult>(this.sourceController.CreateSource(source).Result);
        }
        [Test]
        public void CreateSourceInValid()
        {
            var source = new Source()
            {
                Source_Id = 5,
                Content = "",
                Created_By = "User 1",
                Description = "nothing",
                Meta_Data = "",
                Name = "Source 5",
                Source_Type_Id = 1,
                Tags = ""
            };

            Assert.IsInstanceOfType<BadRequestObjectResult>(this.sourceController.CreateSource(null).Result);
        }

        [Test]
        public async Task GetBySourceByUsername()
        {
            var username = "User 1";
            var sources = await this.sourceController.GetBySourceByUsername(username);
            var found = MockDataContext.Sources.Where(x => x.Created_By.Equals(username));

            Assert.AreEqual(sources.ElementAt(0).Name, found.ElementAt(0).Name);
        }

        [Test]
        public async Task GetSourceById()
        {
            var newSource = new Source()
            {
                Source_Id = 1,
                Content = "",
                Created_By = "User 1",
                Description = "nothing",
                Meta_Data = "",
                Name = "Source 1",
                Source_Type_Id = 1,
                Tags = ""
            };
            var found = await this.sourceController.GetSourceById((int)newSource.Source_Id);

            Assert.AreEqual(newSource.Source_Id, found.Source_Id);
        }

        [Test]
        public void DeleteByIdValid()
        {
            var id = 1;
            Assert.IsInstanceOfType<OkObjectResult>(this.sourceController.DeleteById(id).Result);
        }
    }
}
