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
        private JsonObject content;
        private string username;

        public int SourceId { get { return sourceId; } }
        public JsonObject Content { get { return content; } }
        public string Username { get { return username; } }

        public Note()
        {
            this.sourceId = -1;
            this.content = new JsonObject();
            this.username = string.Empty;
        }

        public Note(int sourceId, JsonObject content, string username) {
            this.sourceId = sourceId;
            this.content = content;
            this.username = username;
        }

    }
}
