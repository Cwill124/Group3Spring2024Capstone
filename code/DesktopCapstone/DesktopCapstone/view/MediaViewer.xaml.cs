using desktop_capstone.DAL;
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace desktop_capstone.view
{
    /// <summary>
    /// Interaction logic for MediaViewer.xaml
    /// </summary>
    public partial class MediaViewer : Window
    {
        public MediaViewer()
        {
            InitializeComponent();
            this.startPdfViewer();
            this.startVideoViewer();
        }

        //private void loadPdfViewer()
        //{
        //    var pdfLink = new DocumentDAL().getSpecifiedDocument(1);
        //    if (pdfLink != null)
        //    {
        //        webPdfViewer.Navigate(new Uri(pdfLink.Link));
        //    }
        //    Debug.WriteLine("link null");
        //    //webPdfViewer.Navigate(new Uri(pdfLink.Link));
        //}

        //private async void test()
        //{
        //    HttpClient client = new HttpClient();
        //    var pdfLink = new DocumentDAL().getSpecifiedDocument(1);

        //    client.BaseAddress = new Uri(pdfLink.Link);
        //    var pdfViewer = new PdfViewer();

        //    var bytes = await client.GetByteArrayAsync(pdfLink.Link);

        //    using (var pdfStream = new MemoryStream(bytes))
        //    {
        //        pdfViewer.Document = PdfDocument.Load(pdfStream);
        //    }
        //    host.Child = pdfViewer;
        //}

        private async void startPdfViewer()
        {
            await webViewPdf.EnsureCoreWebView2Async();
            var pdfLink = new DocumentDAL().getSpecifiedDocument(1);
            webViewPdf.CoreWebView2.Navigate(pdfLink.Link);
        }

        private async void startVideoViewer()
        {
            await webViewVideo.EnsureCoreWebView2Async();
            var videoLink = new VideoDal().getSpecifiedVideo(1);
            webViewVideo.CoreWebView2.Navigate(videoLink.Link);
        }
    }
}
