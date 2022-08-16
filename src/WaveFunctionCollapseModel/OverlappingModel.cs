// Copyright (C) 2016 Maxim Gumin, The MIT License (MIT)

public class OverlappingModel : WafeFunctionCollapseModel
{
    private readonly List<int> colors;
    private readonly List<byte[]> patterns;

    public OverlappingModel(string fileName, int N, int width, int height, bool periodicInput, bool periodic, int symmetry, bool ground, WafeFunctionCollapseHeuristic heuristic)
        : base(width, height, N, periodic, heuristic)
    {
        var (bitmap, sx, sy) = BitmapHelper.LoadBitmap(fileName);
        byte[] sample = new byte[bitmap.Length];
        this.colors = new List<int>();
        for (int i = 0; i < sample.Length; i++)
        {
            int color = bitmap[i];
            int k = 0;
            for (; k < this.colors.Count; k++)
            {
                if (this.colors[k] == color)
                {
                    break;
                }
            }

            if (k == this.colors.Count)
            {
                this.colors.Add(color);
            }

            sample[i] = (byte)k;
        }

        this.patterns = new();
        Dictionary<long, int> patternIndices = new();
        List<double> weightList = new();

        int C = this.colors.Count;
        int xmax = periodicInput ? sx : sx - N + 1;
        int ymax = periodicInput ? sy : sy - N + 1;
        for (int y = 0; y < ymax; y++)
        {
            for (int x = 0; x < xmax; x++)
            {
                byte[][] ps = new byte[8][];

                ps[0] = Pattern((dx, dy) => sample[((x + dx) % sx) + (((y + dy) % sy) * sx)], N);
                ps[1] = Reflect(ps[0], N);
                ps[2] = Rotate(ps[0], N);
                ps[3] = Reflect(ps[2], N);
                ps[4] = Rotate(ps[2], N);
                ps[5] = Reflect(ps[4], N);
                ps[6] = Rotate(ps[4], N);
                ps[7] = Reflect(ps[6], N);

                for (int k = 0; k < symmetry; k++)
                {
                    byte[] p = ps[k];
                    long h = Hash(p, C);
                    if (patternIndices.TryGetValue(h, out int index))
                    {
                        weightList[index] = weightList[index] + 1;
                    }
                    else
                    {
                        patternIndices.Add(h, weightList.Count);
                        weightList.Add(1.0);
                        this.patterns.Add(p);
                    }
                }
            }
        }

        this.weights = weightList.ToArray();
        this.T = this.weights.Length;
        this.ground = ground;

        this.propagator = new int[4][][];
        for (int d = 0; d < 4; d++)
        {
            this.propagator[d] = new int[this.T][];
            for (int t = 0; t < this.T; t++)
            {
                List<int> list = new();
                for (int t2 = 0; t2 < this.T; t2++)
                {
                    if (Agrees(this.patterns[t], this.patterns[t2], dx[d], dy[d], N))
                    {
                        list.Add(t2);
                    }
                }

                this.propagator[d][t] = new int[list.Count];
                for (int c = 0; c < list.Count; c++)
                {
                    this.propagator[d][t][c] = list[c];
                }
            }
        }
    }

    public override void Save(string filename)
    {
        int[] bitmap = new int[this.MX * this.MY];
        if (this.observed[0] >= 0)
        {
            for (int y = 0; y < this.MY; y++)
            {
                int dy = y < this.MY - this.N + 1 ? 0 : this.N - 1;
                for (int x = 0; x < this.MX; x++)
                {
                    int dx = x < this.MX - this.N + 1 ? 0 : this.N - 1;
                    bitmap[x + (y * this.MX)] = this.colors[this.patterns[this.observed[x - dx + ((y - dy) * this.MX)]][dx + (dy * this.N)]];
                }
            }
        }
        else
        {
            for (int i = 0; i < this.wave.Length; i++)
            {
                int contributors = 0, r = 0, g = 0, b = 0;
                int x = i % this.MX, y = i / this.MX;
                for (int dy = 0; dy < this.N; dy++)
                {
                    for (int dx = 0; dx < this.N; dx++)
                    {
                        int sx = x - dx;
                        if (sx < 0)
                        {
                            sx += this.MX;
                        }

                        int sy = y - dy;
                        if (sy < 0)
                        {
                            sy += this.MY;
                        }

                        int s = sx + (sy * this.MX);
                        if (!this.periodic && (sx + this.N > this.MX || sy + this.N > this.MY || sx < 0 || sy < 0))
                        {
                            continue;
                        }

                        for (int t = 0; t < this.T; t++)
                        {
                            if (this.wave[s][t])
                            {
                                contributors++;
                                int argb = this.colors[this.patterns[t][dx + (dy * this.N)]];
                                r += (argb & 0xff0000) >> 16;
                                g += (argb & 0xff00) >> 8;
                                b += argb & 0xff;
                            }
                        }
                    }
                }

                bitmap[i] = unchecked((int)0xff000000 | ((r / contributors) << 16) | ((g / contributors) << 8) | (b / contributors));
            }
        }

        BitmapHelper.SaveBitmap(bitmap, this.MX, this.MY, filename);
    }

    private static byte[] Pattern(Func<int, int, byte> f, int N)
    {
        byte[] result = new byte[N * N];
        for (int y = 0; y < N; y++)
        {
            for (int x = 0; x < N; x++)
            {
                result[x + (y * N)] = f(x, y);
            }
        }

        return result;
    }

    private static byte[] Rotate(byte[] p, int N) => Pattern((x, y) => p[N - 1 - y + (x * N)], N);

    private static byte[] Reflect(byte[] p, int N) => Pattern((x, y) => p[N - 1 - x + (y * N)], N);

    private static bool Agrees(byte[] p1, byte[] p2, int dx, int dy, int N)
    {
        int xmin = dx < 0 ? 0 : dx, xmax = dx < 0 ? dx + N : N, ymin = dy < 0 ? 0 : dy, ymax = dy < 0 ? dy + N : N;
        for (int y = ymin; y < ymax; y++)
        {
            for (int x = xmin; x < xmax; x++)
            {
                if (p1[x + (N * y)] != p2[x - dx + (N * (y - dy))])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static long Hash(byte[] p, int C)
    {
        long result = 0, power = 1;
        for (int i = 0; i < p.Length; i++)
        {
            result += p[p.Length - 1 - i] * power;
            power *= C;
        }

        return result;
    }
}
