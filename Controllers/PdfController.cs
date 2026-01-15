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
        
        //[HttpPost]
        //public IActionResult SignAndSaveIntoDocument(string SignatureData)
        //{
        //    if (string.IsNullOrEmpty(SignatureData))
        //        return BadRequest("No signature received");

        //    // Remove base64 prefix
        //    var base64 = SignatureData.Replace("data:image/png;base64,", "");
        //    byte[] signatureBytes = Convert.FromBase64String(base64);

        //    _service.EmbedSignatureIntoPdf(signatureBytes);

        //    return Ok("PDF signed successfully");
        //}

        [HttpGet]
        public IActionResult ViewPdf()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "sample_files", "test.pdf");
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/pdf");
        }

        [HttpPost]
        public IActionResult Sign(string SignatureData)
        {
            if (string.IsNullOrEmpty(SignatureData))
                return BadRequest("No signature received");

            // Convert base64 signature to bytes
            var base64 = SignatureData.Replace("data:image/png;base64,", "");
            byte[] signatureBytes = Convert.FromBase64String(base64);

            // Read the PDF from the same source as the iframe
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "sample_files/test.pdf");
            byte[] pdfBytes = System.IO.File.ReadAllBytes(filePath);

            // Embed signature in-memory
            byte[] signedPdfBytes = _service.EmbedSignatureIntoPdfInMemory(pdfBytes, signatureBytes);

            // Return signed PDF to browser
            return File(signedPdfBytes, "application/pdf");
        }

    }
}
