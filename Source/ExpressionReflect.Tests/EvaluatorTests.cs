// ReSharper disable InconsistentNaming
namespace ExpressionReflect.Tests
{
	using System;
	using System.Linq.Expressions;
	using ExpressionReflect.Tests.Model;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EvaluatorTests
	{
		private string GetString()
		{
			return "test";
		}

		private static string GetStringStatic()
		{
			return "test";
		}

		private string GetString(int i)
		{
			return "test" + i;
		}

		private static string GetStringStatic(int i)
		{
			return "test" + i;
		}

		[Test]
		public void ShouldPreEvaluate_LocalVariable()
		{
			// Arrange
			string str = "test";
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == str;

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test"")");
		}

		[Test]
		public void ShouldPreEvaluate_LocalFuncWithoutParameter()
		{
			// Arrange
			Func<string> func = () => "test";
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == func();

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test"")");
		}

		[Test]
		public void ShouldPreEvaluate_LocalFuncWithConstantParameter()
		{
			// Arrange
			Func<int, string> func = x => "test" + x;
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == func(5);

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_LocalFuncWithLocalVariableParameter()
		{
			// Arrange
			Func<int, string> func = x => "test" + x;
			int i = 5;
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == func(i);

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_LocalFuncWithLocalFuncParameter()
		{
			// Arrange
			Func<int, string> func = x => "test" + x;
			Func<int> i = () => 5;
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == func(i());

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_InstanceMethodCallWithoutParameter()
		{
			// Arrange
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == this.GetString();

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test"")");
		}

		[Test]
		public void ShouldPreEvaluate_StaticMethodCallWithoutParameter()
		{
			// Arrange
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == GetStringStatic();

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test"")");
		}

		[Test]
		public void ShouldPreEvaluate_InstanceMethodCallWithConstantParameter()
		{
			// Arrange
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == this.GetString(5);

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_StaticMethodCallWithConstantParameter()
		{
			// Arrange
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == GetStringStatic(5);

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_InstanceMethodCallWithLocalVariableParameter()
		{
			// Arrange
			int i = 5;
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == this.GetString(i);

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_StaticMethodCallWithLocalVariableParameter()
		{
			// Arrange
			int i = 5;
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == GetStringStatic(i);

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_InstanceMethodCallWithLocalFuncParameter()
		{
			// Arrange
			Func<int> i = () => 5;
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == this.GetString(i());

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_StaticMethodCallWithLocalFuncParameter()
		{
			// Arrange
			Func<int> i = () => 5;
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == GetStringStatic(i());

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == ""test5"")");
		}

		[Test]
		public void ShouldPreEvaluate_LocalFuncWithLambdaParameterParameter()
		{
			// Arrange
			Func<string, string> func = x => x;
			Expression<Func<Customer, bool>> expresion = x => x.Firstname == func(x.Lastname);

			// Act
			string expressionString = expresion.PartialEval().ToString();
			Console.WriteLine(expressionString);

			// Assert
			expressionString.Should().Be(@"x => (x.Firstname == func(x.Lastname))");
		}
	}
}
// ReSharper restore InconsistentNaming
