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

        public  ObservableCollection<Note> GetNoteById(int id)
        {
            using var connection = new NpgsqlConnection(Connection.ConnectionString);

            connection.Open();

            var notes = new ObservableCollection<Note>();

            var result = connection.Query<dynamic>(SqlConstants.GetNotesById, new { id });

            foreach (var item in result.ToList())
            {
                var newNote = new Note()
                {
                    Content = item.content,
                    SourceId = item.source_id,
                    NoteId = item.note_id,
                    Username = item.username
                };
                notes.Add(newNote);
            }

            return notes;
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

        public void DeleteNoteById(int id)
        {
            using var connection = new NpgsqlConnection(Connection.ConnectionString);

            connection.Open();

            connection.Execute(SqlConstants.DeleteNoteById, new { id });
        }
    }
}
