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
	public class BinaryOperatorTests
	{
		[Test]
		public void ShouldExecuteOperator_Add()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => 33 + 10;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(43);
			reflectionResult.Should().Be(43);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldExecuteOperator_AddChecked()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => checked(33 + 10);
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(43);
			reflectionResult.Should().Be(43);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldExecuteOperator_TypeIs()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
#pragma warning disable 183
			Expression<Func<Customer, bool>> expression = x => x is Customer;
#pragma warning restore 183
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, bool> emit = expression.Compile();
			Func<Customer, bool> reflection = expression.Reflect();

			bool emitResult = emit.Invoke(customer);
			bool reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().BeTrue();
			reflectionResult.Should().BeTrue();
			reflectionResult.Should().Be(emitResult);
		}
	}
}
// ReSharper restore InconsistentNaming
