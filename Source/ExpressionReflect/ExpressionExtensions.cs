namespace ExpressionReflect
{
	using System.Linq.Expressions;

	public static class ExpressionExtensions
	{
		public static object Execute(this Expression expression, params object[] values)
		{
			ExpressionReflectionExecutor visitor = new ExpressionReflectionExecutor(expression, values);
			object result = visitor.Execute();
			return result;
		}
	}
}