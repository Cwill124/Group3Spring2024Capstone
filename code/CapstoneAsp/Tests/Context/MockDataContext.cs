using System.Data;
using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using Moq;

namespace CapstoneASP.Tests.Context;

public class MockDataContext : IDataContext
{
    #region Data members

    private readonly IDbConnectionWrapper connectionWrapper;

    #endregion

    #region Properties

    public static List<UserLogin> UserLogins => MockConnectionWrapper.UserLogins;

    public static List<User> Users => MockConnectionWrapper.Users;

    public static List<Source> Sources => MockConnectionWrapper.Sources;

    public static List<Note> Notes => MockConnectionWrapper.Notes;

    public static List<Tags> Tags => MockConnectionWrapper.Tags;

    #endregion

    #region Constructors

    public MockDataContext()
    {
        var connection = new Mock<IDbConnection>();
        this.connectionWrapper = new MockConnectionWrapper(connection.Object);
        this.connectionWrapper.Open();
    }

    #endregion

    #region Methods

    public Task<IDbConnectionWrapper> CreateConnection()
    {
        return Task.FromResult(this.connectionWrapper);
    }

    public void Init()
    {
    }

    #endregion
}