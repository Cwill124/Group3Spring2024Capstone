using CapstoneASP.Database.DBContext;
using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Repository.ProjectRepositoryTests
{
    public class ProjectRepositoryTests
    {
        #region Data members

        private IDataContext context;

        private IProjectRepository projectRepository;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            this.context = new MockDataContext();
            this.projectRepository = new ProjectRepository(this.context);
        }

        [Test]
        public void NotNullTest()
        {
            var projectRepo = new ProjectRepository(this.context);

            Assert.IsNotNull(projectRepo);
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
            this.projectRepository.Create(newProject);
            var expected = MockDataContext.Projects.Where(x => x.ProjectId == newProject.ProjectId);
            Assert.AreEqual(newProject.ProjectId, expected.ElementAt(0).ProjectId);

        }

        [Test]
        public void TestDelete()
        {
            var projectToDelete = 1;
            this.projectRepository.Delete(projectToDelete);
            var list = MockDataContext.Projects.Where(x => x.ProjectId == projectToDelete);
            Assert.AreEqual(0,list.Count());
        }

        [Test]
        public void TestGetAllByUser()
        {
            var owner = "User 1";

            var projects = this.projectRepository.GetAllProjectsForUser(owner);

            var expected = MockDataContext.Projects.Where(x => x.Owner.Equals(owner));

            Assert.AreEqual(expected.ElementAt(0), projects.Result.ElementAt(0));
        }

        [Test]
        public void TestGetById()
        {
            var id = 2;

            var fetchedProject = this.projectRepository.GetProjectById(id);

            var expected = MockDataContext.Projects.Where(x => x.ProjectId == id);

            Assert.IsNotNull(fetchedProject);
            Assert.AreEqual<Project>(expected.ElementAt(0),fetchedProject.Result);
        }
        #endregion
    }
}
