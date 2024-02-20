using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace CapstoneASP.Database.DBContext
{

    public interface IDbConnectionWrapper : IDbConnection
    {
        #region Methods

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);

        Task<int> ExecuteAsync(string sql, object? param = null);

        #endregion
    }


    [ExcludeFromCodeCoverage]
    public class NpgsqlConnectionWrapper : IDbConnectionWrapper
    {
        #region Data members

        private readonly IDbConnection connection;

        #endregion

        #region Properties

        public string ConnectionString
        {
            get => this.connection.ConnectionString;
            set => this.connection.ConnectionString = value;
        }

        public int ConnectionTimeout => this.connection.ConnectionTimeout;
        public string Database => this.connection.Database;
        public ConnectionState State => this.connection.State;

        #endregion

        #region Constructors

        public NpgsqlConnectionWrapper(IDbConnection connection)
        {
            this.connection = connection;
        }

        #endregion

        #region Methods

        public IDbCommand CreateCommand()
        {
            return this.connection.CreateCommand();
        }

        public IDbTransaction BeginTransaction()
        {
            return this.connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return this.connection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            this.connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            this.connection.Close();
        }

        public void Open()
        {
            this.connection.Open();
        }

        public void Dispose()
        {
            this.connection.Dispose();
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
        {
            return this.connection.QueryAsync<T>(sql, param);
        }

        public Task<int> ExecuteAsync(string sql, object? param = null)
        {
            return this.connection.ExecuteAsync(sql, param);
        }

        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class MockConnectionWrapper : IDbConnectionWrapper
    {


        public string ConnectionString { get; set; }

        public int ConnectionTimeout => 30;
        public string Database => "MockDatabase";
        public ConnectionState State => ConnectionState.Open;

        #region Constructors

        public MockConnectionWrapper(IDbConnection connection)
        {
            this.ConnectionString = connection.ConnectionString;
        }

        #endregion

       

        public IDbCommand CreateCommand()
        {
            throw new NotSupportedException("Mock implementation: CreateCommand is not supported.");
        }

        public IDbTransaction BeginTransaction()
        {
            throw new NotSupportedException("Mock implementation: BeginTransaction is not supported.");
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotSupportedException("Mock implementation: BeginTransaction is not supported.");
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotSupportedException("Mock implementation: ChangeDatabase is not supported.");
        }

        public void Open()
        {
            throw new NotSupportedException();
        }
        public void Close()
        {
            throw new NotImplementedException("Mock implementation: Close is not supported.");
        }
        public void Dispose()
        {
           
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteAsync(string sql, object? param = null)
        {
            throw new NotImplementedException();
        }
    }

}
