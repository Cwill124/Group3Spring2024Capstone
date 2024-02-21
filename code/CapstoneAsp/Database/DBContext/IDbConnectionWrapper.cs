using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.AccessControl;
using System.Xml.Linq;
using CapstoneASP.Model;
using CapstoneASP.Util;
using NUnit.Framework.Constraints;

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


        #region Properties 

        public static List<Note> Notes { get; private set; } = null!;

        public static List<Source> Sources { get; private set; } = null!;

        public static List<User> Users { get; private set; } = null!;

        public static List<UserLogin> UserLogins { get; private set; } = null!;

        #endregion

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
            UserLogins = new List<UserLogin>
            {
                new()
                {
                    UserId = 1,
                    Username = "User 1",
                    Password = "Password 1"
                },
                new()
                {
                    UserId = 2,
                    Username = "User 2",
                    Password = "Password 2",
                }
            };
            Users = new List<User>
            {
                new()
                {
                    Username = "User 1",
                    Email = "test@gmail.com",
                    Firstname = "Fname 1",
                    Lastname = "Lname 2",
                    Password = "",
                    Phone = "6789081232"

                },
                new()
                {
                    Username = "User 2",
                    Email = "test2@gmail.com",
                    Firstname = "Fname 2",
                    Lastname = "Lname 2",
                    Password = "",
                    Phone = "6789081234"

                }
            };
            Sources = new List<Source>
            {
                new()
                {
                    SourceId = 1,
                    Content = "",
                    CreatedBy = "User 1",
                    Description = "nothing",
                    MetaData = "",
                    Name = "Source 1",
                    SourceTypeId = 1,
                    Tags = ""
                },
                new()
                {
                    SourceId = 2,
                    Content = "",
                    CreatedBy = "User 2",
                    Description = "nothing",
                    MetaData = "",
                    Name = "Source 2",
                    SourceTypeId = 2,
                    Tags = ""
                },
                new()
                {
                    SourceId = 3,
                    Content = "",
                    CreatedBy = "User 2",
                    Description = "nothing",
                    MetaData = "",
                    Name = "Source 3",
                    SourceTypeId = 1,
                    Tags = ""
                }
            };
            Notes = new List<Note>
            {
                new()
                {
                    Content = "",
                    NoteId = 1,
                    SourceId = 1,
                    Username = "User 1"
                },
                new()
                {
                    Content = "",
                    NoteId = 2,
                    SourceId = 2,
                    Username = "User 2"
                },
                new()
                {
                    Content = "",
                    NoteId = 3,
                    SourceId = 1,
                    Username = "User 1"
                }
            };

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
            IEnumerable<T> list = null;

            if (typeof(T) == typeof(UserLogin))
            {
                switch (sql)
                {
                    case SqlConstants.GetUserLogin:
                        var userLogins = UserLogins.Where(x =>
                        {
                            var paramId = param?.GetType().GetProperty("Username")?.GetValue(param, null);
                            var id = (string)(paramId ?? throw new ArgumentNullException());
                            return x.Username == id;
                        });
                        list = (IEnumerable<T>)userLogins;
                        break;
                    default:
                        throw new InvalidCastException();
                }
            } else if (typeof(T) == typeof(User))
            {
                switch (sql)
                {
                    case SqlConstants.GetUserByUsername:
                        var user = Users.Where(x =>
                        {
                            var paramId = param?.GetType().GetProperty("Username")?.GetValue(param, null);
                            var id = (string)(paramId ?? throw new ArgumentNullException());
                            return x.Username == id;
                        }); 
                        list = (IEnumerable<T>)user;
                        break;
                    default:
                        throw new InvalidCastException();
                }
            }

            return Task.FromResult(list); ;
        }

        public Task<int> ExecuteAsync(string sql, object? param = null)
        {
            if (param?.GetType() == typeof(UserLogin))
            {
                if (sql.Contains("INSERT"))
                {
                    UserLogins.Add((UserLogin)param);
                }
            } else if (param?.GetType() == typeof(User))
            {
                if (sql.Contains("INSERT"))
                {
                    Users.Add((User)param);
                }
            }
            else
            {
                throw new InvalidCastException();
            }

            return Task.FromResult(0);
        }
    }

}
