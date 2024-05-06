using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;
using static TorchSharp.torch.utils.data;

namespace Yueyinqiu.Su.D2lTorchSharp;
public sealed class FashionMnistDataLoader :
    DataLoader<Dictionary<string, Tensor>, (Tensor data, Tensor label)>
{
    internal FashionMnistDataLoader(
        Dataset dataset, int batchSize, bool shuffle, int numWorker) :
        base(dataset, batchSize, Collate, shuffle: shuffle, num_worker: numWorker)
    {
    }

    private static (Tensor data, Tensor label) Collate(
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
}
