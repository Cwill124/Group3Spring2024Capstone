using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.model
{
    /// <summary>
    /// The project class is used to represent a project in the database that can contain sources
    /// </summary>
    public class Project
    {
        /// <summary>
        /// The unique identifier for the project
        /// </summary>
        public int? ProjectId { get; set; }
        /// <summary>
        /// The given title of the project
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The given description of the project
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The user that created the project
        /// </summary>
        public string Owner { get; set;  }

        /// <summary>
        /// Returns a string representation of the project
        /// </summary>
        /// <returns> a string representation of the project </returns>
        public override string ToString()
        {
            return Title + " -  Description: " + Description;
        }
    }

    
}
