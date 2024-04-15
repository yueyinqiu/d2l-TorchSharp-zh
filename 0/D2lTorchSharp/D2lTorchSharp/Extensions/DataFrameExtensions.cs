﻿using Microsoft.Data.Analysis;
using TorchSharp;

namespace Yueyinqiu.Su.D2lTorchSharp.Extensions;
public static class DataFrameExtensions
{
    public static torch.Tensor ToTorch(
        this DataFrame dataFrame,
        torch.ScalarType? dtype = null,
        torch.Device? device = null,
        bool requires_grad = false)
    {
        using (torch.NewDisposeScope())
        {
            long rowCount = dataFrame.Rows.Count;
            long columnCount = dataFrame.Columns.Count;

            var cpu = torch.device(DeviceType.CPU);

            var result = torch.empty(
                size: rowCount * columnCount,
                dtype: dtype,
                device: cpu,
                requires_grad: false);

            dtype = result.dtype;
            device = torch.InitializeDevice(device);

            long i = 0;
            foreach (var row in dataFrame.Rows)
            {
                foreach (dynamic value in row)
                {
                    result[i] = torch.tensor(value, cpu);
                    i++;
                }
            }

            result = result.view([rowCount, columnCount]);
            result = result.to(dtype.Value, device);
            result = result.with_requires_grad(requires_grad);

            return result.MoveToOuterDisposeScope();
        }
    }
}