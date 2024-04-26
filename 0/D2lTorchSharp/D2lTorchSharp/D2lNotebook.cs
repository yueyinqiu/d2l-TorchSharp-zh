using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Interactive.Formatting;

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
            Formatter.Register<ScottPlot.Plot>(
                (plot, writer) => writer.Write(plot.GetImageHtml(width, height)),
                HtmlFormatter.MimeType);
        }
    }
}
