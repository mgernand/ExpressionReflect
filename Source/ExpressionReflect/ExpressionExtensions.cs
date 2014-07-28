namespace ExpressionReflect
{
	using System.Linq.Expressions;
	using JetBrains.Annotations;

	public static class ExpressionExtensions
	{
		[PublicAPI]
		public static object Execute(this Expression expression, params object[] values)
		{
			ExpressionReflectionExecutor visitor = new ExpressionReflectionExecutor(expression);
			object result = visitor.Execute(values);
			return result;
		}

		[PublicAPI]
		public static TResult Execute<TResult>(this Expression expression, params object[] values)
		{
			return (TResult) expression.Execute(values);
		}
	}
}