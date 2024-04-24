using ScottPlot;
using ScottPlot.Plottables;
using static TorchSharp.torch;

namespace Yueyinqiu.Su.D2lTorchSharp;

public static class PlottableAdderExtensions
{
    public static Scatter ScatterLine(this PlottableAdder adder, 
        Tensor xs, Tensor ys,
        string legend = "",
        LinePattern pattern = LinePattern.Solid,
        Color? color = null)
    {
        if (xs.Dimensions is not 1)
            throw new ArgumentException(
                $"{nameof(xs)}.{nameof(xs.Dimensions)} should be equals to one.",
                nameof(xs));
        if (ys.Dimensions is not 1)
            throw new ArgumentException(
                $"{nameof(ys)}.{nameof(ys.Dimensions)} should be equals to one.",
                nameof(ys));

        using (NewDisposeScope())
        {
            xs = xs.detach().cpu().@double();
            ys = ys.detach().cpu().@double();

            var xArray = xs.data<double>().ToArray();
            var yArray = ys.data<double>().ToArray();
            
            var result = adder.ScatterLine(xArray, yArray, color);
            result.LegendText = legend;
            result.LinePattern = pattern;
            return result;
        }
    }
}
