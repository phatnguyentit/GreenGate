namespace GreenGate
{
    public class PipelineCompletedResult
    {
        public PipelineCompletedResult()
        {
            Expired = false;
        }

        public PipelineCompletedResult(bool isExpired)
        {
            Expired = isExpired;
        }

        public bool Expired { get; private set; }
        public bool Completed { get; set; }
        public bool HasSecondDose { get; set; }
        public bool OnlyFisrtDose { get; set; }
        public string QrCode { get; set; }
        public bool AlreadyScanned { get; set; }
    }
}
