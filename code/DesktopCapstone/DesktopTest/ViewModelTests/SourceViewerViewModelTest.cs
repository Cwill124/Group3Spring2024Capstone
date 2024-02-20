using DesktopCapstone.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTest.ViewModelTests
{
    [TestClass]
    public class SourceViewerViewModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ViewerViewModel viewerViewModel = new ViewerViewModel(1);
            Assert.AreEqual(1, viewerViewModel.CurrentSourceId);
        }

        
        
    }
}
