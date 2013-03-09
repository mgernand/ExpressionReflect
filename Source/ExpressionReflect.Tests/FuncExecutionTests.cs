// ReSharper disable InconsistentNaming
namespace ExpressionReflect.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
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

		[Test]
		public void ShouldCreateSimpleFunc_ArrayAccess_Local()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			string[] array = new string[] { "One", "Two" };
			Expression<Func<Customer, string>> expression = x => array[0];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("One");
			reflectionResult.Should().Be("One");
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_ArrayAccess_Delegate()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Func<string[]> func = () => new string[] { "One", "Two" };
			Expression<Func<Customer, string>> expression = x => func()[0];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("One");
			reflectionResult.Should().Be("One");
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_ArrayAccess_Delegate_CallingInvoke()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Func<string[]> func = () => new string[] { "One", "Two" };
			Expression<Func<Customer, string>> expression = x => func.Invoke()[0];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("One");
			reflectionResult.Should().Be("One");
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_Delegate_CallingMethod()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Func<string> func = () => "Hello";
			Expression<Func<Customer, string>> expression = x => func().ToLower();
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("hello");
			reflectionResult.Should().Be("hello");
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_Delegate_CallingMethod_WithParameter()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Func<string, string> func = x => "hello " + x;
			Expression<Func<Customer, string>> expression = x => func("test").Trim('h', 't');
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string> emit = expression.Compile();
			Func<Customer, string> reflection = expression.Reflect();

			string emitResult = emit.Invoke(customer);
			string reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Should().Be("ello tes");
			reflectionResult.Should().Be("ello tes");
			reflectionResult.Should().Be(emitResult);
		}

		[Test]
		public void ShouldCreateSimpleFunc_ObjectInitializer()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, Customer>> expression = x => new Customer
			{
				Firstname = x.Firstname, 
				Lastname = x.Lastname
			};
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, Customer> emit = expression.Compile();
			Func<Customer, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(customer);
			Customer reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Firstname.Should().Be("John");
			reflectionResult.Firstname.Should().Be("John");
			emitResult.Lastname.Should().Be("Doe");
			reflectionResult.Lastname.Should().Be("Doe");
		}

		[Test]
		public void ShouldCreateSimpleFunc_ListInitializer()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, IList<string>>> expression = x => new List<string>
			{
				"Hello", "World"
			};
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, IList<string>> emit = expression.Compile();
			Func<Customer, IList<string>> reflection = expression.Reflect();

			IList<string> emitResult = emit.Invoke(customer);
			IList<string> reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Count.Should().Be(2);
			emitResult[0].Should().Be("Hello");
			reflectionResult.Count.Should().Be(2);
			reflectionResult[0].Should().Be("Hello");
		}

		[Test]
		public void ShouldCreateSimpleFunc_DictionaryInitializer()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, IDictionary<string, string>>> expression = 
				x => new Dictionary<string, string>
				{
					{ "1", "Hello" },
					{ "2", "World" }
				};
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, IDictionary<string, string>> emit = expression.Compile();
			Func<Customer, IDictionary<string, string>> reflection = expression.Reflect();

			IDictionary<string, string> emitResult = emit.Invoke(customer);
			IDictionary<string, string> reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Count.Should().Be(2);
			emitResult["1"].Should().Be("Hello");
			reflectionResult.Count.Should().Be(2);
			reflectionResult["1"].Should().Be("Hello");
		}

		[Test]
		public void ShouldCreateSimpleFunc_CreateNewArray()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, string[]>> expression = x => new string[] { "1", "2" };
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string[]> emit = expression.Compile();
			Func<Customer, string[]> reflection = expression.Reflect();

			string[] emitResult = emit.Invoke(customer);
			string[] reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult[0].Should().Be("1");
			reflectionResult[0].Should().Be("1");
			reflectionResult[0].Should().Be(emitResult[0]);
		}

		[Test]
		public void ShouldCreateSimpleFunc_CreateNewArray_Bounds()
		{
			// Arrange
			Customer customer = new Customer("John", "Doe");
			Expression<Func<Customer, string[]>> expression = x => new string[12];
			Console.WriteLine(expression.ToString());

			// Act
			Func<Customer, string[]> emit = expression.Compile();
			Func<Customer, string[]> reflection = expression.Reflect();

			string[] emitResult = emit.Invoke(customer);
			string[] reflectionResult = reflection.Invoke(customer);

			// Assert
			emitResult.Length.Should().Be(12);
			reflectionResult.Length.Should().Be(12);
			reflectionResult.Length.Should().Be(emitResult.Length);
		}

		[Test]
		public void ShouldCreateSimpleFunc_LinqSimple()
		{
			// Arrange
			IList<Customer> list = new List<Customer>();
			list.Add(new Customer("John", "Doe"));
			list.Add(new Customer("Jane", "Doe"));
			Expression<Func<IList<Customer>, Customer>> expression = a => a.FirstOrDefault(x => x.Lastname == "Doe");
			Console.WriteLine(expression.ToString());

			// Act
			Func<IList<Customer>, Customer> emit = expression.Compile();
			Func<IList<Customer>, Customer> reflection = expression.Reflect();

			Customer emitResult = emit.Invoke(list);
			Customer reflectionResult = reflection.Invoke(list);

			// Assert
			emitResult.Firstname.Should().Be("John");
			reflectionResult.Firstname.Should().Be("John");
		}

		[Test]
		public void ShouldCreateSimpleFunc_LinqComplex()
		{
			// Arrange
			IList<Customer> list = new List<Customer>();
			list.Add(new Customer("John", "Doe"));
			list.Add(new Customer("Jane", "Doe"));
			Expression<Func<IList<Customer>, Customer[]>> expression = a => a.Where(x => x.Lastname == "Doe").OrderBy(x => x.Firstname).ToArray();
			Console.WriteLine(expression.ToString());

			// Act
			Func<IList<Customer>, Customer[]> emit = expression.Compile();
			Func<IList<Customer>, Customer[]> reflection = expression.Reflect();

			Customer[] emitResult = emit.Invoke(list);
			Customer[] reflectionResult = reflection.Invoke(list);

			// Assert
			emitResult.Length.Should().Be(2);
			reflectionResult.Length.Should().Be(2);
			reflectionResult.Length.Should().Be(emitResult.Length);
		}
	}
}
// ReSharper restore InconsistentNaming
