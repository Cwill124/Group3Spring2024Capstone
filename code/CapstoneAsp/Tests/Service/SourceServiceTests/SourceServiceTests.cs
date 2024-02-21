using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Service.SourceServiceTests
{
    public class SourceServiceTests
    {
        #region Data members

        private ISourceRepository repository;

        private ISourceService sourceService;

        #endregion

        [SetUp]
        public void Setup()
        {
            var context = new MockDataContext();
            this.repository = new SourceRepository(context);
            this.sourceService = new SourceService(this.repository);
        }

        [Test]
        public void NotNullTests()
        {
            var sourceService = new SourceService(this.repository);


            Assert.IsNotNull(sourceService);
        }

        [Test]
        public async Task GetSourceByUsername()
        {
            var username = "User 1";
            var sources = await this.sourceService.GetSourceByUsername(username);
            var found = MockDataContext.Sources.Where(x => x.Created_By.Equals(username));
            
            Assert.AreEqual(sources.ElementAt(0).Name,found.ElementAt(0).Name);

        }

        [Test]
        public async Task Create()
        {
            var newSource = new Source()
            {
                Content = "content",
                Created_By = "User 4",
                Description = " ",
                Meta_Data = "author: name",
                Name = "Source 4",
                Source_Id = 4,
                Source_Type_Id = 1,
                Tags = "tagged"
            };
            await this.sourceService.Create(newSource);

            var found = MockDataContext.Sources.Where(x => x.Name.Equals(newSource.Name)).ElementAt(0);
            Assert.AreEqual(newSource,found);
        }

        [Test]
        public async Task GetById()
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
            var found = await this.sourceService.GetById((int)newSource.Source_Id);

            Assert.AreEqual(newSource.Source_Id,found.Source_Id);
        }

        [Test]
        public async Task Delete()
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
            await this.sourceService.Delete(newSource.Source_Id);

            Assert.IsFalse(MockDataContext.Sources.Contains(newSource));
        }

    }
}
