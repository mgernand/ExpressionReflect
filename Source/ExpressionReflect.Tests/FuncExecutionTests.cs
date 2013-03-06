// ReSharper disable InconsistentNaming
namespace ExpressionReflect.Tests
{
	using System;
	using System.Linq.Expressions;
	using ExpressionReflect.Tests.Model;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class FuncExecutionTests
	{
		[Test]
		public void ShouldCreateSimpleFunc_PropertyGetter()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, string>> expression = x => x.Firstname;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("John");
			reflectionResult.Should().Be("John");
			reflectionResult.Should().Be(emitResult);
		}

	}
}
// ReSharper restore InconsistentNaming
