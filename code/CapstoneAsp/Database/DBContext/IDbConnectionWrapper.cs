using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.DBContext
{
    /// <summary>
    /// Represents a wrapper for IDbConnection with additional asynchronous query and execution methods.
    /// </summary>
    public interface IDbConnectionWrapper : IDbConnection
    {
        #region Methods

        /// <summary>
        /// Asynchronously executes a query and returns the result set.
        /// </summary>
        /// <typeparam name="T">The type of the result set elements.</typeparam>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="param">The parameters for the SQL query.</param>
        /// <returns>Returns a task with an IEnumerable<T> representing the result set.</returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);

        /// <summary>
        /// Asynchronously executes a SQL command and returns the number of affected rows.
        /// </summary>
        /// <param name="sql">The SQL command to execute.</param>
        /// <param name="param">The parameters for the SQL command.</param>
        /// <returns>Returns a task with an int representing the number of affected rows.</returns>
        Task<int> ExecuteAsync(string sql, object? param = null);

        #endregion
    }

    /// <summary>
    /// Represents a wrapper for NpgsqlConnection implementing IDbConnectionWrapper.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NpgsqlConnectionWrapper : IDbConnectionWrapper
    {
        #region Data members

        private readonly IDbConnection connection;

        #endregion

        #region Properties

        /// <inheritdoc />
        public string ConnectionString
        {
            get => this.connection.ConnectionString;
            set => this.connection.ConnectionString = value;
        }

        /// <inheritdoc />
        public int ConnectionTimeout => this.connection.ConnectionTimeout;

        /// <inheritdoc />
        public string Database => this.connection.Database;

        /// <inheritdoc />
        public ConnectionState State => this.connection.State;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NpgsqlConnectionWrapper"/> class.
        /// </summary>
        /// <param name="connection">The IDbConnection to be wrapped.</param>
        public NpgsqlConnectionWrapper(IDbConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public IDbCommand CreateCommand()
        {
            return this.connection.CreateCommand();
        }

        /// <inheritdoc />
        public IDbTransaction BeginTransaction()
        {
            return this.connection.BeginTransaction();
        }

        /// <inheritdoc />
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return this.connection.BeginTransaction(il);
        }

        /// <inheritdoc />
        public void ChangeDatabase(string databaseName)
        {
            this.connection.ChangeDatabase(databaseName);
        }

        /// <inheritdoc />
        public void Close()
        {
            this.connection.Close();
        }

        /// <inheritdoc />
        public void Open()
        {
            this.connection.Open();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.connection.Dispose();
        }

        /// <inheritdoc />
        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
        {
            return this.connection.QueryAsync<T>(sql, param);
        }

        /// <inheritdoc />
        public Task<int> ExecuteAsync(string sql, object? param = null)
        {
            return this.connection.ExecuteAsync(sql, param);
        }

        #endregion
    }
}
[ExcludeFromCodeCoverage]
public class MockConnectionWrapper : IDbConnectionWrapper
{
    #region Properties

    public string ConnectionString { get; set; }

    public int ConnectionTimeout => 30;
    public string Database => "MockDatabase";
    public ConnectionState State => ConnectionState.Open;

    public static List<Note> Notes { get; private set; } = null!;

    public static List<Source> Sources { get; private set; } = null!;

    public static List<User> Users { get; private set; } = null!;

    public static List<UserLogin> UserLogins { get; private set; } = null!;

    public static List<Tags> Tags { get; private set; } = null!;

    public static List<Project> Projects { get; private set; } = null!;

    public static List<ProjectAndSources> ProjectAndSources { get; private set; } =null!;

    #endregion

    #region Constructors

    public MockConnectionWrapper(IDbConnection connection)
    {
        this.ConnectionString = connection.ConnectionString;
    }

    #endregion

