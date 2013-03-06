namespace ExpressionReflect
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	internal sealed class ReflectionOutputExpressionVisitor : ExpressionVisitor
	{
		private readonly IDictionary<string, object> args;
		private readonly Stack<object> result = new Stack<object>();
		private readonly Stack<Type> conversionStack = new Stack<Type>(); 

		internal ReflectionOutputExpressionVisitor(IDictionary<string, object> args)
		{
			this.args = args;
		}

		internal object GetResult(Expression expression)
		{
			this.Visit(expression);

			if (this.result.Count != 1)
			{
				throw new ArgumentException("The result stack contained too much elements.");
			}

			return this.result.Pop();
		}

		protected override Expression VisitMemberAccess(MemberExpression m)
		{
			MemberInfo memberInfo = m.Member;

			if (memberInfo is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;

				ParameterExpression parameter = (ParameterExpression)m.Expression;
				object value = propertyInfo.GetValue(this.GetInvocationTarget(parameter), null);
				this.result.Push(value);
			}
			// Todo: Fields

			return base.VisitMemberAccess(m);
		}

		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression methodCallExpression = base.VisitMethodCall(m);

			object[] parameterValues = this.GetValuesFromStack(m.Arguments);
			MethodInfo methodInfo = m.Method;
		
			ParameterExpression parameter = (ParameterExpression)m.Object;
			object value = methodInfo.Invoke(this.GetInvocationTarget(parameter), parameterValues);
			this.result.Push(value);
			
			return methodCallExpression;
		}

		protected override NewExpression VisitNew(NewExpression nex)
		{
			NewExpression newExpression = base.VisitNew(nex);

			ConstructorInfo constructorInfo = nex.Constructor;
			object[] parameterValues = this.GetValuesFromStack(nex.Arguments);

			object value = constructorInfo.Invoke(parameterValues.ToArray());
			this.result.Push(value);

			return newExpression;
		}

		protected override Expression VisitBinary(BinaryExpression b)
		{
			Expression binaryExpression = base.VisitBinary(b);

			object[] values = this.GetValuesFromStack(2);
			double left = Convert.ToDouble(values.First());
			double right = Convert.ToDouble(values.Last());
			double value;

			switch (b.NodeType)
			{
				case ExpressionType.Add:
					value = left + right;
					break;	
				case ExpressionType.Subtract:
					value = left - right;
					break;
				case ExpressionType.Multiply:
					value = left * right;
					break;
				case ExpressionType.Divide:
					value = left / right;
					break;
				case ExpressionType.Modulo:
					value = left % right;
					break;
				//case ExpressionType.AddChecked:
				//case ExpressionType.SubtractChecked:
				//case ExpressionType.MultiplyChecked:					
				//case ExpressionType.And:
				//case ExpressionType.AndAlso:
				//case ExpressionType.Or:
				//case ExpressionType.OrElse:
				//case ExpressionType.LessThan:
				//case ExpressionType.LessThanOrEqual:
				//case ExpressionType.GreaterThan:
				//case ExpressionType.GreaterThanOrEqual:
				//case ExpressionType.Equal:
				//case ExpressionType.NotEqual:
				//case ExpressionType.Coalesce:
				//case ExpressionType.ArrayIndex:
				//case ExpressionType.RightShift:
				//case ExpressionType.LeftShift:
				//case ExpressionType.ExclusiveOr:
				default:
					throw new ArgumentOutOfRangeException();
			}

			this.result.Push(value);
			
			return binaryExpression;
		}

		private object[] GetValuesFromStack(IEnumerable<Expression> arguments)
		{
			IList<object> parameterValues = new List<object>();

			Expression[] expressions = arguments.ToArray();
			for (int i = 0; i < expressions.Count(); i++)
			{
				Expression expression = expressions[i];		
				object parameterValue = this.result.Pop();
				parameterValue = Convert.ChangeType(parameterValue, expression.Type, CultureInfo.InvariantCulture);
				parameterValues.Add(parameterValue);
			}

			return parameterValues.Reverse().ToArray();
		}

		private object[] GetValuesFromStack(int count)
		{
			IList<object> parameterValues = new List<object>();

			for (int i = 0; i < count; i++)
			{
				object parameterValue = this.result.Pop();
				parameterValues.Add(parameterValue);
			}

			return parameterValues.Reverse().ToArray();
		}

		private object GetInvocationTarget(ParameterExpression parameter)
		{
			return this.args[parameter.Name];
		}
	}
}