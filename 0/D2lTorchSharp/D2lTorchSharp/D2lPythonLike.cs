using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Interactive.Formatting;
using System.Numerics;

namespace Yueyinqiu.Su.D2lTorchSharp;

partial class D2l
{
    public static class PythonLike
    {
        public static IEnumerable<T> Range<T>(T start, T stop, T step) where T : INumber<T>
        {
            for (; T.Sign(stop - start) == T.Sign(step); start += step)
                yield return start;
        }

        public static IEnumerable<T> Range<T>(T stop) where T : INumber<T>
        {
            return Range(T.Zero, stop, T.One);
        }

        public static IEnumerable<T> Range<T>(T start, T stop) where T : INumber<T>
        {
            return Range(start, stop, T.One);
        }
    }
}
