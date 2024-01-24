using Microsoft.EntityFrameworkCore;
using System;
using Npgsql;

namespace CapstoneASP.Database.DBContext
{
    public class DBContext : DbContext
    {
        public NpgsqlConnection Connection => new NpgsqlConnection(Database.GetDbConnection().ConnectionString);
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
    }
}
