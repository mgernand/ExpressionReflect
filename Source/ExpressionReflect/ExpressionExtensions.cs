namespace ExpressionReflect
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public static class ExpressionExtensions
	{
		public static Func<T, TResult> Reflect<T, TResult>(this Expression<Func<T, TResult>> expression)
		{
			// Note: Remember to always give the parameter values to CreateArgs(...) in the correct order.
			Func<T, TResult> func = x => (TResult)ExecuteExpression(expression, CreateArgs(expression.Parameters, x));
			return func;
		}

		private static object ExecuteExpression(Expression expression, IDictionary<string, object> args)
		{
			object result = null;

			// Todo: Traverse the complete expression tree, create reflection code for every statement and execute it.
			ReflectionOutputExpressionVisitor visitor = new ReflectionOutputExpressionVisitor(args);
			result = visitor.GetResult(expression);

			return result;
		}

		private static IDictionary<string, object> CreateArgs(IEnumerable<ParameterExpression> parameters, params object[] values)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();

			int index = 0;
			foreach(ParameterExpression parameter in parameters)
			{
				string name = parameter.Name;
				dictionary.Add(name, values[index++]);
			}

			return dictionary;
		}
	}
}