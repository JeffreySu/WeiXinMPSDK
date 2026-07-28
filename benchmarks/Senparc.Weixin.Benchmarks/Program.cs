/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
  
    文件名：Program.cs
    文件功能描述：Senparc.Weixin 性能基准与性能门禁入口
    
    
    创建标识：Senparc - 20260728
    
    修改标识：Senparc - 20260729
    修改描述：v1.0.0 新增脱敏与并发注册性能基准及门禁

----------------------------------------------------------------*/

using System.Diagnostics;
using System.Globalization;
using Senparc.Weixin;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Benchmarks;

internal static class Program
{
    private const double MaximumRedactionNanosecondsPerOperation = 1_000_000;
    private const double MaximumRedactionBytesPerOperation = 32_768;
    private const double MaximumRegistrationNanosecondsPerOperation = 5_000_000;
    private const double MaximumRegistrationBytesPerOperation = 4_096;

    private static int Main(string[] args)
    {
        var stress = args.Contains("--stress", StringComparer.OrdinalIgnoreCase);
        var gate = args.Contains("--gate", StringComparer.OrdinalIgnoreCase);
        var redactionIterations = stress ? 250_000 : 50_000;
        var registrationIterations = stress ? 100_000 : 20_000;

        WarmUp();
        var redaction = MeasureRedaction(redactionIterations);
        var registration = MeasureConcurrentRegistration(registrationIterations);

        Print("trace-redaction", redaction);
        Print("concurrent-registration", registration);

        if (!gate)
        {
            return 0;
        }

        var passed = redaction.NanosecondsPerOperation <= MaximumRedactionNanosecondsPerOperation &&
                     redaction.BytesPerOperation <= MaximumRedactionBytesPerOperation &&
                     registration.NanosecondsPerOperation <= MaximumRegistrationNanosecondsPerOperation &&
                     registration.BytesPerOperation <= MaximumRegistrationBytesPerOperation &&
                     registration.CompletedOperations == registrationIterations;

        Console.WriteLine(passed ? "PERFORMANCE_GATE_OK" : "PERFORMANCE_GATE_FAILED");
        return passed ? 0 : 1;
    }

    private static void WarmUp()
    {
        for (var index = 0; index < 1_000; index++)
        {
            _ = WeixinTraceRedactor.RedactAndTruncate(CreateTraceInput(index), 512);
        }

        var registrations = new BaseContainerRegisterFuncCollection<BenchmarkBag>
        {
            MaximumCount = 1_000
        };
        Parallel.For(0, 1_000, index =>
            registrations[$"warmup-{index}"] = static () => Task.FromResult(new BenchmarkBag()));
    }

    private static BenchmarkResult MeasureRedaction(int iterations)
    {
        ForceCollection();
        var allocatedBefore = GC.GetAllocatedBytesForCurrentThread();
        var stopwatch = Stopwatch.StartNew();
        var completed = 0;

        for (var index = 0; index < iterations; index++)
        {
            var result = WeixinTraceRedactor.RedactAndTruncate(CreateTraceInput(index), 512);
            completed += result.Length > 0 ? 1 : 0;
        }

        stopwatch.Stop();
        var allocated = GC.GetAllocatedBytesForCurrentThread() - allocatedBefore;
        return CreateResult(stopwatch, allocated, iterations, completed);
    }

    private static BenchmarkResult MeasureConcurrentRegistration(int iterations)
    {
        ForceCollection();
        var allocatedBefore = GC.GetTotalAllocatedBytes(true);
        var stopwatch = Stopwatch.StartNew();
        var registrations = new BaseContainerRegisterFuncCollection<BenchmarkBag>
        {
            MaximumCount = iterations
        };

        Parallel.For(0, iterations, index =>
            registrations[$"app-{index}"] = static () => Task.FromResult(new BenchmarkBag()));

        stopwatch.Stop();
        var allocated = GC.GetTotalAllocatedBytes(true) - allocatedBefore;
        return CreateResult(stopwatch, allocated, iterations, registrations.Count);
    }

    private static BenchmarkResult CreateResult(
        Stopwatch stopwatch,
        long allocatedBytes,
        int iterations,
        int completedOperations)
    {
        return new BenchmarkResult(
            stopwatch.Elapsed.TotalMilliseconds * 1_000_000 / iterations,
            (double)allocatedBytes / iterations,
            completedOperations);
    }

    private static string CreateTraceInput(int index)
    {
        return $"https://api.weixin.qq.com/path?access_token=token-{index}&openid=user-{index} " +
               $"{{\"appsecret\":\"secret-{index}\",\"mobile\":\"13800138000\",\"safe\":\"ok\"}}";
    }

    private static void ForceCollection()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    private static void Print(string name, BenchmarkResult result)
    {
        Console.WriteLine(string.Format(
            CultureInfo.InvariantCulture,
            "{0}: {1:N0} ops, {2:N1} ns/op, {3:N1} B/op",
            name,
            result.CompletedOperations,
            result.NanosecondsPerOperation,
            result.BytesPerOperation));
    }

    private sealed class BenchmarkBag : BaseContainerBag
    {
    }

    private readonly record struct BenchmarkResult(
        double NanosecondsPerOperation,
        double BytesPerOperation,
        int CompletedOperations);
}
