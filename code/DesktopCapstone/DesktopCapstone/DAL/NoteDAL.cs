using Dapper;
using desktop_capstone.DAL;
using desktop_capstone.model;
using DesktopCapstone.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DesktopCapstone.DAL
{
    public class NoteDAL
    {

        public ObservableCollection<Note> getNotesWithId(int id)
        {
            var connectionString = Connection.ConnectionString;
            var query = "select * from capstone.note where source_id = @Id";
            //var sourceList = new ObservableCollection<Source>();

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var noteList = new ObservableCollection<Note>(dbConnection.Query<Note>(query, new { Id = id }).ToList());
                return noteList;
            }
            //return null;
        }

        public bool addNewNote(Note newNote)
        {
            var connectionString = Connection.ConnectionString;
            var query = "insert into capstone.note (source_id, content, username) values (@SourceId, @Content::json, @Username)";
            var result = false;
            var rowsEffected = 0;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                rowsEffected = dbConnection.Execute(query, newNote);

            }
            if (rowsEffected > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
