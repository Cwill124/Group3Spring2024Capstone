using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Service.TagServiceTests
{
    public class TagServiceTests
    {
        #region Data members

        private ITagRepository tagRepository;
        
        private ITagService tagService;


        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            var context = new MockDataContext();
            this.tagRepository = new TagRepository(context);
            this.tagService = new TagService(this.tagRepository);
        }

        [Test]
        public void NotNullTests()
        {
            var tagService = new TagService(this.tagRepository);

            Assert.IsNotNull(tagService);
        }
        [Test]
        public async Task DeleteByTagId(){
            var tagId = 1;

            var amountBefore = MockDataContext.Tags.Count;
            await this.tagService.DeleteTagById(tagId);
            var amountAfter = MockDataContext.Tags.Count;

            Assert.AreNotEqual(amountBefore,amountAfter);
        }

        #endregion
    }
}
