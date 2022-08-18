using WaveFunctionCollapseModel.Data;

namespace InteractiveTestUI
{
    public partial class InteractiveTestUI : Form
    {
        private static string OutputDirectory = "output";

        private static string FileFilterOverlappingFiles = "PNG|*.png|JPEG|*.jpeg|JPG|*.jpg|BMP|*.bmp|WEBP|*.webp";

        private static string FileFilterTiledFiles = "XML|*.xml";

        private string[] imageFiles;

        private TiledDataConfig tileImagesConfig;

        private string subSet = string.Empty;

        private string tileImagesConfigFile;

        private bool isOverlapping = true;

        private bool isPeriodic = true;

        private bool isPeriodicInput = true;

        private bool isGround = false;

        private int size = 48;

        private int N = 2;

        private WafeFunctionCollapseHeuristic Heuristic = WafeFunctionCollapseHeuristic.Entropy;

        public InteractiveTestUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenOverlappingImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select overlapping image";
            fileDialog.Multiselect = false;
            fileDialog.Filter = FileFilterOverlappingFiles;
            fileDialog.InitialDirectory = @"..\..\..\..\WaveFunctionCollapseExamples\samples";
            DialogResult dialogResult = fileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.isOverlapping = true;
                this.InputImagesPanel.Controls.Clear();

                this.imageFiles = fileDialog.FileNames;
                int x = 20;
                int y = 20;
                var file = this.imageFiles[0];
                PictureBox box = new PictureBox();
                box.Image = Image.FromFile(file);
                box.Location = new Point(x, y);

                this.InputImagesPanel.Controls.Add(box);
            }
        }

        private void OpenTiledSettingFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select tiled configuration file";
            fileDialog.Multiselect = false;
            fileDialog.Filter = FileFilterTiledFiles;
            fileDialog.InitialDirectory = @"..\..\..\..\WaveFunctionCollapseExamples\tilesets";
            DialogResult dialogResult = fileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.isOverlapping = false;
                this.InputImagesPanel.Controls.Clear();
                this.tileImagesConfigFile = fileDialog.FileNames[0];
                this.tileImagesConfig = new TiledDataConfig(this.tileImagesConfigFile);

                if (this.tileImagesConfig.SubSets != null)
                {
                    this.SubsetsComboBox.Enabled = true;
                    this.SubsetsComboBox.Items.Clear();
                    foreach (var subSetsKey in this.tileImagesConfig.SubSets.Keys)
                    {
                        this.SubsetsComboBox.Items.Add(subSetsKey);
                    }
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
            bool hasError = await Task.Run(() => GeneratePicture());
            this.Status.Text = hasError ? "Error" : "Done";
        }

        /// <summary>
        /// Generates the picture from the given input data.
        /// </summary>
        /// <returns>true, if there was an error.</returns>
        private bool GeneratePicture()
        {
            try
            {
                Random random = new();

                string fileNameWithoutExtension = string.Empty;
                WafeFunctionCollapseModel model;
                int screenShots = 2;
                int width = this.size;
                int height = this.size;
                if (this.isOverlapping)
                {
                    if (imageFiles == null)
                    {
                        MessageBox.Show("No input images selected");
                        return false;
                    }

                    string fileName = imageFiles[0];
                    fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    Directory.CreateDirectory(OutputDirectory);
                    int symmetry = 8;
                    model = new OverlappingModel(fileName, this.N, width, height, this.isPeriodicInput, this.isPeriodic, symmetry, this.isGround, this.Heuristic);
                }
                else
                {
                    if (this.tileImagesConfigFile == null)
                    {
                        MessageBox.Show("No tile input image configuration selected");
                        return false;
                    }

                    bool blackBackground = false;
                    fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.tileImagesConfigFile);
                    model = new SimpleTiledModel(this.tileImagesConfig, this.subSet, width, height, this.isPeriodic, blackBackground, this.Heuristic);
                }

                for (int i = 0; i < screenShots; i++)
                {
                    for (int k = 0; k < 20; k++)
                    {
                        Console.Write("> ");
                        int seed = random.Next();
                        bool success = model.Run(seed, -1);
                        if (success)
                        {
                            Console.WriteLine("DONE");
                            var outputFileName = $"{OutputDirectory}{Path.DirectorySeparatorChar}{fileNameWithoutExtension}-{seed}.png";
                            model.Save(outputFileName);

                            var generatedImage = Image.FromFile(outputFileName);
                            if (generatedImage.Width > this.OutputPicture.Width ||
                                generatedImage.Height > this.OutputPicture.Height)
                            {
                                var resizedImage = (Image)(new Bitmap(generatedImage, new Size(this.OutputPicture.Width, this.OutputPicture.Height)));
                                this.OutputPicture.Image = resizedImage;
                            }
                            else
                            {
                                this.OutputPicture.Image = generatedImage;
                            }

                            break;
                        }

                        Console.WriteLine("CONTRADICTION");
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return true;
            }
        }

        private void Periodic_CheckedChanged(object sender, EventArgs e)
        {
            this.isPeriodic = Periodic.Checked;
        }

        private void Ground_CheckedChanged(object sender, EventArgs e)
        {
            this.isGround = Ground.Checked;
        }

        private void PeriodicInput_CheckedChanged(object sender, EventArgs e)
        {
            this.isPeriodicInput = Periodic.Checked;
        }

        private void SizeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.size = SizeTrackBar.Value;
            this.SizeLabel.Text = $"Size: {this.size}";
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

        private void SubsetsCoboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.subSet = this.SubsetsComboBox.SelectedItem.ToString();
        }
    }
}