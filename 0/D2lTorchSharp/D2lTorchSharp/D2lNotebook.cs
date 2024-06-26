﻿using Microsoft.DotNet.Interactive.Formatting;
using ScottPlot;
using SkiaSharp;

namespace Yueyinqiu.Su.D2lTorchSharp;

partial class D2l
{
    public static class Notebook
    {
        public static void PrepareAll()
        {
            RegisterScottPlotFormatter();
        }

        public static void RegisterScottPlotFormatter(int width = 600, int height = 400)
        {
            Formatter.Register<Plot>(
                (plot, writer) => writer.Write(plot.GetSvgXml(width, height)),
                "text/html");
        }
    }
}
