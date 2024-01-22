using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;

#pragma warning disable CS8767

namespace webapi.Database.Context;

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