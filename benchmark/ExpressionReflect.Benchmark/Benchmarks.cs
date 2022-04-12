namespace ExpressionReflect.Benchmark
{
	using System.Linq.Expressions;
	using BenchmarkDotNet.Attributes;
	using BenchmarkDotNet.Order;

	[MemoryDiagnoser]
	[Orderer(SummaryOrderPolicy.FastestToSlowest)]
	[RankColumn]
	public class Benchmarks
	{
		private readonly Customer customer = new Customer { Name = "Niklaus Wirth" };
		private readonly Expression<Func<Customer, string>> expression = x => x.Name;
		private Func<Customer, string> funcCompile;
		private Func<Customer, string> funcCompilePreferInterpretation;

		private Func<Customer, string> funcExpressionReflect;

		[GlobalSetup]
		public void GlobalSetup()
		{
			this.funcExpressionReflect = this.expression.Reflect();
			this.funcCompilePreferInterpretation = this.expression.Compile(true);
			this.funcCompile = this.expression.Compile();
		}

		[Benchmark]
		public void GetPropertyValue_ExpressionReflect()
		{
			string name = this.funcExpressionReflect.Invoke(this.customer);
		}

		[Benchmark]
		public void GetPropertyValue_Compile_PreferInterpretation()
		{
			string name = this.funcCompilePreferInterpretation.Invoke(this.customer);
		}

		[Benchmark]
		public void GetPropertyValue_Compile()
		{
			string name = this.funcCompile.Invoke(this.customer);
		}
	}
}
