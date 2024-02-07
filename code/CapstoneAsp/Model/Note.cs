namespace CapstoneASP.Model;

public class Note
{
    #region Properties

    public int NoteId { get; set; }

    public int SourceId { get; set; }

    public string? Content { get; set; }

    public string Username { get; set; }

    #endregion

    #region Methods

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Note otherNote = (Note)obj;
        return this.NoteId == otherNote.NoteId;
    }

    public override int GetHashCode()
    {
        return NoteId.GetHashCode();
    }
    #endregion
}