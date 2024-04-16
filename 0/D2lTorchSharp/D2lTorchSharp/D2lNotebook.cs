using Microsoft.DotNet.Interactive.Formatting;

namespace Yueyinqiu.Su.D2lTorchSharp;

partial class D2l
{
    public static class Notebook
    {
        public static void RegisterScottPlotFormatter(int width = 300, int height = 200)
        {
            Formatter.Register<ScottPlot.Plot>(
                (plot, writer) => writer.Write(plot.GetImageHtml(width, height)),
                HtmlFormatter.MimeType);
        }
    }
}
