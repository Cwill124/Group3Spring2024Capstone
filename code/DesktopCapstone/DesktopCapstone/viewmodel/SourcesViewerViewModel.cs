using System.Collections.ObjectModel;
using System.Diagnostics;
using DesktopCapstone.DAL;
using DesktopCapstone.model;

namespace DesktopCapstone.viewmodel;

/// <summary>
///     View model for the SourcesViewer window, providing data for source management.
/// </summary>
public class SourcesViewerViewModel
{
    #region Data members

    #endregion

    #region Properties

    /// <summary>
    ///     Gets the collection of sources.
    /// </summary>
    public ObservableCollection<Source> Sources { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourcesViewerViewModel" /> class with default values.
    /// </summary>
    public SourcesViewerViewModel()
    {
        this.Sources = new ObservableCollection<Source>();
        this.InitializeSources();
        Debug.WriteLine(this.Sources.Count);
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Refreshes the collection of sources from the data source.
    /// </summary>
    public void RefreshSources()
    {
        this.Sources.Clear();
        foreach (var source in DALConnection.SourceDAL.GetAllSources())
        {
            this.Sources.Add(source);
        }
    }

    /// <summary>
    ///     Initializes the collection of sources.
    /// </summary>
    private void InitializeSources()
    {
        this.Sources = DALConnection.SourceDAL.GetAllSources();
    }

    #endregion
}