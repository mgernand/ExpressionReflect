namespace ExpressionReflect
{
	using System.Linq.Expressions;

	public static class ExpressionExtensions
	{
		public static object Execute(this Expression expression, params object[] values)
		{
			ReflectionOutputExpressionVisitor visitor = new ReflectionOutputExpressionVisitor(expression, values);
			object result = visitor.Execute();
			return result;
		}
	}
}