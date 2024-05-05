using ScottPlot;
using SkiaSharp;

namespace Yueyinqiu.Su.D2lTorchSharp;

public static class EnumerableOfPlotExtensions
{
    public static SKBitmap ToSKBitmap(this IEnumerable<Plot> plots,
        int columnCount, int plotWidth, int plotHeight)
    {
        var plotsArray = plots.ToArray();

        var height = (plotsArray.Length + columnCount - 1) / columnCount * plotHeight;
        var width = plotWidth * columnCount;

        var bitmap = new SKBitmap(width, height);
        using var canvas = new SKCanvas(bitmap);

        var left = 0;
        var top = 0;
        foreach (var plot in plots)
        {
            using SKSurface surface = SKSurface.Create(new SKImageInfo(plotWidth, plotHeight));
            plot.Render(surface.Canvas, plotWidth, plotHeight);
            canvas.DrawSurface(surface, left, top);

            left += plotWidth;
            if (left == width)
            {
                left = 0;
                top += plotHeight;
            }
        }
        return bitmap;
    }
}
