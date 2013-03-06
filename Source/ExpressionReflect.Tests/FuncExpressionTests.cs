// ReSharper disable InconsistentNaming
namespace ExpressionReflect.Tests
{
	using System;
	using System.Linq.Expressions;
	using ExpressionReflect.Tests.Model;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class FuncExpressionTests
	{
		#region Property

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

		#endregion

		#region Method

		[Test]
		public void ShouldCreateSimpleFunc_MethodCall()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => x.CalculateAge();
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(Customer.AgeConstant);
			reflectionResult.Should().Be(Customer.AgeConstant);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_MethodCall_WithExpressionParameters()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => x.CalculateLength(x.Firstname);
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(4);
			reflectionResult.Should().Be(4);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_MethodCall_WithExpressionParameters_BinaryExpression()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => x.Calculate(x.Age + x.Value);
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(76);
			reflectionResult.Should().Be(76);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_MethodCall_WithExpressionParameters_BinaryExpression_ConstantExpression()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => x.Calculate(x.Age + 100);
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(166);
			reflectionResult.Should().Be(166);
			reflectionResult.Should().Be(emitResult);
		}

		#endregion

		#region Constructor

		[Test]
		public void ShouldCreateSimpleFunc_New_WithoutParameters()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => new Customer();
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().NotBeNull();
			reflectionResult.Should().NotBeNull();
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => new Customer(x.Lastname, x.Firstname);
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().NotBeNull();
			reflectionResult.Should().NotBeNull();
			reflectionResult.Firstname.Should().Be(emitResult.Firstname);
			reflectionResult.Lastname.Should().Be(emitResult.Lastname);
			reflectionResult.Firstname.Should().Be("Doe");
			reflectionResult.Lastname.Should().Be("John");
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_BinaryExpression()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => new Customer(x.Age + x.Value);
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(43);
			reflectionResult.CalculationValue.Should().Be(43);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_BinaryExpression_ConstantExpression()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => new Customer(x.Age + 100);
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(133);
			reflectionResult.CalculationValue.Should().Be(133);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		#endregion
	}
}
// ReSharper restore InconsistentNaming
