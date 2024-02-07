using desktop_capstone.DAL;
using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DesktopCapstone.viewmodel
{
    public class SourceCreationViewModel
    {
        ObservableCollection<SourceType> sourceTypes;
        ObservableCollection<string> sourceFormat;

        public SourceCreationViewModel() {
            this.initializeLists();
        }

        public JsonObject getJsonFromUrl(string url) {
            JsonObject urlJson = new JsonObject(url);
            return null;
        }
            

        public void addNewSource(Source sourceToAdd)
        {
            SourceDAL sourceDal = new SourceDAL();
            sourceDal.addNewSource(sourceToAdd);
        }

        private void initializeLists()
        {
            SourceDAL sourceDal = new SourceDAL();
            sourceTypes = sourceDal.getSourceTypes();

            sourceFormat = ["URL", "File"];
        }
    }
}
