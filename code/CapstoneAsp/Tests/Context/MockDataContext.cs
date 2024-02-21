using CapstoneASP.Database.DBContext;
using Moq;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Model;

namespace CapstoneASP.Tests.Context
{
    public class MockDataContext : IDataContext
    {
        #region Data members

        private readonly IDbConnectionWrapper connectionWrapper;

        public static List<UserLogin> UserLogins => MockConnectionWrapper.UserLogins;

        public static List<User> Users => MockConnectionWrapper.Users;

        public static List<Source> Sources => MockConnectionWrapper.Sources;

        public static List<Note> Notes => MockConnectionWrapper.Notes;

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
}
