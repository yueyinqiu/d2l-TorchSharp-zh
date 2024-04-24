using ScottPlot;
using ScottPlot.Plottables;
using TorchSharp;
using static TorchSharp.torch;

namespace Yueyinqiu.Su.D2lTorchSharp;

public static class PlottableAdderExtensions
{
    public static Scatter ScatterLine(this PlottableAdder adder, 
        Tensor? xs, Tensor ys,
        string legend = "",
        LinePattern pattern = LinePattern.Solid,
        Color? color = null)
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
            
            var result = adder.ScatterLine(xArray, yArray, color);
            result.LegendText = legend;
            result.LinePattern = pattern;
            return result;
        }
    }
}
