using Dapper;
using desktop_capstone.DAL;
using DesktopCapstone.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.DAL
{
    public class NoteDAL
    {

        public ObservableCollection<Note> getAllNotes()
        {
            var connectionString = Connection.ConnectionString;
            var query = "select * from capstone.note";
            //var sourceList = new ObservableCollection<Source>();

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var noteList = new ObservableCollection<Note>(dbConnection.Query<Note>(query).ToList());
                return noteList;
            }
            //return null;
        }
    }
}
