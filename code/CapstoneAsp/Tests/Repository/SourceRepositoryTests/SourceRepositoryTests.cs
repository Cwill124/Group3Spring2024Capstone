using CapstoneASP.Database.DBContext;
using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Repository.SourceRepositoryTests;

public class SourceRepositoryTests
{
    #region Data members

    private IDataContext context;

    private ISourceRepository sourceRepository;

    #endregion

    #region Methods

    [SetUp]
    public void Setup()
    {
        this.context = new MockDataContext();
        this.sourceRepository = new SourceRepository(this.context);
    }

    [Test]
    public void NotNullTest()
    {
        var sourceRepository = new SourceRepository(this.context);

        Assert.IsNotNull(sourceRepository);
    }

    [Test]
    public async Task GetSourceByUsername()
    {
        var username = "User 1";
        var sources = await this.sourceRepository.GetSourcesByUsername(username);
        var found = MockDataContext.Sources.Where(x => x.Created_By.Equals(username));

        Assert.AreEqual(sources.ElementAt(0).Name, found.ElementAt(0).Name);
    }

    [Test]
    public async Task Create()
    {
        var newSource = new Source
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
        await this.sourceRepository.Create(newSource);

        var found = MockDataContext.Sources.Where(x => x.Name.Equals(newSource.Name)).ElementAt(0);
        Assert.AreEqual(newSource, found);
    }

    [Test]
    public async Task GetById()
    {
        var newSource = new Source
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
        var found = await this.sourceRepository.GetById(newSource.Source_Id);

        Assert.AreEqual(newSource.Source_Id, found.Source_Id);
    }

    [Test]
    public async Task Delete()
    {
        var newSource = new Source
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
        await this.sourceRepository.Delete(newSource.Source_Id);

        Assert.IsFalse(MockDataContext.Sources.Contains(newSource));
    }

    [Test]
    public void AddSourceToProjectTest()
    {
        var projectId = 1;
        var newSource = 2;

        this.sourceRepository.AddSourceToProject(newSource, projectId);
        var expected = MockDataContext.ProjectAndSources.Where(x => x.projectId == projectId);
        var contains = expected.ElementAt(0).sources.Contains(newSource);
        Assert.AreEqual(true,contains);

    }
    [Test]
    public void DeleteSourceToProjectTest()
    {
        var projectId = 2;
        var newSource = 3;

        this.sourceRepository.DeleteSourceFromProject(newSource, projectId);
        var expected = MockDataContext.ProjectAndSources.Where(x => x.projectId == projectId);
        var contains = expected.ElementAt(0).sources.Contains(newSource);
        Assert.AreEqual(false, contains);
    }

    [Test]
    public void GetAllNotInProjectTest()
    {
        var projectId = new Project()
        {
            Description = "",
            Owner = "Corey124",
            ProjectId = 1,
            Title = "Title"
        };
        var sources = this.sourceRepository.GetAllNotInProject(projectId).Result;

        Assert.IsNotNull(sources);
        Assert.AreEqual(2, sources.Count());

    }
    [Test]
    public void GetAllInProjectTest()
    {
        var projectId = 1;
        var sources = this.sourceRepository.GetAllInProject(projectId).Result;

        Assert.IsNotNull(sources);
        Assert.AreEqual(1, sources.Count());

    }

    #endregion
}