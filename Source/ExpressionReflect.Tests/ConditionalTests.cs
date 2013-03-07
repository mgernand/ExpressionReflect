// ReSharper disable InconsistentNaming
namespace ExpressionReflect.Tests
{
	using System;
	using System.Linq.Expressions;
	using ExpressionReflect.Tests.Model;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class ConditionalTests
	{
		[Test]
		public void ShouldExecuteConditional()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe") { IsPremium = true };
			Expression<Func<Customer, int>> expression = x => x.IsPremium ? 500 : 300;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(500);
			reflectionResult.Should().Be(500);
			reflectionResult.Should().Be(emitResult);
		}

	}
}
// ReSharper restore InconsistentNaming
