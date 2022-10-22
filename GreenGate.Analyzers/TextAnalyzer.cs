using System.Drawing;
using System.Threading.Tasks;
using GreenGate.Analyzers.Results;
using System;
using IronOcr;
using System.Collections.Generic;
using System.Linq;

namespace GreenGate.Analyzers
{
    public class TextAnalyzer : ImageAnalyzer<TextResult>
    {
        public event Action<TextResult> OnEnrichCompleted;

        public float ThresholdValue { get; set; } = 0.60f;

        public TextAnalyzer()
        {
        }

        public override void Subscribe(Bitmap bitmap)
        {
            Task.Run(async () =>
            {
                try
                {
                    //SaveFile(bitmap);
                    var ocr = new IronTesseract
                    {
                        Language = OcrLanguage.VietnameseAlphabetBest
                    };

                    ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5;

                    //AI OCR only without font analysis
                    ocr.Configuration.EngineMode = TesseractEngineMode.LstmOnly;

                    //Turn off unneeded options
                    ocr.Configuration.ReadBarCodes = false;
                    ocr.Configuration.RenderSearchablePdfsAndHocr = false;

                    // Assume text is laid out neatly in an orthagonal document
                    ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.SparseText;

                    using var input = new OcrInput();
                    input.AddImage(bitmap);
                    input.Contrast();
                    input.DeepCleanBackgroundNoise();

                    var result = await ocr.ReadAsync(input);
                    var text = result.Text.MakeTextEasier();
                    OnEnrichCompleted(new TextResult(text));
                }
                catch (Exception e)
                {

                    throw;
                }
            });
        }

        public void SubscribePatch(List<Bitmap> bitmaps)
        {
            Task.Run(() =>
            {
                var ocr = new IronTesseract
                {
                    Language = OcrLanguage.VietnameseAlphabetBest
                };

                ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5;

                //AI OCR only without font analysis
                ocr.Configuration.EngineMode = TesseractEngineMode.LstmOnly;

                //Turn off unneeded options
                ocr.Configuration.ReadBarCodes = false;
                ocr.Configuration.RenderSearchablePdfsAndHocr = false;

                // Assume text is laid out neatly in an orthagonal document
                ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.Auto;

                var ocrInputs = bitmaps.Select(bm =>
                {
                    var input = new OcrInput();

                    input.AddImage(bm);
                    input.Contrast();
                    input.DeepCleanBackgroundNoise();
                    return input;
                });

                var result = ocr.ReadMultithreaded(ocrInputs);
                if (result.Any())
                {
                    foreach (var text in result)
                    {
                        OnEnrichCompleted(new TextResult(text.Text.MakeTextEasier()));
                    }

                }
            });
        }
    }
}