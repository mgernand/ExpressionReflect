// ReSharper disable InconsistentNaming
namespace ExpressionReflect.Tests
{
	using System;
	using System.Linq.Expressions;
	using ExpressionReflect.Tests.Model;
	using FluentAssertions;
	using NUnit.Framework;

	// Todo: Add test for every operator
	[TestFixture]
	public class UnaryOperatorTests
	{
		[Test]
		public void ShouldExecuteOperator_Not()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe") { IsPremium = true };
			Expression<Func<Customer, bool>> expression = x => !x.IsPremium;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, bool> emit = expression.Compile();
			Func<Customer, bool> reflection = expression.Reflect();

			bool emitResult = emit.Invoke(customer);
			bool reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().BeFalse();
			reflectionResult.Should().BeFalse();
			reflectionResult.Should().Be(emitResult);
		}
	}
}
// ReSharper restore InconsistentNaming
