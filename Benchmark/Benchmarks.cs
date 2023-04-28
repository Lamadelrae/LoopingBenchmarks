using Benchmark.Core;
using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark(Baseline = true)]
        public async Task ParallelExecution()
        {
            var data = DataGenerator.People;

            await Parallel.ForEachAsync(data, async (unit, cancellationToken) =>
            {
                await Processor.ExecuteAsync(unit);
            });
        }

        [Benchmark]
        public async Task AsyncAwaitExecution()
        {
            var data = DataGenerator.People;

            foreach (var unit in data)
            {
                await Processor.ExecuteAsync(unit);
            }
        }

        [Benchmark]
        public void SyncExecution()
        {
            var data = DataGenerator.People;

            foreach (var unit in data)
            {
                Processor.Execute(unit);
            }
        }

        [Benchmark]
        public async Task BatchExecution()
        {
            var data = DataGenerator.People;
            var workers = Enumerable.Range(0, 10).Select(i => new Worker()).ToList();

            //assign
            var count = data.Count;
            var workerCount = workers.Count;
            for (int i = 0; i < count; i++)
            {
                var whichWorker = i % workerCount;

                var worker = workers[whichWorker];
                var unit = data[i];
                worker.People.Add(unit);
            }

            await Task.WhenAll(workers.Select(worker => worker.Execute()));
        }
    }
}
