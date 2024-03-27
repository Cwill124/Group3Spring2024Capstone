using System.Collections.ObjectModel;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace DesktopCapstone.viewmodel;

/// <summary>
///     View model for the Viewer and VideoViewer window, providing data for source and note management.
/// </summary>
public class ViewerViewModel
{
    #region Data members

    private int currentSourceId;
    private NoteDAL noteDal;
    private SourceDAL sourceDal;

    #endregion

    #region Properties

    /// <summary>
    ///     Gets the collection of notes.
    /// </summary>
    public ObservableCollection<Note> Notes { get; private set; }

    /// <summary>
    ///     Gets the current source link.
    /// </summary>
    public Uri CurrentSourceLink { get; set; }

    /// <summary>
    ///     Gets or sets the current source ID.
    ///     When set, initializes the source link and refreshes notes for the new source.
    /// </summary>
    public int CurrentSourceId
    {
        get => this.currentSourceId;
        set
        {
            this.currentSourceId = value;
            this.initializeSourceLink();
            this.RefreshNotes();
        }
    }

    public int SourceType { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="ViewerViewModel" /> class with the specified current source ID.
    /// </summary>
    /// <param name="currentSourceId">The ID of the current source.</param>
    public ViewerViewModel(int currentSourceId, int sourceType, NoteDAL noteDal, SourceDAL sourceDal)
    {
        this.noteDal = noteDal;
        this.sourceDal = sourceDal;
        this.Notes = new ObservableCollection<Note>();
        this.SourceType = sourceType;
        this.CurrentSourceId = currentSourceId;
        this.initializeLists();
        //this.CurrentSourceId = currentSourceId;
        this.initializeSourceLink();
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Refreshes the collection of notes for the current source from the data source.
    /// </summary>
    public void RefreshNotes()
    {
        this.Notes.Clear();
        foreach (var note in this.noteDal.GetNoteById(this.currentSourceId))
        {
            this.Notes.Add(note);
        }
    }

    private void initializeLists()
    {
        this.Notes = this.noteDal.GetNoteById(this.currentSourceId);
    }

    private void initializeSourceLink()
    {
        //SourceDAL sourceDal = new SourceDAL();
        var source = this.sourceDal.GetSourceWithId(this.currentSourceId);
        var json = JObject.Parse(source.Content);
        var link = (string)json["url"];
        this.CurrentSourceLink = new Uri(link);
        if (this.SourceType == 2)
        {
            this.loadVideo();
        }
    }

    private void loadVideo()
    {
        if (this.CurrentSourceLink.ToString().Contains("youtube"))
        {
            var youtubeLink = this.CurrentSourceLink.ToString();
            var youtubeId = this.convertYoutubeLinkToId(youtubeLink);
            this.loadEmbeddedYoutubeVideo(youtubeId);
        }
    }

    private void loadEmbeddedYoutubeVideo(string youtubeId)
    {
        var youtubeLink = "https://www.youtube.com/embed/" + youtubeId;
        this.CurrentSourceLink = new Uri(youtubeLink);
    }

    private string convertYoutubeLinkToId(string youtubeLink)
    {
        var id = youtubeLink.Substring(youtubeLink.IndexOf('=') + 1);
        return id;
    }

    #endregion
}