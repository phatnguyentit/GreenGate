using System.Drawing;

namespace GreenGate.Analyzers.Results
{
    public class QrCodeResult : BaseAnalyzerResult
    {
        public QrCodeResult(string qrCode = null)
        {
            QrCode = qrCode;
            IsSuccess = !string.IsNullOrWhiteSpace(qrCode) && qrCode.Length > 20;
        }

        public string QrCode { get; private set; }
        public Bitmap Image { get; set; }
    }
}