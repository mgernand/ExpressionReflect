namespace ExpressionReflect
{
	using System.Linq.Expressions;

	public static class ExpressionReflector
	{
		public static object Execute(Expression expression, params object[] values)
		{
			ReflectionOutputExpressionVisitor visitor = new ReflectionOutputExpressionVisitor(expression, values);
			object result = visitor.Execute();
			return result;
		}
	}
}