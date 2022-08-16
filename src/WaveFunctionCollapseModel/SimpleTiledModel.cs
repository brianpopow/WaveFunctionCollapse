// Copyright (C) 2016 Maxim Gumin, The MIT License (MIT)

using System.Xml.Linq;

public class SimpleTiledModel : WafeFunctionCollapseModel
{
    private readonly List<int[]> tiles;

    private readonly List<string> tileNames;

    private readonly bool blackBackground;

    private readonly int tileSize;

    public SimpleTiledModel(string name, string subsetName, int width, int height, bool periodic, bool blackBackground, WafeFunctionCollapseHeuristic heuristic)
        : base(width, height, 1, periodic, heuristic)
    {
        this.blackBackground = blackBackground;
        XElement xroot = XDocument.Load($"tilesets/{name}.xml").Root;
        bool unique = xroot.Get("unique", false);

        List<string> subset = null;
        if (subsetName != null)
        {
            XElement xsubset = xroot.Element("subsets").Elements("subset").FirstOrDefault(x => x.Get<string>("name") == subsetName);
            if (xsubset == null)
            {
                Console.WriteLine($"ERROR: subset {subsetName} is not found");
            }
            else
            {
                subset = xsubset.Elements("tile").Select(x => x.Get<string>("name")).ToList();
            }
        }

        static int[] Tile(Func<int, int, int> f, int size)
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

        static int[] Rotate(int[] array, int size) => Tile((x, y) => array[size - 1 - y + (x * size)], size);
        static int[] Reflect(int[] array, int size) => Tile((x, y) => array[size - 1 - x + (y * size)], size);

        this.tiles = new List<int[]>();
        this.tileNames = new List<string>();
        var weightList = new List<double>();

        var action = new List<int[]>();
        var firstOccurrence = new Dictionary<string, int>();

        foreach (XElement xtile in xroot.Element("tiles").Elements("tile"))
        {
            string tilename = xtile.Get<string>("name");
            if (subset != null && !subset.Contains(tilename))
            {
                continue;
            }

            Func<int, int> a, b;
            int cardinality;

            char sym = xtile.Get("symmetry", 'X');
            if (sym == 'L')
            {
                cardinality = 4;
                a = i => (i + 1) % 4;
                b = i => i % 2 == 0 ? i + 1 : i - 1;
            }
            else if (sym == 'T')
            {
                cardinality = 4;
                a = i => (i + 1) % 4;
                b = i => i % 2 == 0 ? i : 4 - i;
            }
            else if (sym == 'I')
            {
                cardinality = 2;
                a = i => 1 - i;
                b = i => i;
            }
            else if (sym == '\\')
            {
                cardinality = 2;
                a = i => 1 - i;
                b = i => 1 - i;
            }
            else if (sym == 'F')
            {
                cardinality = 8;
                a = i => i < 4 ? (i + 1) % 4 : 4 + ((i - 1) % 4);
                b = i => i < 4 ? i + 4 : i - 4;
            }
            else
            {
                cardinality = 1;
                a = i => i;
                b = i => i;
            }

            this.T = action.Count;
            firstOccurrence.Add(tilename, this.T);

            int[][] map = new int[cardinality][];
            for (int t = 0; t < cardinality; t++)
            {
                map[t] = new int[8];

                map[t][0] = t;
                map[t][1] = a(t);
                map[t][2] = a(a(t));
                map[t][3] = a(a(a(t)));
                map[t][4] = b(t);
                map[t][5] = b(a(t));
                map[t][6] = b(a(a(t)));
                map[t][7] = b(a(a(a(t))));

                for (int s = 0; s < 8; s++)
                {
                    map[t][s] += this.T;
                }

                action.Add(map[t]);
            }

            if (unique)
            {
                for (int t = 0; t < cardinality; t++)
                {
                    int[] bitmap;
                    (bitmap, this.tileSize, this.tileSize) = BitmapHelper.LoadBitmap($"tilesets/{name}/{tilename} {t}.png");
                    this.tiles.Add(bitmap);
                    this.tileNames.Add($"{tilename} {t}");
                }
            }
            else
            {
                int[] bitmap;
                (bitmap, this.tileSize, this.tileSize) = BitmapHelper.LoadBitmap($"tilesets/{name}/{tilename}.png");
                this.tiles.Add(bitmap);
                this.tileNames.Add($"{tilename} 0");

                for (int t = 1; t < cardinality; t++)
                {
                    if (t <= 3)
                    {
                        this.tiles.Add(Rotate(this.tiles[this.T + t - 1], this.tileSize));
                    }

                    if (t >= 4)
                    {
                        this.tiles.Add(Reflect(this.tiles[this.T + t - 4], this.tileSize));
                    }

                    this.tileNames.Add($"{tilename} {t}");
                }
            }

            for (int t = 0; t < cardinality; t++)
            {
                weightList.Add(xtile.Get("weight", 1.0));
            }
        }

        this.T = action.Count;
        this.weights = weightList.ToArray();

        this.propagator = new int[4][][];
        var densePropagator = new bool[4][][];
        for (int d = 0; d < 4; d++)
        {
            densePropagator[d] = new bool[this.T][];
            this.propagator[d] = new int[this.T][];
            for (int t = 0; t < this.T; t++)
            {
                densePropagator[d][t] = new bool[this.T];
            }
        }

        foreach (XElement xneighbor in xroot.Element("neighbors").Elements("neighbor"))
        {
            string[] left = xneighbor.Get<string>("left").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] right = xneighbor.Get<string>("right").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (subset != null && (!subset.Contains(left[0]) || !subset.Contains(right[0])))
            {
                continue;
            }

            int L = action[firstOccurrence[left[0]]][left.Length == 1 ? 0 : int.Parse(left[1])], D = action[L][1];
            int R = action[firstOccurrence[right[0]]][right.Length == 1 ? 0 : int.Parse(right[1])], U = action[R][1];

            densePropagator[0][R][L] = true;
            densePropagator[0][action[R][6]][action[L][6]] = true;
            densePropagator[0][action[L][4]][action[R][4]] = true;
            densePropagator[0][action[L][2]][action[R][2]] = true;

            densePropagator[1][U][D] = true;
            densePropagator[1][action[D][6]][action[U][6]] = true;
            densePropagator[1][action[U][4]][action[D][4]] = true;
            densePropagator[1][action[D][2]][action[U][2]] = true;
        }

