using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DesktopCapstone.model
{
    public class Source
    {

        private string description;
        private string name;
        private JsonObject content;
        private JsonObject metaData;
        private int sourceType;
        private JsonObject tags;
        private string createdBy;

        public string Description { get { return description; } }
        public string Name { get { return name; } }
        public JsonObject Content { get { return content; } }
        public JsonObject MetaData { get { return metaData;} }
        public int SourceType { get {  return sourceType; } }
        public JsonObject Tags { get { return tags; } }
        public string CreatedBy { get {  return createdBy; } }
    
        public Source ()
        {
            this.description = string.Empty;
            this.name = string.Empty;
            this.content = new JsonObject ();
            this.metaData = new JsonObject ();
            this.sourceType = 0;
            this.tags = new JsonObject ();
            this.createdBy = string.Empty;
            this.tags = new JsonObject ();

        }

        public Source (string description, string name, JsonObject content, JsonObject metaData, int sourceType, JsonObject tags, string createdBy)
        {
            this.description = description;
            this.name = name;
            this.content = content;
            this.metaData = metaData;
            this.sourceType = sourceType;
            this.tags = tags;
            this.createdBy = createdBy;
        }
    }
}
