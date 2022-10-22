using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.ComponentModel;
using AForge.Vision.Motion;
using GreenGate.Analyzers;

namespace GreenGate
{
    public partial class MainForm : Form
    {
        // Event
        private readonly GateContextPipeline _gateContextPipeline;
        private readonly QrCodeAnalyzer _codeAnalyzer;
        private readonly SoundTrigger _soundTrigger;
        private VideoCaptureDevice _videoCaptureDevice;
        private FilterInfoCollection _filterInfoCollection;
        private static int count1C = 0;
        private static int count2C = 0;
        private const float MaxMotionLevel = 0.8f;
        private readonly Timer _timer;

        private readonly MotionDetector _motionDetector = new MotionDetector(new SimpleBackgroundModelingDetector(), new MotionAreaHighlighting());

        public MainForm(License.LicenseManager licenseManager)
        {
            InitializeComponent();
            try
            {
                licenseManager.CheckAppLicense();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            _timer = new Timer
            {
                Interval = 888
            };

            _gateContextPipeline = new GateContextPipeline();
            _codeAnalyzer = new QrCodeAnalyzer();
            _soundTrigger = new SoundTrigger();

            Load += OnFormLoad;
            Closing += OnFormClosing;
            btnStart.Click += StartButtonClick;
            btnRefreshCamList.Click += OnRefreshButtonClick;

            btnStart.Text = AppResource_.StrStart;
            listboxCapture.ScrollAlwaysVisible = true;
            pbCam.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCam.BorderStyle = BorderStyle.FixedSingle;
            pbCam.BackColor = Color.Green;
        }

        private void OnRefreshButtonClick(object sender, EventArgs e)
        {
            StopCapturing();
            LoadCameraList();
        }

        private void OnFormLoad(object sender, EventArgs e) => LoadCameraList();

        private void LoadCameraList()
        {
            cbbCam.Items.Clear();
            _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo device in _filterInfoCollection)
            {
                cbbCam.Items.Add(device.Name);
            }

            cbbCam.SelectedIndex = 0;
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            if (btnStart.Text.Equals(AppResource_.StrStart))
            {
                StartCapturing();
                btnStart.Text = AppResource_.StrStop;
            }
            else
            {
                StopCapturing();
                btnStart.Text = AppResource_.StrStart;
                ChangeStatus(AppResource_.StrStop);
            }
        }

        private void ShowImage(object sender, NewFrameEventArgs eventArgs)
        {
            if (eventArgs?.Frame is Bitmap capturedPic)
            {
                pbCam.Image = (Bitmap)capturedPic.Clone();

                var image = (Bitmap)pbCam.Image.Clone();

                var motionLevel = _motionDetector.ProcessFrame(image);
                if (motionLevel < MaxMotionLevel)
                {
                    _codeAnalyzer.Subscribe((Bitmap)capturedPic.Clone());
                }
                else
                {
                    ChangeStatus(AppResource_.PleaseKeepImageStable, Color.Red);
                }
            }
        }

        private void OnQrCodeReceived(Analyzers.Results.QrCodeResult qrCodeResult)
        {
            if (qrCodeResult.IsSuccess)
            {
                _gateContextPipeline.Push(qrCodeResult.Image, qrCodeResult.QrCode);
                ChangeStatus(AppResource_.QrCodeReceived, Color.YellowGreen);
            }
        }

        private void OnDataRetrieved(string data)
        {
            Invoke((Action)(() =>
            {
                listboxCapture.Items.Add(data);
            }));
        }

        private void StartCapturing()
        {
            _codeAnalyzer.OnQrCodeReveived += OnQrCodeReceived;
            _videoCaptureDevice = new VideoCaptureDevice(_filterInfoCollection[cbbCam.SelectedIndex].MonikerString);
            _videoCaptureDevice.NewFrame += ShowImage;
            _videoCaptureDevice.Start();

            // Start event
            _gateContextPipeline.OnPipelineCompleted += OnPipelineCompleted;
            _timer.Tick += OnTimerTick;
            _timer.Enabled = true;
            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _gateContextPipeline.Next();
        }

        private void OnFormClosing(object sender, CancelEventArgs e) => StopCapturing();

        private void OnPipelineCompleted(PipelineCompletedResult result)
        {
            Invoke((Action)(() =>
            {
                if (result.AlreadyScanned)
                {
                    listboxCapture.Items.Add("QRCODE NÀY ĐÃ ĐƯỢC SCAN");
                }
                else if (result.OnlyFisrtDose)
                {
                    count1C++;
                    lb1CCount.Text = count1C.ToString();
                    lb1CCount.BackColor = Color.Red;
                    lbProcessing1CText.BackColor = Color.Red;
                    listboxCapture.Items.Add("ĐÃ ĐỌC ĐƯỢC THÔNG TIN 1 MŨI");
                    _soundTrigger.Warning();
                }
                else if (result.HasSecondDose)
                {
                    count2C++;
                    lb2CCount.Text = count2C.ToString();
                    lb2CCount.BackColor = Color.Green;
                    lbProcessing2CText.BackColor = Color.Green;
                    listboxCapture.Items.Add("ĐÃ ĐỌC ĐƯỢC THÔNG TIN 2 MŨI");
                    _soundTrigger.Welcome();
                }
                else if (result.Expired)
                {
                    listboxCapture.Items.Add("Đã hết thời gian quét cho mã này");
                }

                listboxCapture.Items.Add("Đang chờ mã tiếp theo");
                listboxCapture.SelectedIndex = listboxCapture.Items.Count - 1;
                ChangeStatus(AppResource_.NextOne);
            }));
        }

        private void StopCapturing()
        {
            if (_videoCaptureDevice?.IsRunning == true)
            {
                try
                {
                    _videoCaptureDevice.NewFrame -= ShowImage;
                    _videoCaptureDevice.SignalToStop();

                    _gateContextPipeline.OnPipelineCompleted -= OnPipelineCompleted;

                    _timer.Tick -= OnTimerTick;
                    _timer.Enabled = false;
                    _timer.Stop();
                    _codeAnalyzer.OnQrCodeReveived -= OnQrCodeReceived;
                }
                catch (Exception e)
                {

                }
            }
        }

        private void ChangeStatus(string status, Color? color = null)
        {
            Invoke((Action)(() =>
            {
                lbCurrentCheck.Text = status;
                lbCurrentCheck.BackColor = color ?? BackColor;
            }));
        }
    }
}