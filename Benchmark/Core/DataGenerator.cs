namespace Benchmark.Core
{
    public class DataGenerator
    {
        public static List<Person> People = new List<Person>(Enumerable.Range(0, 100).Select(i => new Person()));
    }
}
