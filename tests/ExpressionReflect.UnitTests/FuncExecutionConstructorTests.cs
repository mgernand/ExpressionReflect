namespace ExpressionReflect.UnitTests
{
	using System;
	using System.Linq.Expressions;
	using ExpressionReflect.Tests.Model;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class FuncExecutionConstructorTests
	{
		[Test]
		public void ShouldCreateSimpleFunc_New()
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

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_BinaryExpression_LocalVariable()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			int value = 666;
			Expression<Func<Customer, Customer>> expression = x => new Customer(value);
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(666);
			reflectionResult.CalculationValue.Should().Be(666);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_NestedNew()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			int value = 666;
			Expression<Func<Customer, Customer>> expression = x => new Customer(new Customer(value));
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(666);
			reflectionResult.CalculationValue.Should().Be(666);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_NestedMethodCall()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => new Customer(x.CalculateAge());
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(33);
			reflectionResult.CalculationValue.Should().Be(33);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_NestedDelegateCall()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Func<int> method = () => 100;
			Expression<Func<Customer, Customer>> expression = x => new Customer(method());
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(100);
			reflectionResult.CalculationValue.Should().Be(100);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_NestedDelegateCall_WithParameters_Constant()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Func<int, int> method = x => x + 100;
			Expression<Func<Customer, Customer>> expression = x => new Customer(method(10));
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(110);
			reflectionResult.CalculationValue.Should().Be(110);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_NestedDelegateCall_WithParameters_Local()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Func<int, int> method = x => x + 100;
			int arg = 10;
			Expression<Func<Customer, Customer>> expression = x => new Customer(method(arg));
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(110);
			reflectionResult.CalculationValue.Should().Be(110);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithExpressionParameters_NestedDelegateCall_WithParameters_BinaryExpression()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Func<int, int> method = x => x + 100;
			Expression<Func<Customer, Customer>> expression = x => new Customer(method(5 + 5));
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.CalculationValue.Should().Be(110);
			reflectionResult.CalculationValue.Should().Be(110);
			reflectionResult.CalculationValue.Should().Be(emitResult.CalculationValue);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_MethodCall()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => new Customer().CalculateAge();
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(33);
			reflectionResult.Should().Be(33);
		}

		[Test]
		public void ShouldCreateSimpleFunc_New_WithMixedParameters()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => new Customer(x.Lastname, x, 10, x.Firstname);
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
			reflectionResult.CalculationValue.Should().Be(43);
		}
	}
}
// ReSharper restore InconsistentNaming
