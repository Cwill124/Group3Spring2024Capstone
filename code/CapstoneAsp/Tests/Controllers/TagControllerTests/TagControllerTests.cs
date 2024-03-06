using CapstoneASP.Controllers;
using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Controllers.TagControllerTests
{
    public class TagControllerTests
    {
        #region Data Members

        private ITagRepository tagRepository;

        private ITagService tagService;

        private TagController tagController;
        #endregion

        #region Methods

        [SetUp]
        public void SetUp()
        {
            var context = new MockDataContext();
            this.tagRepository = new TagRepository(context);
            this.tagService = new TagService(this.tagRepository);
            this.tagController = new TagController(null, this.tagService);
        }


        [Test]
        public void TestNotNull()
        {
            var tagController = new TagController(null, this.tagService);

            Assert.IsNotNull(tagController);
        }
        [Test]
        public void TestDeleteById()
        {
            var tagId = 1;

            Assert.IsInstanceOfType<OkObjectResult>(this.tagController.DeleteById(tagId).Result);
        }
        [Test]
        public void TestCreateTag(){
            var newTag = new Tags()
            {
                Note = 1,
                Tag = "Test Tag",
                TagId = 3
            };

            Assert.IsInstanceOfType<OkObjectResult>(this.tagController.CreateTag(newTag).Result);
        }

        [Test]
        public void TestGetTagsByNoteId()
        {
            var id = 1;
            Assert.IsNotNull(this.tagController.GetTagsByNoteId(id));
        }
        #endregion
    }
}
