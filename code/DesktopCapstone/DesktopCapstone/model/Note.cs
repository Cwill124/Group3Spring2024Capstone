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
        private int sourceId;
        private string content;
        private string username;

        public int SourceId { get { return sourceId; } }
        public string Content { get { return content; } }
        public string Username { get { return username; } }

        public Note()
        {
            this.sourceId = -1;
            this.content = string.Empty;
            this.username = string.Empty;
        }

        public Note(int sourceId, string content, string username) {
            this.sourceId = sourceId;
            this.content = content;
            this.username = username;
        }

        public override string ToString()
        {
            var json = JObject.Parse(content);

            return (string)json["noteTitle"] + " - " + (string)json["noteContent"];
        }
    }
}
