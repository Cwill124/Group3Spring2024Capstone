using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.model
{
    public class SourceType
    {

        private int sourceTypeId;
        private string typeName;

        public int SourceTypeId { get { return sourceTypeId; } }
        public string TypeName { get { return typeName; } }

        public SourceType()
        {
            this.sourceTypeId = 0;
            this.typeName = string.Empty;
        }

        public SourceType(int typeId, string typeName)
        {
            this.typeName = typeName;
            this.sourceTypeId = typeId;
        }
    }
}
