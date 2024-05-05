using ScottPlot;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using System.Reactive.Joins;
using static TorchSharp.torch;

namespace Yueyinqiu.Su.D2lTorchSharp;

public static class PlottableAdderExtensions
{
    private static ScatterSourceDoubleArray GetScatterSource(Tensor? xs, Tensor ys)
    {
        if (ys.Dimensions is not 1)
            throw new ArgumentException(
                $"{nameof(ys)}.{nameof(ys.Dimensions)} should be equals to one.",
                nameof(ys));

        if (xs is not null && xs.Dimensions is not 1)
            throw new ArgumentException(
                $"{nameof(xs)}.{nameof(xs.Dimensions)} should be equals to one.",
                nameof(xs));

        using (NewDisposeScope())
        {
            ys = ys.detach().cpu().@double();
            if (xs is null)
                xs = arange(0, ys.size(0), ScalarType.Float64, device("cpu"), false);
            else
                xs = xs.detach().cpu().@double();

            var xArray = xs.data<double>().ToArray();
            var yArray = ys.data<double>().ToArray();

            return new ScatterSourceDoubleArray(xArray, yArray);
        }
    }

    public static Scatter ScatterPoints(this PlottableAdder adder,
        Tensor? xs, Tensor ys,
        string legend = "",
        MarkerShape shape = MarkerShape.FilledCircle,
        float size = 5,
        Color? color = null)
    {
        var source = GetScatterSource(xs, ys);
        var result = adder.ScatterPoints(source, color);
        result.LegendText = legend;
        result.MarkerSize = size;
        result.MarkerShape = shape;
        return result;
    }

    public static Scatter ScatterLine(this PlottableAdder adder,
        Tensor? xs, Tensor ys,
        string legend = "",
        LinePattern pattern = LinePattern.Solid,
        Color? color = null)
    {
        var source = GetScatterSource(xs, ys);
        var result = adder.ScatterLine(source, color);
        result.LegendText = legend;
        result.LinePattern = pattern;
        return result;
    }

    public static Heatmap Heatmap(this PlottableAdder adder, 
        Tensor intensities)
    {
        if (intensities.shape.Length is not 2)
            throw new ArgumentException(
                $"{nameof(intensities)}.{nameof(intensities.shape)}.{nameof(intensities.shape.Length)} " +
                $"should be equals to two.", nameof(intensities));

        using (NewDisposeScope())
        {
            intensities = intensities.detach().cpu().@double();
            var array = intensities.data<double>().ToNDArray();
            return adder.Heatmap((double[,])array);
        }
    }
}
