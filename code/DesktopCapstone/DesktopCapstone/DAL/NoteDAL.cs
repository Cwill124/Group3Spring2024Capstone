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
using DesktopCapstone.util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DesktopCapstone.DAL
{
    public class NoteDAL
    {

        public ObservableCollection<Note> GetNoteById(int id)
        {
            var connectionString = Connection.ConnectionString;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var noteList = new ObservableCollection<Note>(dbConnection.Query<Note>(SqlConstants.GetNotesById, new { Id = id }).ToList());
                return noteList;
            }
        }

        public bool CreateNote(Note newNote)
        {
            var connectionString = Connection.ConnectionString;
            var result = false;
            var rowsEffected = 0;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                rowsEffected = dbConnection.Execute(SqlConstants.CreateNewNote, newNote);

            }
            if (rowsEffected > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
