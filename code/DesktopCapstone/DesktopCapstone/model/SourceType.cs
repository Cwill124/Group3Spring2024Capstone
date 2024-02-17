using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.model
{
    public class SourceType
    {
        public int SourceTypeId { get; set; }
        public string TypeName { get; set; }

        
        public override string ToString()
        {
            return this.TypeName;
        }
    }
}
