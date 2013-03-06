namespace ExpressionReflect
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	internal sealed class ReflectionOutputExpressionVisitor : ExpressionVisitor
	{
		private readonly IDictionary<string, object> args;
		private readonly Stack<object> evaluatedData = new Stack<object>();
		private readonly Stack<string> localVariableNames = new Stack<string>(); 

		internal ReflectionOutputExpressionVisitor(IDictionary<string, object> args)
		{
			this.args = args;
		}

		internal object GetResult(Expression expression)
		{
			this.Visit(expression);

			if (this.evaluatedData.Count != 1)
			{
				throw new ArgumentException("The result stack contained too much elements.");
			}

			return this.evaluatedData.Pop();
		}

		protected override Expression VisitMemberAccess(MemberExpression m)
		{
			MemberInfo memberInfo = m.Member;

			if (memberInfo is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;

				ParameterExpression parameter = (ParameterExpression)m.Expression;
				object value = propertyInfo.GetValue(this.GetInvocationTarget(parameter), null);
				this.evaluatedData.Push(value);
			}
			if(memberInfo is FieldInfo)
			{
				bool isCompilerGenerated = memberInfo.DeclaringType.IsCompilerGenerated();
				if (isCompilerGenerated) // Special case for local variables
				{
					localVariableNames.Push(memberInfo.Name);
				}
			}

			return base.VisitMemberAccess(m);
		}

		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression methodCallExpression = base.VisitMethodCall(m);

			object[] parameterValues = this.GetValuesFromStack(m.Arguments);
			MethodInfo methodInfo = m.Method;
		
			ParameterExpression parameter = (ParameterExpression)m.Object;
			object value = methodInfo.Invoke(this.GetInvocationTarget(parameter), parameterValues);
			this.evaluatedData.Push(value);
			
			return methodCallExpression;
		}

		protected override NewExpression VisitNew(NewExpression nex)
		{
			NewExpression newExpression = base.VisitNew(nex);

			ConstructorInfo constructorInfo = nex.Constructor;
			object[] parameterValues = this.GetValuesFromStack(nex.Arguments);

			object value = constructorInfo.Invoke(parameterValues.ToArray());
			this.evaluatedData.Push(value);

			return newExpression;
		}

		protected override Expression VisitBinary(BinaryExpression b)
		{
			Expression binaryExpression = base.VisitBinary(b);

			object[] values = this.GetValuesFromStack(2);
			object value;

			switch (b.NodeType)
			{
				case ExpressionType.Add:
					value = Convert.ToDouble(values.First()) + Convert.ToDouble(values.Last());
					break;	
				case ExpressionType.Subtract:
					value = Convert.ToDouble(values.First()) - Convert.ToDouble(values.Last());
					break;
				case ExpressionType.Multiply:
					value = Convert.ToDouble(values.First()) * Convert.ToDouble(values.Last());
					break;
				case ExpressionType.Divide:
					value = Convert.ToDouble(values.First()) / Convert.ToDouble(values.Last());
					break;
				case ExpressionType.Modulo:
					value = Convert.ToDouble(values.First()) % Convert.ToDouble(values.Last());
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

			this.evaluatedData.Push(value);
			
			return binaryExpression;
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			object value = null;

			bool isCompilerGenerated = c.Type.IsCompilerGenerated();
			if(isCompilerGenerated) // Special case for local variables
			{
				string memberName = this.localVariableNames.Pop();
				MemberInfo memberInfo = c.Type.GetMember(memberName).First();

				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				value = fieldInfo.GetValue(c.Value);

				if(value is Delegate)
				{
					Delegate del = (Delegate)value;
					ParameterInfo[] parameterInfos = del.Method.GetParameters();
					object[] parameterValues = GetValuesFromStack(parameterInfos.Length);
					value = del.DynamicInvoke(parameterValues);
				}
			}
			else
			{
				value = c.Value;
			}

			this.evaluatedData.Push(value);

			return base.VisitConstant(c);
		}

		private object[] GetValuesFromStack(IEnumerable<Expression> arguments)
		{
			IList<object> parameterValues = new List<object>();

			Expression[] expressions = arguments.ToArray();
			for (int i = 0; i < expressions.Count(); i++)
			{
				Expression expression = expressions[i];		
				object parameterValue = this.evaluatedData.Pop();
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
				object parameterValue = this.evaluatedData.Pop();
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