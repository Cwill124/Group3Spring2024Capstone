using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DesktopCapstone.model
{
    public class Note
    {
        public int SourceId { get; set; }

        public int NoteId { get; set; }

        public string Content { get; set; }
        public string Username { get; set; }

        public override string ToString()
        {
            var json = JObject.Parse(Content);

            return (string)json["noteTitle"] + "\n" + (string)json["noteContent"];
        }
    }
}
