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
        private string content;
        private string metaData;
        private int sourceType;
        private string tags;
        private string createdBy;

        public string Description { get { return description; } }
        public string Name { get { return name; } }
        public string Content { get { return content; } }
        public string MetaData { get { return metaData;} }
        public int SourceType { get {  return sourceType; } }
        public string Tags { get { return tags; } }
        public string CreatedBy { get {  return createdBy; } }
    
        public Source ()
        {
            this.description = string.Empty;
            this.name = string.Empty;
            this.content = string.Empty;
            this.metaData = string.Empty;
            this.sourceType = 0;
            this.tags = string.Empty;
            this.createdBy = string.Empty;
            this.tags = string.Empty;

        }

        public Source (string description, string name, string content, string metaData, int sourceType, string tags, string createdBy)
        {
            this.description = description;
            this.name = name;
            this.content = content;
            this.metaData = metaData;
            this.sourceType = sourceType;
            this.tags = tags;
            this.createdBy = createdBy;
        }

        override
        public string ToString()
        {
            return Name;
        }
    }
}
