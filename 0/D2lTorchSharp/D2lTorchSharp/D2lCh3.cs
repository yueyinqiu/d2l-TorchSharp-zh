using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;

namespace Yueyinqiu.Su.D2lTorchSharp;

partial class D2l
{
    public static class Ch3
    {
        public static (Tensor X, Tensor y) synthetic_data(Tensor w, Tensor b, int num_examples)
        {
            using (NewDisposeScope())
            {
                var X = normal(0, 1, [num_examples, w.size(0)]);
                var y = matmul(X, w) + b;
                y += normal(0, 0.01, y.shape);
                return (X.MoveToOuterDisposeScope(),
                    y.reshape([-1, 1]).MoveToOuterDisposeScope());
            }
        }

        public static (DataLoader<Dictionary<string, Tensor>, (Tensor data, Tensor label)> trainIter,
            DataLoader<Dictionary<string, Tensor>, (Tensor data, Tensor label)> testIter)
            load_data_fashion_mnist(int batch_size, torchvision.ITransform transform)
        {
            static (Tensor data, Tensor label) Collate(
                IEnumerable<Dictionary<string, Tensor>> d, Device device)
            {
                using (NewDisposeScope())
                {
                    var data = cat(d.Select((item) => item["data"]).ToArray());
                    var label = cat(d.Select((item) => item["label"].unsqueeze(0)).ToArray());
                    return (data.to(device).MoveToOuterDisposeScope(),
                        label.to(device).MoveToOuterDisposeScope());
                }
            }

            var train = torchvision.datasets.FashionMNIST(
                "../gitignored/data", train: true, download: true, transform);
            var test = torchvision.datasets.FashionMNIST(
                "../gitignored/data", train: false, download: true, transform);
            return (new(train, batch_size, Collate, shuffle: true, num_worker: 4), 
                new(train, batch_size, Collate, shuffle: false, num_worker: 4));
        }

        public static (DataLoader<Dictionary<string, Tensor>, (Tensor data, Tensor label)> trainIter,
            DataLoader<Dictionary<string, Tensor>, (Tensor data, Tensor label)> testIter)
            load_data_fashion_mnist(int batch_size, (int height, int width) resize)
        {
            var transform = torchvision.transforms.Resize(resize.height, resize.width);
            return load_data_fashion_mnist(batch_size, transform);
        }

        public static (DataLoader<Dictionary<string, Tensor>, (Tensor data, Tensor label)> trainIter,
            DataLoader<Dictionary<string, Tensor>, (Tensor data, Tensor label)> testIter)
            load_data_fashion_mnist(int batch_size, int resize)
        {
            var transform = torchvision.transforms.Resize(resize);
            return load_data_fashion_mnist(batch_size, transform);
        }
    }
}

