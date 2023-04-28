namespace Benchmark.Core
{
    public static class Processor
    {
        public static async Task ExecuteAsync(Person person)
        {
            await Task.Delay(10);
        }

        public static void Execute(Person person)
        {
            Task.Delay(10).Wait();
        }
    }
}
