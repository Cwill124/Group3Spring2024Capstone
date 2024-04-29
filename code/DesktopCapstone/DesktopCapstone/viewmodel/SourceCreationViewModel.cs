using System.Collections.ObjectModel;
using DesktopCapstone.DAL;
using DesktopCapstone.model;

namespace DesktopCapstone.viewmodel;

/// <summary>
///     View model for the SourceCreation window, providing data for source type and format selection.
/// </summary>
public class SourceCreationViewModel
{
    #region Data members

    private SourceDAL dal;
    #endregion

    #region Properties

    /// <summary>
    ///     Gets the collection of source types.
    /// </summary>
    public ObservableCollection<SourceType> SourceTypes { get; private set; }


    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourceCreationViewModel" /> class with default values.
    /// </summary>
    public SourceCreationViewModel(SourceDAL dal)
    {
        this.dal = dal;
        this.SourceTypes = new ObservableCollection<SourceType>();
        this.InitializeLists();
        
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Initializes the collections of source types and formats.
    /// </summary>
    private void InitializeLists()
    {
        this.SourceTypes = this.dal.GetSourceTypes();

        
    }

    #endregion
}