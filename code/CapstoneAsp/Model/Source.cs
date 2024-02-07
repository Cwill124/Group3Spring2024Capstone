namespace CapstoneASP.Model
{
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

        #region Methods

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Source otherSource = (Source)obj;
            return this.SourceId == otherSource.SourceId;
        }

        public override int GetHashCode()
        {
            return SourceId.GetHashCode();
        }

        #endregion
    }
}