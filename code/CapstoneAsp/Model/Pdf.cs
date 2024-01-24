using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneASP.Model
{
    public class Pdf
    {
        [Column("name")]public string? Name { get; set; }
        [Column("id")] public int Id { get; set; }

        [Column("link")] public string? Link { get; set; }
    }
}
