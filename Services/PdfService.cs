using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Image;
using iText.Layout.Element;
using iText.Kernel.Pdf.Canvas;



namespace e_signature_mvc.Services
{
    public class PdfService : IPdfService
    {
        //public void EmbedSignatureIntoPdf(byte[] signatureBytes)
        //{
        //    string inputPdf = @"C:\Users\engkeng.puah\Downloads\test.pdf";
        //    string outputPdf = @"C:\Users\engkeng.puah\Downloads\test_signed.pdf";

        //    using var reader = new PdfReader(inputPdf);
        //    using var writer = new PdfWriter(outputPdf);
        //    using var pdf = new PdfDocument(reader, writer);

        //    using var document = new Document(pdf);

        //    ImageData imageData = ImageDataFactory.Create(signatureBytes);
        //    Image signatureImage = new Image(imageData);

        //    signatureImage.ScaleToFit(200f, 80f);
        //    signatureImage.SetFixedPosition(100f, 100f);

        //    document.Add(signatureImage);
        //}

        public byte[] EmbedSignatureIntoPdfInMemory(byte[] pdfBytes, byte[] signatureBytes)
        {
            using var ms = new MemoryStream();
            using var reader = new PdfReader(new MemoryStream(pdfBytes));
            using var writer = new PdfWriter(ms);
            using var pdf = new PdfDocument(reader, writer);

            // Target the last page
            int pageNumber = pdf.GetNumberOfPages();
            var page = pdf.GetPage(pageNumber);

            // Prepare image
            ImageData imageData = ImageDataFactory.Create(signatureBytes);

            // Fixed position (x, y) and size
            float x = 50f;
            float y = 100f;
            float width = 200f;
            float height = 80f;

            // Draw the image directly on the page
            var pdfCanvas = new PdfCanvas(page);
            pdfCanvas.AddImageFittedIntoRectangle(imageData, new iText.Kernel.Geom.Rectangle(x, y, width, height), false);

            pdf.Close();
            return ms.ToArray();
        }

    }
}
