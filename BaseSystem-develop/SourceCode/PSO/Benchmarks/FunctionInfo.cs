namespace PSO.Benchmarks
{
    public class FunctionInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }

        public FunctionInfo(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
        }
    }
}
