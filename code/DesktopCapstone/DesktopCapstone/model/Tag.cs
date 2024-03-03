using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int Note { get; set; }

        public bool Equals(Note other)
        {
            return this.TagId == other.NoteId;
        }
        public override string ToString()
        {
            return this.TagName;
        }

    }
}
