namespace ExpressionReflect
{
	using System.Linq.Expressions;

	internal static class ExpressionReflector
	{
		internal static object Execute(Expression expression, params object[] values)
		{
			ReflectionOutputExpressionVisitor visitor = new ReflectionOutputExpressionVisitor(expression, values);
			object result = visitor.Execute();
			return result;
		}
	}
}