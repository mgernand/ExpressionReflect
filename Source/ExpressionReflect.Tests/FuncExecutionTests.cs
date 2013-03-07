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

		[Test]
		public void ShouldCreateSimpleFunc_Indexer()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => x[2];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(35);
			reflectionResult.Should().Be(35);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_Indexer_MultipleParameters()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			int arg = 5;
			Expression<Func<Customer, int>> expression = x => x[2, arg];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(40);
			reflectionResult.Should().Be(40);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_PropertyGetter_Indexer()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, char>> expression = x => x.Firstname[0];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, char> emit = expression.Compile();
			Func<Customer, char> reflection = expression.Reflect();

			char emitResult = emit.Invoke(customer);
			char reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be('J');
			reflectionResult.Should().Be('J');
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_PropertyGetter_CustomIndexer()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, int>> expression = x => x.NameIndex["John"];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, int> emit = expression.Compile();
			Func<Customer, int> reflection = expression.Reflect();

			int emitResult = emit.Invoke(customer);
			int reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be(0);
			reflectionResult.Should().Be(0);
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_ArrayAccess()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, string>> expression = x => x.Names[1];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("Doe");
			reflectionResult.Should().Be("Doe");
			reflectionResult.Should().Be(emitResult);
		}
	}
}
// ReSharper restore InconsistentNaming
