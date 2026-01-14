using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Image;
using iText.Layout.Element;



namespace e_signature_mvc.Services
{
    public class PdfService : IPdfService
    {
        public void EmbedSignatureIntoPdf(byte[] signatureBytes)
        {
            string inputPdf = @"C:\Users\engkeng.puah\Downloads\test.pdf";
            string outputPdf = @"C:\Users\engkeng.puah\Downloads\test_signed.pdf";

            using var reader = new PdfReader(inputPdf);
            using var writer = new PdfWriter(outputPdf);
            using var pdf = new PdfDocument(reader, writer);

            using var document = new Document(pdf);

            ImageData imageData = ImageDataFactory.Create(signatureBytes);
            Image signatureImage = new Image(imageData);

            signatureImage.ScaleToFit(200f, 80f);
            signatureImage.SetFixedPosition(100f, 100f);

            document.Add(signatureImage);
        }
    }
}
