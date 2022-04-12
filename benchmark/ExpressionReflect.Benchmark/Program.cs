namespace ExpressionReflect.Benchmark
{
	using BenchmarkDotNet.Running;

	public static class Program
	{
		public static void Main(string[] args)
		{
			// Benchmark result for v2.0.3
			// ---------------------------
			// 
			// As expected the reflection-based ExpressionReflect approach is the slowest of the three methods.
			//
			//

			//|                                        Method |       Mean |     Error |    StdDev | Rank |  Gen 0 | Allocated |
			//|---------------------------------------------- |-----------:|----------:|----------:|-----:|-------:|----------:|
			//|                      GetPropertyValue_Compile |   1.111 ns | 0.0513 ns | 0.0591 ns |    1 |      - |         - |
			//| GetPropertyValue_Compile_PreferInterpretation |  75.320 ns | 1.5276 ns | 2.2864 ns |    2 | 0.0362 |     152 B |
			//|            GetPropertyValue_ExpressionReflect | 281.360 ns | 4.8041 ns | 4.2587 ns |    3 | 0.1297 |     544 B |
			BenchmarkRunner.Run<Benchmarks>();
		}
	}
}
