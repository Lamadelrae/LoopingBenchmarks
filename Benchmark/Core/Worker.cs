namespace Benchmark.Core
{
    public class Worker
    {
        public List<Person> People { get; set; } = new List<Person>();

        public async Task Execute()
        {
            foreach (var person in People)
            {
                await Processor.ExecuteAsync(person);
            }
        }
    }
}
