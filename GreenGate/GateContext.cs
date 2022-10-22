using System;
using System.Drawing;

namespace GreenGate
{
    internal class GateContext : ICloneable
    {
        private int _scanCount = 0;
        private bool _firstDose = false;
        private bool _secondDose = false;

        public GateContext(string qrCode)
        {
            QrCode = qrCode;
        }

        public Bitmap Image { get; private set; }
        public string QrCode { get; private set; } = string.Empty;
        public bool HasFirstDose
        {
            get => _firstDose;
            set
            {
                _firstDose = value;
                Completed = true;
            }
        }
        public bool HasSecondDose
        {
            get => _secondDose;
            set
            {
                _secondDose = value;
                Completed = true;
            }
        }
        public bool HasQrCode => !string.IsNullOrWhiteSpace(QrCode);
        public bool HasName { get; set; }
        public bool Completed { get; private set; }
        public void UpdateScanCount() => _scanCount++;

        public bool ScanCountReached => _scanCount > 5;
        public bool IsGood => Completed && HasSecondDose;
        public int GoodQrCodeCounter { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
