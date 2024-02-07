using DesktopCapstone.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTest.ViewModelTests
{
    [TestClass]
    public class PDFViewerViewModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            PDFViewerViewModel pdfViewerViewModel = new PDFViewerViewModel(1);
            Assert.AreEqual(1, pdfViewerViewModel.CurrentSourceId);
        }

        
        
    }
}
