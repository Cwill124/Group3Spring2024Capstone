using CapstoneASP.Controllers;
using CapstoneASP.Database.DBContext;
using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Controllers.ProjectControllerTests
{
    public class ProjectControllerTests
    {
        #region Data members

        private IDataContext context;
        private IProjectRepository repository;
        private IProjectService projectService;
        private ProjectController controller;

        #endregion

        #region Methods

        [SetUp]
        public void SetUp()
        {
            this.context = new MockDataContext();
            this.repository = new ProjectRepository(this.context);
            this.projectService = new ProjectService(this.repository);
            this.controller = new ProjectController(null, this.projectService);
        }

        [Test]
        public void IsNotNullTest()
        {
            var projectController = new ProjectController(null, this.projectService);

            Assert.IsNotNull(projectController);
        }

        [Test]
        public async Task CreateProjectValid()
        {
          
            var newProject = new Project()
            {
                Description = "Test",
                Owner = "User 1",
                ProjectId = 3,
                Title = "Test Title"
            };

            var result = await this.controller.CreateProject(newProject);

          
            Assert.IsNotNull(result); 
            Assert.IsInstanceOfType(result, typeof(ActionResult)); 
        }
        [Test]
        public async Task CreateProjectInvalid()
        {

            var result = await controller.CreateProject(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [Test]
        public void TestGetAllByUser()
        {
            var owner = "User 1";

            var projects = this.controller.GetAllByUser(owner);

            Assert.IsNotNull(projects);
            Assert.AreNotEqual(0,projects.Result.Count());
        }
        [Test]
        public void TestGetById()
        {
            var id = 1;

            var projects = this.controller.GetById(id);

            Assert.IsNotNull(projects);
            Assert.AreEqual(id,projects.Result.ProjectId);
        }

        [Test]
        public void TestDelete()
        {
            var id = 1;

            Assert.IsInstanceOfType(this.controller.Delete(id).Result, typeof(ActionResult));
        }
        #endregion
    }
}
