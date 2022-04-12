namespace ExpressionReflect
{
	using System.Linq.Expressions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="Expression" /> type.
	/// </summary>
	[PublicAPI]
	public static class ExpressionExtensions
	{
		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static object Execute(this Expression expression, params object[] values)
		{
			ExpressionReflectionExecutor visitor = new ExpressionReflectionExecutor(expression);
			object result = visitor.Execute(values);
			return result;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="expression"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static TResult Execute<TResult>(this Expression expression, params object[] values)
		{
			return (TResult)expression.Execute(values);
		}
	}
}
