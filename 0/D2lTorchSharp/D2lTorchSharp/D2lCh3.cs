using static TorchSharp.torch;

namespace Yueyinqiu.Su.D2lTorchSharp;

partial class D2l
{
    public static class Ch3
    {
        public static (Tensor X, Tensor y) synthetic_data(Tensor w, Tensor b, int num_examples)
        {
            var X = normal(0, 1, [num_examples, w.size(0)]);
            var y = matmul(X, w) + b;
            y += normal(0, 0.01, y.shape);
            return (X, y.reshape([-1, 1]));
        }
    }
}

