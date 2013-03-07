namespace ExpressionReflect
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public static class ExpressionExtensions
	{
		public static Func<TResult> Reflect<TResult>(this Expression<Func<TResult>> target)
		{
			Func<TResult> func = () => (TResult)Execute(target, CreateArgs(target.Parameters));
			return func;
		}

		public static Func<T, TResult> Reflect<T, TResult>(this Expression<Func<T, TResult>> target)
		{
			Func<T, TResult> func = a => (TResult)Execute(target, CreateArgs(target.Parameters, a));
			return func;
		}

		public static Func<T1, T2, TResult> Reflect<T1, T2, TResult>(this Expression<Func<T2, T2, TResult>> target)
		{
			Func<T1, T2, TResult> func = (a, b) => (TResult)Execute(target, CreateArgs(target.Parameters, a, b));
			return func;
		}

		public static Func<T1, T2, T3, TResult> Reflect<T1, T2, T3, TResult>(this Expression<Func<T2, T2, T3, TResult>> target)
		{
			Func<T1, T2, T3, TResult> func = (a, b, c) => (TResult)Execute(target, CreateArgs(target.Parameters, a, b, c));
			return func;
		}

		public static Func<T1, T2, T3, T4, TResult> Reflect<T1, T2, T3, T4, TResult>(this Expression<Func<T2, T2, T3, T4, TResult>> target)
		{
			Func<T1, T2, T3, T4, TResult> func = (a, b, c, d) => (TResult)Execute(target, CreateArgs(target.Parameters, a, b, c, d));
			return func;
		}

		private static object Execute(Expression expression, IDictionary<string, object> args)
		{
			ReflectionOutputExpressionVisitor visitor = new ReflectionOutputExpressionVisitor(args);
			object result = visitor.GetResult(expression);

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