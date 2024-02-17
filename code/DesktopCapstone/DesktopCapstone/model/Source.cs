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
        //private int sourceId;
        //private string description;
        //private string name;
        //private string content;
        //private string metaData;
       // private int sourceType;
        //private string tags;
        //private string createdBy;

        public int? SourceId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string MetaData { get; set; }
        public int SourceType { get; set; }
        public string Tags { get; set;  }
        public string CreatedBy { get; set; }
    
        //public Source ()
        //{
        //    this.SourceId = 0;
        //    this.Description = string.Empty;
        //    this.Name = string.Empty;
        //    this.Content = string.Empty;
        //    this.MetaData = string.Empty;
        //    this.SourceType = 0;
        //    this.Tags = string.Empty;
        //    this.CreatedBy = string.Empty;
        //    this.Tags = string.Empty;
        //}

        //public Source (int sourceId, string description, string name, string content, string metaData, int sourceType, string tags, string createdBy)
        //{
        //    this.SourceId = sourceId;
        //    this.Description = description;
        //    this.Name = name;
        //    this.Content = content;
        //    this.MetaData = metaData;
        //    this.SourceType = sourceType;
        //    this.Tags = tags;
        //    this.CreatedBy = createdBy;
        //}

        override
        public string ToString()
        {
            return Name;
        }
    }
}
