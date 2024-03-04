using CapstoneASP.Database.DBContext;
using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Repository.TagRepositoryTests
{
    public class TagRepositoryTests
    {
        #region Data members

        private IDataContext context;

        private ITagRepository repository;

        #endregion

        #region Methods

        [SetUp]
        public void SetUp()
        {
            this.context = new MockDataContext();
            this.repository = new TagRepository(this.context);
        }

        [Test]
        public void NotNullTest()
        {
            var tagRepo = new TagRepository(this.context);

            Assert.IsNotNull(tagRepo);
        }

        [Test]
        public async Task CreateValidTag()
        {
            var newTag = new Tags()
            {
                Note = 1,
                Tag = "Test Tag",
                TagId = 3
            };
            await this.repository.CreateTag(newTag);
            var found = MockDataContext.Tags.Where(x => x.TagId == newTag.TagId);

            Assert.AreEqual(newTag.Tag, found.ElementAt(0).Tag);
        }

        [Test]
        public async Task GetTagsByNoteId()
        {
            var noteId = 1;
            var tags = await this.repository.GetTagsByNoteId(noteId);
            var found = MockDataContext.Tags.Where(x => x.Note == noteId);

            Assert.AreEqual(tags.ToList().ElementAt(0), found.ToList().ElementAt(0));
            
            Assert.AreEqual(tags.ToList().ElementAt(1), found.ToList().ElementAt(1));
        }
        #endregion
    }
}
