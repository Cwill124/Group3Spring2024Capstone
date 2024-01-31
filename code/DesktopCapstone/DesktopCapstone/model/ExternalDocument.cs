using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_capstone.model
{
    public class ExternalDocument
    {
        private int id;
        private string name;
        private string link;

        public int Id { get { return this.id; } }
        public string Name { get { return this.name; } }
        public string Link { get { return this.link; } }

        public ExternalDocument()
        {
            this.id = 0;
            this.name = String.Empty;
            this.link = String.Empty;
        }

        public ExternalDocument(int id, string name, string link)
        {
            this.id = id;
            this.name = name;
            this.link = link;
        }

    }
}
