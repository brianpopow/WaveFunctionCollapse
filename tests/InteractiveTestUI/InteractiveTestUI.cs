namespace InteractiveTestUI
{
    public partial class InteractiveTestUI : Form
    {
        private string[] imageFiles;

        private static string OutputDirectory = "output";

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
            fileDialog.Filter = "PNG|*.png|JPEG|*.jpeg|JPG|*.jpg";
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

            Random random = new();

            string fileName = imageFiles[0];
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            Directory.CreateDirectory(OutputDirectory);

            bool isOverlapping = true;
            int size = isOverlapping ? 48 : 24;
            int width = size;
            int height = size;
            int N = 3;
            bool periodicInput = true;
            bool periodic = false;
            int symmetry = 8;
            bool ground = false;
            int screenShots = 2;
            string heuristicString = string.Empty;
            var heuristic = heuristicString == "Scanline" ? WafeFunctionCollapseHeuristic.Scanline : (heuristicString == "MRV" ? WafeFunctionCollapseHeuristic.MRV : WafeFunctionCollapseHeuristic.Entropy);

            WafeFunctionCollapseModel model = new OverlappingModel(fileName, N, width, height, periodicInput, periodic, symmetry, ground, heuristic);
            
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

            this.GenerateButton.Enabled = true;
        }
    }
}