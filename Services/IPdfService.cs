namespace e_signature_mvc.Services
{
    public interface IPdfService
    {
        void EmbedSignatureIntoPdf(byte[] signatureBytes);
    }
}
