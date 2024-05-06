using ScottPlot;
using ScottPlot.DataSources;
using ScottPlot.Plottables;
using System.Reactive.Joins;
using static TorchSharp.torch;

namespace Yueyinqiu.Su.D2lTorchSharp;

public static class EnumerableExtensions
{
    public static string ToArrayString<T>(this IEnumerable<T> values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}
