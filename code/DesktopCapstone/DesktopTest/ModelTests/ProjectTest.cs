using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCapstone.model;

namespace DesktopTest.ModelTests
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void TestProjectCreation()
        {
            Project project = new Project
            {
                ProjectId = 1,
                Title = "Test Title",
                Description = "Test Description",
                Owner = "Test Owner"
            };
            Assert.AreEqual(1, project.ProjectId);
            Assert.AreEqual("Test Title", project.Title);
            Assert.AreEqual("Test Description", project.Description);
            Assert.AreEqual("Test Owner", project.Owner);
        }

        [TestMethod]
        public void TestToString()
        {
            Project project = new Project
            {
                ProjectId = 1,
                Title = "Test Title",
                Description = "Test Description",
                Owner = "Test Owner"
            };
            Assert.AreEqual("Test Title -  Description: Test Description", project.ToString());
        }


    }
}
