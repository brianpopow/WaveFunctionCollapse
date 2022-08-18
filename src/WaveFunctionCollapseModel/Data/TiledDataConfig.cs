using System.Xml.Linq;

namespace WaveFunctionCollapseModel.Data
{
    internal class TiledDataConfig
    {
        public TiledDataConfig(string xmlConfigPath, string subsetName)
        {
            this.FirstOccurrence = new Dictionary<string, int>();
            this.Action = new List<int[]>();
            this.Weights = new List<double>();
            this.Tiles = new List<TileData>();
            this.SubsetNames = null;

            var configPath = Path.GetDirectoryName(xmlConfigPath);
            var tileImagesPath = Path.Combine(configPath, Path.GetFileNameWithoutExtension(xmlConfigPath));

            XElement xRoot = XDocument.Load(xmlConfigPath).Root;
            this.Unique = xRoot.Get("unique", false);

            if (!string.IsNullOrEmpty(subsetName))
            {
                XElement xsubset = xRoot.Element("subsets").Elements("subset").FirstOrDefault(x => x.Get<string>("name") == subsetName);
                if (xsubset == null)
                {
                    Console.WriteLine($"ERROR: subset {subsetName} is not found");
                }
                else
                {
                    this.SubsetNames = xsubset.Elements("tile").Select(x => x.Get<string>("name")).ToList();
                }
            }

            var tileNames = new List<string>();
            foreach (XElement xtile in xRoot.Element("tiles").Elements("tile"))
            {
                string tileName = xtile.Get<string>("name");
                TileData tileData = new TileData(tileName);
                tileData.Images = new List<string>();
                Func<int, int> a, b;

                tileData.Symmetry = xtile.Get("symmetry", 'X');
                if (tileData.Symmetry == 'L')
                {
                    tileData.Cardinality = 4;
                    a = i => (i + 1) % 4;
                    b = i => i % 2 == 0 ? i + 1 : i - 1;
                }
                else if (tileData.Symmetry == 'T')
                {
                    tileData.Cardinality = 4;
                    a = i => (i + 1) % 4;
                    b = i => i % 2 == 0 ? i : 4 - i;
                }
                else if (tileData.Symmetry == 'I')
                {
                    tileData.Cardinality = 2;
                    a = i => 1 - i;
                    b = i => i;
                }
                else if (tileData.Symmetry == '\\')
                {
                    tileData.Cardinality = 2;
                    a = i => 1 - i;
                    b = i => 1 - i;
                }
                else if (tileData.Symmetry == 'F')
                {
                    tileData.Cardinality = 8;
                    a = i => i < 4 ? (i + 1) % 4 : 4 + ((i - 1) % 4);
                    b = i => i < 4 ? i + 4 : i - 4;
                }
                else
                {
                    tileData.Cardinality = 1;
                    a = i => i;
                    b = i => i;
                }

                this.FirstOccurrence.Add(tileName, this.Action.Count);

                this.Map = new int[tileData.Cardinality][];
                var actionCount = this.Action.Count;
                for (int t = 0; t < tileData.Cardinality; t++)
                {
                    this.Map[t] = new int[8];

                    this.Map[t][0] = t;
                    this.Map[t][1] = a(t);
                    this.Map[t][2] = a(a(t));
                    this.Map[t][3] = a(a(a(t)));
                    this.Map[t][4] = b(t);
                    this.Map[t][5] = b(a(t));
                    this.Map[t][6] = b(a(a(t)));
                    this.Map[t][7] = b(a(a(a(t))));

                    for (int s = 0; s < 8; s++)
                    {
                        this.Map[t][s] += actionCount;
                    }

                    this.Action.Add(this.Map[t]);
                }

                if (this.Unique)
                {
                    for (int t = 0; t < tileData.Cardinality; t++)
                    {
                        tileData.Images.Add(Path.Combine(tileImagesPath, $"{tileName} {t}.png"));
                        tileNames.Add($"{tileName} {t}");
                    }
                }
                else
                {
                    tileData.Images.Add(Path.Combine(tileImagesPath, $"{tileName}.png"));
                    tileNames.Add($"{tileName} 0");

                    for (int t = 1; t < tileData.Cardinality; t++)
                    {
                        // TODO:
                        /*if (t <= 3)
                        {
                            tileData.Images.Add(Rotate(this.tiles[this.T + t - 1], this.tileSize));
                        }

                        if (t >= 4)
                        {
                            tileData.Images.Add(Reflect(this.tiles[this.T + t - 4], this.tileSize));
                        }

                        tileNames.Add($"{tileName} {t}");*/
                    }
                }

                for (int t = 0; t < tileData.Cardinality; t++)
                {
                    this.Weights.Add(xtile.Get("weight", 1.0));
                }

                this.Tiles.Add(tileData);
            }

            this.Neighbors = new List<NeighborData>();
            foreach (XElement xneighbor in xRoot.Element("neighbors").Elements("neighbor"))
            {
                string[] left = xneighbor.Get<string>("left").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] right = xneighbor.Get<string>("right").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                this.Neighbors.Add(new NeighborData(left, right));
            }
        }

        public bool Unique { get; }

        public List<TileData> Tiles { get; }

        public List<double> Weights { get; }

        public List<NeighborData> Neighbors { get; }

        public List<string> SubsetNames { get; }

        public int[][] Map { get; }

        public List<int[]> Action { get; }

        public Dictionary<string, int> FirstOccurrence { get; }

        private static int[] Tile(Func<int, int, int> f, int size)
        {
            int[] result = new int[size * size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    result[x + (y * size)] = f(x, y);
                }
            }

            return result;
        }

        private static int[] Rotate(int[] array, int size) => Tile((x, y) => array[size - 1 - y + (x * size)], size);

        private static int[] Reflect(int[] array, int size) => Tile((x, y) => array[size - 1 - x + (y * size)], size);
    }
}
