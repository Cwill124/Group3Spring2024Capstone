using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CapstoneASP.Model
{
    public class Source
    {
        [Column("name")] public string Name { get; set; }

        [Column("description")] public string Description { get; set; }

        
        [Column("content")]
        public string? Content { get; set; }

       
        [Column("metadata")]
        public string? MetaData { get; set; }

        
        [Column("tags")]
        public string? Tags { get; set; }


        [Column("source_type_id")] public int SourceTypeId { get; set; }

        [Column("created_by")] public string CreatedBy { get; set; }
    }
}
