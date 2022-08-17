namespace InteractiveTestUI
{
    public partial class InteractiveTestUI : Form
    {
        private static string OutputDirectory = "output";

        private static string FileFilter = "PNG|*.png|JPEG|*.jpeg|JPG|*.jpg|BMP|*.bmp|WEBP|*.webp";

        private string[] imageFiles;

        private bool IsOverlapping = true;

        private bool IsSimpletiled = false;

        private bool IsPeriodic = true;

        private bool IsPeriodicInput = true;

        private bool IsGround = false;

        private int Size = 48;

        private int N = 2;

        private WafeFunctionCollapseHeuristic Heuristic = WafeFunctionCollapseHeuristic.Entropy;

        public InteractiveTestUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenImagesMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select test images";
            fileDialog.Multiselect = true;
            fileDialog.Filter = FileFilter;
            fileDialog.InitialDirectory = @"..\..\..\..\WaveFunctionCollapseExamples\samples";
            DialogResult dialogResult = fileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.InputImagesPanel.Controls.Clear();

                this.imageFiles = fileDialog.FileNames;
                int x = 20;
                int y = 20;
                int maxHeight = -1;
                foreach (var file in this.imageFiles)
                {
                    PictureBox box = new PictureBox();
                    box.Image = Image.FromFile(file);
                    box.Location = new Point(x, y);
                    x += box.Width + 10;
                    maxHeight = Math.Max(maxHeight, box.Height);
                    if (x > this.ClientSize.Width - 50)
                    {
                        x = 20;
                        y += maxHeight + 10;
                    }

                    this.InputImagesPanel.Controls.Add(box);
                }
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            this.GenerateButton.Enabled = false;

            StartGeneratePictureTask();

            this.GenerateButton.Enabled = true;
        }

        private async Task StartGeneratePictureTask()
        {
            this.Status.Text = "Generating...";
            await Task.Run(() => GeneratePicture());
            this.Status.Text = "Done";
        }

        private void GeneratePicture()
        {
            if (imageFiles == null)
            {
                MessageBox.Show("No input images selected");
                return;
            }

            Random random = new();

            string fileName = imageFiles[0];
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            Directory.CreateDirectory(OutputDirectory);

            int width = this.Size;
            int height = this.Size;
            int symmetry = 8;
            int screenShots = 2;
            WafeFunctionCollapseModel model = new OverlappingModel(fileName, this.N, width, height, this.IsPeriodicInput, this.IsPeriodic,
                symmetry, this.IsGround, this.Heuristic);

            for (int i = 0; i < screenShots; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Console.Write("> ");
                    int seed = random.Next();
                    bool success = model.Run(seed, -1);
                    if (success)
                    {
                        Console.WriteLine("DONE");
                        var outputFileName = $"{OutputDirectory}{Path.DirectorySeparatorChar}{fileNameWithoutExtension}-{seed}.png";
                        model.Save(outputFileName);

                        this.OutputPicture.Image = Image.FromFile(outputFileName);

                        break;
                    }

                    Console.WriteLine("CONTRADICTION");
                }
            }
        }

        private void Overlapping_CheckedChanged(object sender, EventArgs e)
        {
            this.IsOverlapping = Overlapping.Checked;
            this.IsSimpletiled = !this.IsOverlapping;
        }

        private void Simpletiled_CheckedChanged(object sender, EventArgs e)
        {
            this.IsSimpletiled = Simpletiled.Checked;
            this.IsOverlapping = !this.IsSimpletiled;
        }

        private void Periodic_CheckedChanged(object sender, EventArgs e)
        {
            this.IsPeriodic = Periodic.Checked;
        }

        private void Ground_CheckedChanged(object sender, EventArgs e)
        {
            this.IsGround = Ground.Checked;
        }

        private void PeriodicInput_CheckedChanged(object sender, EventArgs e)
        {
            this.IsPeriodicInput = Periodic.Checked;
        }

        private void SizeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.Size = SizeTrackBar.Value;
            this.SizeLabel.Text = $"Size: {this.Size}";
        }

        private void HeuristicComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (HeuristicComboBox.Text)
            {
                case "MRV":
                    this.Heuristic = WafeFunctionCollapseHeuristic.MRV;
                    break;
                case "Scanline":
                    this.Heuristic = WafeFunctionCollapseHeuristic.Scanline;
                    break;
                case "Entropy":
                    this.Heuristic = WafeFunctionCollapseHeuristic.Entropy;
                    break;
                default:
                    this.Heuristic = WafeFunctionCollapseHeuristic.Entropy;
                    break;
            }
        }

        private void NComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (NComboBox.Text)
            {
                case "2":
                    this.N = 2;
                    break;
                case "3":
                    this.N = 3;
                    break;
                default:
                    this.N = 3;
                    break;
            }
        }
    }
}