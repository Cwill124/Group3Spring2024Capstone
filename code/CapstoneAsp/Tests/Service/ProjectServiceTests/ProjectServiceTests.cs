using CapstoneASP.Database.DBContext;
using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Service.ProjectServiceTests
{
    public class ProjectServiceTests
    {
        #region Data Members
        private IDataContext context;
        private IProjectRepository repository;
        private IProjectService projectService;
        #endregion

        #region Methods

        [SetUp]
        public void SetUp()
        {
            this.context = new MockDataContext();
            this.repository = new ProjectRepository(this.context);
            this.projectService = new ProjectService(this.repository);
        }

        [Test]
        public void IsNotNullTest()
        {
            var projectService = new ProjectService(this.repository);

            Assert.IsNotNull(projectService);
        }
        [Test]
        public void TestCreate()
        {
            var newProject = new Project()
            {
                Description = "Test",
                Owner = "User 1",
                ProjectId = 3,
                Title = "Test Title"
            };
            this.projectService.Create(newProject);
            var expected = MockDataContext.Projects.Where(x => x.ProjectId == newProject.ProjectId);
            Assert.AreEqual(newProject.ProjectId, expected.ElementAt(0).ProjectId);

        }

        [Test]
        public void TestDelete()
        {
            var projectToDelete = 1;
            this.projectService.Delete(projectToDelete);
            var list = MockDataContext.Projects.Where(x => x.ProjectId == projectToDelete);
            Assert.AreEqual(0, list.Count());
        }

        [Test]
        public void TestGetAllByUser()
        {
            var owner = "User 1";

            var projects = this.projectService.GetAllProjectsForUser(owner);

            var expected = MockDataContext.Projects.Where(x => x.Owner.Equals(owner));

            Assert.AreEqual(expected.ElementAt(0), projects.Result.ElementAt(0));
        }

        [Test]
        public void TestGetById()
        {
            var id = 2;

            var fetchedProject = this.projectService.GetProjectById(id);

            var expected = MockDataContext.Projects.Where(x => x.ProjectId == id);

            Assert.IsNotNull(fetchedProject);
            Assert.AreEqual<Project>(expected.ElementAt(0), fetchedProject.Result);
        }
        #endregion
    }
}
