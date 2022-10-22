namespace GreenGate.Analyzers.Results
{
    public class DoseCountResult : BaseAnalyzerResult
    {
        public DoseCountResult(byte doseCount, string text = null)
        {
            DoseCount = doseCount;
            Text = text;
            IsSuccess = doseCount > 0;
        }

        public byte DoseCount { get; private set; }
        public string Text { get; private set; }
    }
}
