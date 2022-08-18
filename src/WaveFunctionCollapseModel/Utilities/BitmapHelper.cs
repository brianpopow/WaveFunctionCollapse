// Copyright (C) 2016 Maxim Gumin, The MIT License (MIT)

using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

/// <summary>
/// Helper methods for loading and saving bitmaps.
/// </summary>
public static class BitmapHelper
{
    public static (int[], int, int) LoadBitmap(string filename)
    {
        using var image = Image.Load<Bgra32>(filename);
        int width = image.Width;
        int height = image.Height;
        int[] result = new int[width * height];
        image.CopyPixelDataTo(MemoryMarshal.Cast<int, Bgra32>(result));
        return (result, width, height);
    }

    public static unsafe void SaveBitmap(int[] data, int width, int height, string filename)
    {
        fixed (int* pData = data)
        {
            using var image = Image.WrapMemory<Bgra32>(pData, width, height);
            image.SaveAsPng(filename);
        }
    }
}
