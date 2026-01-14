using e_signature_mvc.Services;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;

namespace e_signature_mvc.Controllers
{
    public class PdfController : Controller
    {
        private IPdfService _service;
        public PdfController(IPdfService IPdfService) 
        {
            _service = IPdfService;
        }

        [HttpPost]
        public IActionResult Sign(string SignatureData)
        {
            if (string.IsNullOrEmpty(SignatureData))
                return BadRequest("No signature received");

            // Remove base64 prefix
            var base64 = SignatureData.Replace("data:image/png;base64,", "");
            byte[] signatureBytes = Convert.FromBase64String(base64);

            _service.EmbedSignatureIntoPdf(signatureBytes);
            //EmbedSignatureIntoPdf(signatureBytes); 

            return Ok("PDF signed successfully");
        }

        //private void EmbedSignatureIntoPdf(byte[] signatureBytes)
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
    }
}
