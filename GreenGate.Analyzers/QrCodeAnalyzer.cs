using System;
using System.Drawing;
using GreenGate.Analyzers.Results;
using ZXing;

namespace GreenGate.Analyzers
{
    public class QrCodeAnalyzer : ImageAnalyzer<QrCodeResult>
    {
        public event Action<QrCodeResult> OnQrCodeReveived;

        public override void Subscribe(Bitmap capturedImage)
        {
            var codeReader = new BarcodeReader();
            var decodeResult = codeReader.Decode(capturedImage);

            if (decodeResult?.ToString() is string capturedQrCode)
            {
                OnQrCodeReveived(new QrCodeResult(capturedQrCode.Trim())
                {
                    Image = capturedImage
                });
            }
        }
    }
}