using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using System.Linq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Service.SourceServiceTests;

public class SourceServiceTests
{
    #region Data members

    private ISourceRepository repository;

    private ISourceService sourceService;

    #endregion

    #region Methods

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
        await this.sourceService.Create(newSource);

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
        var found = await this.sourceService.GetById(newSource.Source_Id);

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
        await this.sourceService.Delete(newSource.Source_Id);

        Assert.IsFalse(MockDataContext.Sources.Contains(newSource));
    }

    [Test]
    public void AddSourceToProjectTest()
    {
        var projectAndSources = new ProjectAndSources()
        {
            projectId = 1,
            sources = new List<int>()
            {
                { 2 }
            }
        };
        this.sourceService.AddSourcesToProject(projectAndSources);
        var expected = MockDataContext.ProjectAndSources.Where(x => x.projectId == projectAndSources.projectId);
        var contains = expected.ElementAt(0).sources.Contains(projectAndSources.sources.ElementAt(0));
        Assert.AreEqual(true, contains);
    }
    [Test]
    public void DeleteSourceToProjectTest()
    {
        var projectAndSources = new ProjectAndSources()
        {
            projectId = 1,
            sources = new List<int>()
            {
                { 3 }
            }
        };
        this.sourceService.DeleteSourceFromProject(projectAndSources);
        var expected = MockDataContext.ProjectAndSources.Where(x => x.projectId == projectAndSources.projectId);
        var contains = expected.ElementAt(0).sources.Contains(projectAndSources.sources.ElementAt(0));
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
        var sources = this.sourceService.GetAllNotInProject(projectId).Result;

        Assert.IsNotNull(sources);
        Assert.AreEqual(2, sources.Count());

    }
    [Test]
    public void GetAllInProjectTest()
    {
        var projectId = 1;
        var sources = this.sourceService.GetAllInProject(projectId).Result;

        Assert.IsNotNull(sources);
        Assert.AreEqual(1, sources.Count());

    }
    #endregion
}