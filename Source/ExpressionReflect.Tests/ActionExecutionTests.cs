// ReSharper disable InconsistentNaming
namespace ExpressionReflect.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using ExpressionReflect.Tests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class ActionExecutionTests
	{
		[Test]
		public void ShouldCreateSimpleAction_NestedAction()
		{
			IList<string> list = new List<string>();
			list.Add("Hallo");
			list.Add("Welt");

			Expression<Action<IEnumerable<string>>> expression = x => x.ForEach(() => Console.WriteLine("Write"));
			Console.WriteLine(expression);

			Action<IEnumerable<string>> reflect = expression.Reflect();
			reflect.Invoke(list);
		}
	}
}
// ReSharper restore InconsistentNaming