        for (int t2 = 0; t2 < this.T; t2++)
        {
            for (int t1 = 0; t1 < this.T; t1++)
            {
                densePropagator[2][t2][t1] = densePropagator[0][t1][t2];
                densePropagator[3][t2][t1] = densePropagator[1][t1][t2];
            }
        }

        List<int>[][] sparsePropagator = new List<int>[4][];
        for (int d = 0; d < 4; d++)
        {
            sparsePropagator[d] = new List<int>[this.T];
            for (int t = 0; t < this.T; t++)
            {
                sparsePropagator[d][t] = new List<int>();
            }
        }

        for (int d = 0; d < 4; d++)
        {
            for (int t1 = 0; t1 < this.T; t1++)
            {
                List<int> sp = sparsePropagator[d][t1];
                bool[] tp = densePropagator[d][t1];

                for (int t2 = 0; t2 < this.T; t2++)
                {
                    if (tp[t2])
                    {
                        sp.Add(t2);
                    }
                }

                int ST = sp.Count;
                if (ST == 0)
                {
                    Console.WriteLine($"ERROR: tile {this.tileNames[t1]} has no neighbors in direction {d}");
                }

                this.propagator[d][t1] = new int[ST];
                for (int st = 0; st < ST; st++)
                {
                    this.propagator[d][t1][st] = sp[st];
                }
            }
        }
    }

    public override void Save(string filename)
    {
        int[] bitmapData = new int[this.MX * this.MY * this.tileSize * this.tileSize];
        if (this.observed[0] >= 0)
        {
            for (int x = 0; x < this.MX; x++)
            {
                for (int y = 0; y < this.MY; y++)
                {
                    int[] tile = this.tiles[this.observed[x + (y * this.MX)]];
                    for (int dy = 0; dy < this.tileSize; dy++)
                    {
                        for (int dx = 0; dx < this.tileSize; dx++)
                        {
                            bitmapData[(x * this.tileSize) + dx + (((y * this.tileSize) + dy) * this.MX * this.tileSize)] = tile[dx + (dy * this.tileSize)];
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < this.wave.Length; i++)
            {
                int x = i % this.MX, y = i / this.MX;
                if (this.blackBackground && this.sumsOfOnes[i] == this.T)
                {
                    for (int yt = 0; yt < this.tileSize; yt++)
                    {
                        for (int xt = 0; xt < this.tileSize; xt++)
                        {
                            bitmapData[(x * this.tileSize) + xt + (((y * this.tileSize) + yt) * this.MX * this.tileSize)] = 255 << 24;
                        }
                    }
                }
                else
                {
                    bool[] w = this.wave[i];
                    double normalization = 1.0 / this.sumsOfWeights[i];
                    for (int yt = 0; yt < this.tileSize; yt++)
                    {
                        for (int xt = 0; xt < this.tileSize; xt++)
                        {
                            int idi = (x * this.tileSize) + xt + (((y * this.tileSize) + yt) * this.MX * this.tileSize);
                            double r = 0, g = 0, b = 0;
                            for (int t = 0; t < this.T; t++)
                            {
                                if (w[t])
                                {
                                    int argb = this.tiles[t][xt + (yt * this.tileSize)];
                                    r += ((argb & 0xff0000) >> 16) * this.weights[t] * normalization;
                                    g += ((argb & 0xff00) >> 8) * this.weights[t] * normalization;
                                    b += (argb & 0xff) * this.weights[t] * normalization;
                                }
                            }

                            bitmapData[idi] = unchecked((int)0xff000000 | ((int)r << 16) | ((int)g << 8) | (int)b);
                        }
                    }
                }
            }
        }

        BitmapHelper.SaveBitmap(bitmapData, this.MX * this.tileSize, this.MY * this.tileSize, filename);
    }

    public string TextOutput()
    {
        var result = new System.Text.StringBuilder();
        for (int y = 0; y < this.MY; y++)
        {
            for (int x = 0; x < this.MX; x++)
            {
                result.Append($"{this.tileNames[this.observed[x + (y * this.MX)]]}, ");
            }

            result.Append(Environment.NewLine);
        }

        return result.ToString();
    }
}
