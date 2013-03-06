namespace ExpressionReflect
{
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;

	internal sealed class ReflectionOutputExpressionVisitor : ExpressionVisitor
	{
		// Todo: When evaluating complex(er) expressions like 'x != y' we maybe need to stack up the intermediate values.
		private object result = null;
		private readonly IDictionary<string, object> args;

		internal ReflectionOutputExpressionVisitor(IDictionary<string, object> args)
		{
			this.args = args;
		}

		protected override Expression VisitMemberAccess(MemberExpression m)
		{
			ParameterExpression parameter = (ParameterExpression)m.Expression;
			MemberInfo memberInfo = m.Member;
			if (memberInfo is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				object value = propertyInfo.GetValue(this.args[parameter.Name], null);
				this.result = value;
			}

			return base.VisitMemberAccess(m);
		}

		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			ParameterExpression parameter = (ParameterExpression)m.Object;
			MethodInfo methodInfo = m.Method;

			if(parameter != null)
			{		
				object value = methodInfo.Invoke(this.args[parameter.Name], null);
				this.result = value;	
			}

			return base.VisitMethodCall(m);
		}

		public object GetResult(Expression expression)
		{
			this.Visit(expression);
			return this.result;
		}
	}
}