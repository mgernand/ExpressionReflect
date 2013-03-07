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
		public void ShouldCreateSimpleFunc_ReturnExpressionParameter()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => x;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().NotBeNull();
			reflectionResult.Should().NotBeNull();
			reflectionResult.Should().BeSameAs(emitResult);
		}

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

		[Test]
		public void ShouldCreateSimpleFunc_PropertyGetter_MethodCall()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, string>> expression = x => x.Firstname.ToLower();
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("john");
			reflectionResult.Should().Be("john");
			reflectionResult.Should().Be(emitResult);
		}


		[Test]
		public void ShouldCreateSimpleFunc_PropertyGetter_BinaryExpression()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, bool>> expression = x => x.Firstname != "Jane";
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

		[Test]
		public void ShouldCreateSimpleFunc_PropertyGetter_UnaryExpression()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			int arg = 10;
			Expression<Func<Customer, int>> expression = x => -arg;
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(-10);
			reflectionResult.Should().Be(-10);
			reflectionResult.Should().Be(emitResult);
		}
	}
}
// ReSharper restore InconsistentNaming
