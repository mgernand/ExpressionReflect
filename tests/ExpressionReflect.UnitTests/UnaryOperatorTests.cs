namespace ExpressionReflect.UnitTests
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

		[Test]
		public void ShouldExecuteOperator_Convert()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, string>> expression = x => x.Firstname + "-" + 50;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("John-50");
			reflectionResult.Should().Be("John-50");
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldExecuteOperator_ArrayLength()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => x.Names.Length;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(2);
			reflectionResult.Should().Be(2);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldExecuteOperator_TypeAs()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => x.Object as Customer;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(null);
			reflectionResult.Should().Be(null);
			reflectionResult.Should().Be(emitResult);
		}
	}

}
// ReSharper restore InconsistentNaming
