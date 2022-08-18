// Copyright (C) 2016 Maxim Gumin, The MIT License (MIT)

using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using WaveFunctionCollapseModel.Data;

public static class Program
{
    public static void Main()
    {
        string outputDirectory = "output";
        string samplesDirectory = "samples";
        Stopwatch sw = Stopwatch.StartNew();
        Directory.CreateDirectory(outputDirectory);

        Random random = new();
        XDocument xdoc = XDocument.Load("samples.xml");

        foreach (XElement xelem in xdoc.Root.Elements("overlapping", "simpletiled"))
        {
            WafeFunctionCollapseModel model;
            string fileName = xelem.Get<string>("name");
            Console.WriteLine($"< {fileName}");

            bool isOverlapping = xelem.Name == "overlapping";
            int size = xelem.Get("size", isOverlapping ? 48 : 24);
            int width = xelem.Get("width", size);
            int height = xelem.Get("height", size);
            bool periodic = xelem.Get("periodic", false);
            string heuristicString = xelem.Get<string>("heuristic");
            var heuristic = heuristicString == "Scanline" ? WafeFunctionCollapseHeuristic.Scanline : (heuristicString == "MRV" ? WafeFunctionCollapseHeuristic.MRV : WafeFunctionCollapseHeuristic.Entropy);

            if (isOverlapping)
            {
                int N = xelem.Get("N", 3);
                bool periodicInput = xelem.Get("periodicInput", true);
                int symmetry = xelem.Get("symmetry", 8);
                bool ground = xelem.Get("ground", false);

                fileName = Path.Combine(samplesDirectory, fileName + ".png");
                Directory.CreateDirectory(Path.Combine(outputDirectory, samplesDirectory));
                model = new OverlappingModel(fileName, N, width, height, periodicInput, periodic, symmetry, ground, heuristic);
            }
            else
            {
                string subset = xelem.Get<string>("subset");
                bool blackBackground = xelem.Get("blackBackground", false);
                var tileImagesConfig = new TiledDataConfig(fileName);
                model = new SimpleTiledModel(tileImagesConfig, subset, width, height, periodic, blackBackground, heuristic);
            }

            for (int i = 0; i < xelem.Get("screenshots", 2); i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Console.Write("> ");
                    int seed = random.Next();
                    bool success = model.Run(seed, xelem.Get("limit", -1));
                    if (success)
                    {
                        Console.WriteLine("DONE");
                        model.Save($"{outputDirectory}{Path.DirectorySeparatorChar}{fileName} {seed}.png");
                        if (model is SimpleTiledModel stmodel && xelem.Get("textOutput", false))
                        {
                            File.WriteAllText($"{outputDirectory}{Path.DirectorySeparatorChar}{fileName} {seed}.txt", stmodel.TextOutput());
                        }

                        break;
                    }

                    Console.WriteLine("CONTRADICTION");
                }
            }
        }

        Console.WriteLine($"time = {sw.ElapsedMilliseconds} ms");
    }
}