    #region Methods

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
                Password = "AQAAAAIAAYagAAAAEB+6mEL/6DOZRYw2oba0x3oACXGLL04rTzCzGIyR4Zpe31uBoYZhoI1BlxvFNKz5Yw=="
            },
            new()
            {
                UserId = 2,
                Username = "User 2",
                Password = "Password 2"
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
                Source_Id = 1,
                Content = "",
                Created_By = "User 1",
                Description = "nothing",
                Meta_Data = "",
                Name = "Source 1",
                Source_Type_Id = 1,
                Tags = ""
            },
            new()
            {
                Source_Id = 2,
                Content = "",
                Created_By = "User 2",
                Description = "nothing",
                Meta_Data = "",
                Name = "Source 2",
                Source_Type_Id = 2,
                Tags = ""
            },
            new()
            {
                Source_Id = 3,
                Content = "",
                Created_By = "User 2",
                Description = "nothing",
                Meta_Data = "",
                Name = "Source 3",
                Source_Type_Id = 1,
                Tags = ""
            }
        };
        Notes = new List<Note>
        {
            new()
            {
                Content = "",
                Note_Id = 1,
                Source_Id = 1,
                Username = "User 1"
            },
            new()
            {
                Content = "",
                Note_Id = 2,
                Source_Id = 2,
                Username = "User 2"
            },
            new()
            {
                Content = "",
                Note_Id = 3,
                Source_Id = 1,
                Username = "User 1"
            }
        };
        Tags = new List<Tags>
        {
            new()
            {
                Note = 1,
                Tag = "tag",
                TagId = 1
            },
            new()
            {
                Note = 1,
                Tag = "tag2",
                TagId = 2
            }

        };
        Projects = new List<Project>()
        {
            new()
            {
                Description = "Test Description 1",
                Owner = "User 1",
                ProjectId = 1,
                Title = "Test Title 1"
            },
            new()
            {
                Description = "Test Description 2",
                Owner = "User 2",
                ProjectId = 2,
                Title = "Test Title 2"
            }
        };
        ProjectAndSources = new List<ProjectAndSources>()
        {
            new()
            {
                projectId = 1,
                sources = new List<int>()
                {
                    { 1 }
                }
            },
            new()
            {
                projectId = 2,
                sources = new List<int>()
                {
                    { 2 },
                    { 3 }
                }
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
        }
        else if (typeof(T) == typeof(User))
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
        else if (typeof(T) == typeof(Source))
        {
            switch (sql)
            {
                case SqlConstants.GetSourcesByUsername:
                    var sources = Sources.Where(x =>
                    {
                        var paramId = param?.GetType().GetProperty("username")?.GetValue(param, null);
                        var id = (string)(paramId ?? throw new ArgumentNullException());
                        return x.Created_By == id;
                    });
                    list = (IEnumerable<T>)sources;
                    break;
                case SqlConstants.GetSourceById:
                    var source = Sources.Where(x =>
                    {
                        var paramId = param?.GetType().GetProperty("id")?.GetValue(param, null);
                        var id = (int)(paramId ?? throw new ArgumentNullException());
                        return x.Source_Id == id;
                    });
                    list = (IEnumerable<T>)source;
                    break;
                case SqlConstants.GetSourcesNotInProject:
                    var paramId = param?.GetType().GetProperty("projectId")?.GetValue(param, null);

                    if (paramId == null)
                    {
                        throw new ArgumentNullException(nameof(paramId), "projectId cannot be null");
                    }

                    var id = (int)paramId;

                    var projectAndSources = ProjectAndSources.FirstOrDefault(y => y.projectId == id);

                    if (projectAndSources == null)
                    {
                        throw new ArgumentException($"No ProjectAndSources found with projectId: {id}", nameof(paramId));
                    }

                    var notInSourcesIds = Sources
                        .Where(x => !projectAndSources.sources.Contains(x.Source_Id))
                        .Select(x => x)
                        .ToList();
                    list = (IEnumerable<T>)notInSourcesIds;
                    break;
                case SqlConstants.GetSourcesInProject:
                    var paramId2 = param?.GetType().GetProperty("projectId")?.GetValue(param, null);

                    if (paramId2 == null)
                    {
                        throw new ArgumentNullException(nameof(paramId2), "projectId cannot be null");
                    }

                    var id2 = (int)paramId2;

                    var projectAndSources2 = ProjectAndSources.FirstOrDefault(y => y.projectId == id2);

                    if (projectAndSources2 == null)
                    {
                        throw new ArgumentException($"No ProjectAndSources found with projectId: {id2}", nameof(paramId));
                    }

                    var InSourcesIds = Sources
                        .Where(x => projectAndSources2.sources.Contains(x.Source_Id))
                        .Select(x => x)
                        .ToList();
                    list = (IEnumerable<T>)InSourcesIds;
                    break;
                default:
                    throw new InvalidCastException();
            }
        }
        else if (typeof(T) == typeof(Note))
        {
            switch (sql)
            {
                case SqlConstants.GetNotesBySourceId:
                    var notes = Notes.Where(x =>
                    {
                        var paramId = param?.GetType().GetProperty("sourceId")?.GetValue(param, null);
                        var id = (int)(paramId ?? throw new ArgumentNullException());
                        return x.Source_Id == id;
                    });
                    list = (IEnumerable<T>)notes;
                    break;
                case SqlConstants.GetNoteLastAdded:
                    var lastNote = Notes.Where(x => x.Note_Id == 5);
                    list = (IEnumerable<T>)lastNote;
                    break;
                default:
                    throw new InvalidCastException();
            }
        } else if (typeof(T) == typeof(Project))
        {
            switch (sql)
            {
                case SqlConstants.GetAllProjectsByOwner:
                    var projects = Projects.Where(x =>
                    {
                        var paramId = param?.GetType().GetProperty("owner")?.GetValue(param);
                        var owner = (string) (paramId ?? throw new ArgumentNullException());
                        return x.Owner.Equals(owner);
                    });
                    list = (IEnumerable<T>)projects;
                    break;
                case SqlConstants.GetProjectById:
                    var currentProject = Projects.Where(x =>
                    {
                        var paramId = param?.GetType().GetProperty("id")?.GetValue(param, null);
                        var id = (int)(paramId ?? throw new ArgumentNullException());
                        return x.ProjectId == id;
                    });
                    list = (IEnumerable<T>)currentProject;
                    break;
                default:
                    throw new InvalidCastException();
            }

        }
        else {
            if(sql.Equals(SqlConstants.GetTagByNoteId)){
                var tags = Tags.Where(x =>
                {
                    var paramId = param?.GetType().GetProperty("noteId")?.GetValue(param, null);
                    var id = (int)(paramId ?? throw new ArgumentNullException());
                    return x.Note == id;
                });
                list = (IEnumerable<T>)tags;
            } 
            else {

                return Task.FromResult(list);
            }
        }

        return Task.FromResult(list);
         
    }

    public Task<int> ExecuteAsync(string sql, object? param = null)
    {
        if (param?.GetType() == typeof(UserLogin))
        {
            if (sql.Contains("INSERT"))
            {
                UserLogins.Add((UserLogin)param);
            }
        }
        else if (param?.GetType() == typeof(User))
        {
            if (sql.Contains("INSERT"))
            {
                Users.Add((User)param);
            }
        }
        else if (param?.GetType() == typeof(Source))
        {
            if (sql.Contains("INSERT"))
            {
                Sources.Add((Source)param);
            }

            if (sql.Contains("DELETE"))
            {
                Sources.Remove((Source)param);
            }
        }
        else if (param?.GetType() == typeof(Note))
        {
            if (sql.Contains("INSERT"))
            {
                Notes.Add((Note)param);
            }
        } 
        else if (param?.GetType() == typeof(Tags))
        {
            if (sql.Contains("INSERT"))
            {
                Tags.Add((Tags)param);
            }
        }
        else if (param?.GetType() == typeof(Project))
        {
            if (sql.Contains("INSERT"))
            {
                Projects.Add((Project)param);
            } 
        }
        else
        {
            if (SqlConstants.DeleteById.Equals(sql))
            {
                var idProperty = param.GetType().GetProperty("id");

                if (idProperty != null && idProperty.PropertyType == typeof(int))
                {
                    var idValue = (int)idProperty.GetValue(param, null);

                    var sourceToRemove = Sources.FirstOrDefault(x => x.Source_Id == idValue);

                    if (sourceToRemove != null)
                    {
                        Sources.Remove(sourceToRemove);
                    }
                }
            }
            else if (SqlConstants.DeleteNote.Equals(sql))
            {
                var idProperty = param.GetType().GetProperty("noteId");

                if (idProperty != null && idProperty.PropertyType == typeof(int))
                {
                    var idValue = (int)idProperty.GetValue(param, null);

                    var notesToRemove = Notes.FirstOrDefault(x => x.Note_Id == idValue);

                    if (notesToRemove != null)
                    {
                        Notes.Remove(notesToRemove);
                    }
                }
            }
            else if(SqlConstants.DeleteTagById.Equals(sql))
            {
                var idProperty = param.GetType().GetProperty("tagId");
                if (idProperty != null && idProperty.PropertyType == typeof(int))
                {
                    var idValue = (int)idProperty.GetValue(param, null);

                    var tagToRemove = Tags.FirstOrDefault(x => x.TagId == idValue);
                    if (tagToRemove != null)
                    {
                        Tags.Remove(tagToRemove);
                    }
                }
            } else if (SqlConstants.DeleteProject.Equals(sql))
            {
                var idProperty = param.GetType().GetProperty("id");
                if (idProperty != null && idProperty.PropertyType == typeof(int))
                {
                    var idValue = (int)idProperty.GetValue(param, null);

                    var projectToRemove = Projects.FirstOrDefault(x => x.ProjectId == idValue);
                    if (projectToRemove != null)
                    {
                        Projects.Remove(projectToRemove);
                    }
                }
            } else if (SqlConstants.AddSourceToProject.Equals(sql))
            {
                var sourceId = param.GetType().GetProperty("sourceId");
                var projectId = param.GetType().GetProperty("projectId");
                if ((sourceId != null && sourceId.PropertyType == typeof(int)) &&
                    (projectId != null && projectId.PropertyType == typeof(int)))
                {
                    var sourceIdValue = (int)sourceId.GetValue(param, null);
                    var projectIdValue = (int)projectId.GetValue(param, null);
                    var projectToAdd = ProjectAndSources.FirstOrDefault(x => x.projectId == projectIdValue);
                    if (projectToAdd != null)
                    {
                        projectToAdd.sources.Add(sourceIdValue);
                    }
                }
            } else if (SqlConstants.DeleteSourceFromProject.Equals(sql))
            {
                var sourceId = param.GetType().GetProperty("sourceId");
                var projectId = param.GetType().GetProperty("projectId");
                if ((sourceId != null && sourceId.PropertyType == typeof(int)) &&
                    (projectId != null && projectId.PropertyType == typeof(int)))
                {
                    var sourceIdValue = (int)sourceId.GetValue(param, null);
                    var projectIdValue = (int)projectId.GetValue(param, null);
                    var projectToAdd = ProjectAndSources.FirstOrDefault(x => x.projectId == projectIdValue);
                    if (projectToAdd != null)
                    {
                        projectToAdd.sources.Remove(sourceIdValue);
                    }
                }
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        return Task.FromResult(0);
    }

    #endregion
}