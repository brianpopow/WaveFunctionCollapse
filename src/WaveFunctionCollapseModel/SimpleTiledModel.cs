// Copyright (C) 2016 Maxim Gumin, The MIT License (MIT)

using WaveFunctionCollapseModel.Data;

public class SimpleTiledModel : WafeFunctionCollapseModel
{
    private readonly List<int[]> tiles;

    private readonly List<string> tileNames;

    private readonly bool blackBackground;

    private readonly int tileSize;

    public SimpleTiledModel(string fileNameConfig, string subsetName, int width, int height, bool periodic, bool blackBackground, WafeFunctionCollapseHeuristic heuristic)
        : base(width, height, 1, periodic, heuristic)
    {
        this.blackBackground = blackBackground;

        TiledDataConfig config = new TiledDataConfig(fileNameConfig, subsetName);
        bool unique = config.Unique;

        this.tiles = new List<int[]>();
        var action = config.Action;
        var firstOccurrence = config.FirstOccurrence;
        foreach (var tile in config.Tiles)
        {
            string tileName = tile.Name;
            if (config.SubsetNames != null && !config.SubsetNames.Contains(tileName))
            {
                continue;
            }

            this.T = action.Count;

            if (unique)
            {
                for (int t = 0; t < tile.Cardinality; t++)
                {
                    int[] bitmap;
                    (bitmap, this.tileSize, this.tileSize) = BitmapHelper.LoadBitmap(tile.Images[t]);
                    this.tiles.Add(bitmap);
                }
            }
            else
            {
                int[] bitmap;
                (bitmap, this.tileSize, this.tileSize) = BitmapHelper.LoadBitmap(tile.Images[0]);
                this.tiles.Add(bitmap);
            }
        }

        this.T = action.Count;
        this.weights = config.Weights.ToArray();

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

        for (var index = 0; index < config.Neighbors.Count; index++)
        {
            var neighbor = config.Neighbors[index];
            string[] left = neighbor.Left;
            string[] right = neighbor.Right;

            if (config.SubsetNames != null &&
                (!config.SubsetNames.Contains(left[0]) || !config.SubsetNames.Contains(right[0])))
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
