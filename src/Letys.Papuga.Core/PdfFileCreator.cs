using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Collections.Generic;

namespace Letys.Parrot.Core
{
    public static class PdfFileCreator
    {
        private static readonly XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
        private static readonly XFont titleFont = new XFont("Times New Roman", 20, XFontStyle.Bold, options);
        private static readonly XFont descriptionFont = new XFont("Times New Roman", 18, XFontStyle.Italic, options);
        private static readonly XFont questionFont = new XFont("Times New Roman", 12, XFontStyle.Bold, options);
        private static readonly XFont answerFont = new XFont("Times New Roman", 12, XFontStyle.Regular, options);
        private static readonly XFont exampleFont = new XFont("Times New Roman", 11, XFontStyle.Italic, options);
        private const int MARGIN = 10;
        private const int MAXIMUM_PAGE_SIZE = 770;

        public static string CreateFlashcardPdfFile(TestSet testHeader)
        {
            var tempFileName = CreateTemporaryFilename();
            PdfDocument doc = CreatePdfFile(testHeader);
            doc.Save(tempFileName);

            return tempFileName;
        }

        private static PdfDocument CreatePdfDocument(TestSet testHeader)
        {
            var pdf = new PdfDocument();
            pdf.Info.Title = testHeader.Name;
            pdf.Info.Author = Consts.ApplicationName;
            pdf.Info.Subject = testHeader.Description;
            return pdf;
        }

        private static PdfDocument CreatePdfFile(TestSet testHeader)
        {
            PdfDocument pdf = CreatePdfDocument(testHeader);
            PdfPage pdfPage = pdf.AddPage();
            pdfPage.Size = PdfSharp.PageSize.A4;
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            int yPoint = MARGIN;

            WritePageItem(graph, testHeader.Name, titleFont, yPoint, pdfPage);

            if (!string.IsNullOrEmpty(testHeader.Description))
            {
                yPoint += 25;
                WritePageItem(graph, testHeader.Description, descriptionFont, yPoint, pdfPage);
            }

            if (testHeader.Items != null)
            {
                yPoint += 25;
                WriteTestItems(graph, testHeader.Items, yPoint, pdfPage, pdf);
            }

            return pdf;
        }

        private static void WriteTestItems(XGraphics graph, IList<TestItem> items, int position, PdfPage pdfPage, PdfDocument pdf)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (position > MAXIMUM_PAGE_SIZE)
                {
                    pdfPage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfPage);
                    position = MARGIN;
                }
                position += 20;
                WritePageItem(graph, items[i].Question, questionFont, position, pdfPage);
                position += 16;
                WritePageItem(graph, items[i].Answer, answerFont, position, pdfPage);

                if (!string.IsNullOrEmpty(items[i].Example))
                {
                    position += 16;
                    WritePageItem(graph, items[i].Example, exampleFont, position, pdfPage);
                }

                position += 15;
            }
        }

        private static void WritePageItem(XGraphics graph, string item, XFont font, int position, PdfPage pdfPage)
        {
            graph.DrawString(item, font, XBrushes.Black, new XRect(MARGIN, position, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
        }

        private static string CreateTemporaryFilename()
        {
            return $"{Path.GetTempPath()}{Guid.NewGuid()}.pdf";
        }
    }
}
