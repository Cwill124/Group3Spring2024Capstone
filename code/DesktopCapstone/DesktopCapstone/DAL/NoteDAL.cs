using Dapper;
using desktop_capstone.DAL;
using DesktopCapstone.model;
using Npgsql;
using System.Collections.ObjectModel;
using System.Data;
using DesktopCapstone.util;

namespace DesktopCapstone.DAL
{
    /// <summary>
    /// Data Access Layer for handling operations related to Note entities.
    /// </summary>
    public class NoteDAL
    {

        private IDbConnection dbConnection;

        public NoteDAL(IDbConnection connection)
        {
            this.dbConnection = connection;
        }

        /// <summary>
        /// Retrieves a collection of notes by their associated ID.
        /// </summary>
        /// <param name="id">The ID of the notes to retrieve.</param>
        /// <returns>An ObservableCollection of Note objects.</returns>
        public ObservableCollection<Note> GetNoteById(int id)
        {
            var connection = this.dbConnection;

            //connection.Open();

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

        /// <summary>
        /// Creates a new note with the provided information.
        /// </summary>
        /// <param name="newNote">The Note object containing note details.</param>
        /// <returns>True if the note creation is successful; otherwise, false.</returns>
        public bool CreateNote(Note newNote)
        {
            var connectionString = Connection.ConnectionString;
            var result = false;
            var rowsEffected = 0;


            rowsEffected = dbConnection.Execute(SqlConstants.CreateNewNote, newNote);


            if (rowsEffected > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Deletes a note by its ID.
        /// </summary>
        /// <param name="id">The ID of the note to be deleted.</param>
        public void DeleteNoteById(int id)
        {
            using var connection = new NpgsqlConnection(Connection.ConnectionString);

            connection.Open();

            connection.Execute(SqlConstants.DeleteNoteById, new { id });
        }
    }
}
