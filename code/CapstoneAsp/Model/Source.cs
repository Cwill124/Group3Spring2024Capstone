namespace CapstoneASP.Model;

public class Source
{
    #region Properties

    public int SourceId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string MetaData { get; set; }

    public string Tags { get; set; }

    public int SourceTypeId { get; set; }

    public string CreatedBy { get; set; }

    #endregion
}