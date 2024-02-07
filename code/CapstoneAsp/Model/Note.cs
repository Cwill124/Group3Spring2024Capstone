namespace CapstoneASP.Model;

public class Note
{
    #region Properties

    public int NoteId { get; set; }

    public int SourceId { get; set; }

    public string? Content { get; set; }

    public string Username { get; set; }

    #endregion
}