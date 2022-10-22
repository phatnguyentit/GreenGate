using GreenGate.Analyzers.Results;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GreenGate.Analyzers
{
    public class DoseCountAnalyzer : ImageAnalyzer<DoseCountResult>
    {
        public event Action<DoseCountResult> OnDoseCountReveived;
        public event Action<string> OnTextReceived;

        private readonly TextAnalyzer _textAnalyzer;

        public DoseCountAnalyzer()
        {
            _textAnalyzer = new TextAnalyzer();
            _textAnalyzer.OnEnrichCompleted += OnTextEnrichCompleted;
        }

        public override void Subscribe(Bitmap input)
        {
            _textAnalyzer.Subscribe(input);
        }

        public void SubscribePatch(List<Bitmap> inputs)
        {
            _textAnalyzer.SubscribePatch(inputs);
        }

        private void OnTextEnrichCompleted(TextResult textResult)
        {
            //OnTextReceived.Invoke(textResult.Text);
            if (textResult.IsSuccess)
            {
                if (textResult.Text.Contains(AnalyzerResource.FirstDose))
                {
                    OnDoseCountReveived(new DoseCountResult(1, textResult.Text));
                }
                else if (textResult.Text.Contains(AnalyzerResource.SecondDose))
                {
                    OnDoseCountReveived(new DoseCountResult(2, textResult.Text));
                }
                else
                {
                    OnDoseCountReveived(new DoseCountResult(0, textResult.Text));
                }
            }
        }
    }
}
