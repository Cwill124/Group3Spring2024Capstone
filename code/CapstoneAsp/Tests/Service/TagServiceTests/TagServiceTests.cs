using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
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

        [Test]
        public async Task CreateTag()
        {
            var newTag = new Tags()
            {
                Note = 1,
                Tag = "Test Tag",
                TagId = 3
            };
            await this.tagService.CreateTag(newTag);
            var found = MockDataContext.Tags.Where(x => x.TagId == newTag.TagId);

            Assert.AreEqual(newTag.Tag, found.ElementAt(0).Tag);
        }

        [Test]
        public async Task GetTagsByNotesId()
        {
                var noteId = 1;
                var tags = await this.tagService.GetTagsByNoteId(noteId);
                var found = MockDataContext.Tags.Where(x => x.Note == noteId);

                Assert.AreEqual(tags.ToList().ElementAt(0), found.ToList().ElementAt(0));

                Assert.AreEqual(tags.ToList().ElementAt(1), found.ToList().ElementAt(1));
        }

        [Test]
        public async Task GetTagsBelongingToUser()
        {
            var username = "TestUser";
            Assert.IsNotNull(this.tagService.GetTagsBelongingToUser(username));
        }
        #endregion
    }
}
