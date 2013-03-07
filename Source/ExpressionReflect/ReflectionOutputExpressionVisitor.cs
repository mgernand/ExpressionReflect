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

		internal object GetResult(Expression expression, Type returnType)
		{
			this.Visit(expression);

			if (this.evaluatedData.Count > 1)
			{
				throw new ArgumentException("The result stack contained too much elements.");
			}
			if (this.evaluatedData.Count < 1)
			{
				throw new ArgumentException("The result stack contained too few elements.");
			}

			object value = this.GetValueFromStack(returnType);
			return value;
		}

		protected override Expression VisitParameter(ParameterExpression p)
		{
			Expression expression = base.VisitParameter(p);

			object argument = this.args[p.Name];
			this.evaluatedData.Push(argument);
			
			return expression;
		}

		protected override Expression VisitMemberAccess(MemberExpression m)
		{
			// Note: Call base.VisitMemberAccess(m) late for local variables special case.
			Expression expression = null;
			MemberInfo memberInfo = m.Member;

			// Call Visit early if declaring type is not compiler generated.
			// If the declaring type is compiler generated we need to wait
			// with the call to Visit until after the variable name discovery.
			bool callVisit = !memberInfo.DeclaringType.IsCompilerGenerated();
			if (callVisit)
			{
				expression = base.VisitMemberAccess(m);
			}

			if (memberInfo is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;

				ParameterExpression parameter = (ParameterExpression)m.Expression;
				object value = propertyInfo.GetValue(this.GetValueFromStack(parameter.Type), null);
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

			return expression ?? base.VisitMemberAccess(m);
		}

		// Todo: Mix expression parameters with constant or other parameters
		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression methodCallExpression = base.VisitMethodCall(m);

			object target = null;
			object[] parameterValues = null;

			MethodInfo methodInfo = m.Method;
			bool isExtensionMethod = methodInfo.DeclaringType.IsExtensionMethod();
			if(isExtensionMethod)
			{
				parameterValues = this.GetValuesFromStack(m.Arguments);
			}
			else
			{
				parameterValues = this.GetValuesFromStack(m.Arguments);
			}		

			object value = null;
			Expression expression = m.Object;
			if (expression is ParameterExpression) // Method call on expression variable (f.e x.DoSomething())
			{
				ParameterExpression parameter = (ParameterExpression)expression;
				target = this.GetValueFromStack(parameter.Type);
			}
			else if (expression is MemberExpression) // The method call was on a property (f.e. x.Text.ToLower())
			{
				MemberExpression parameter = (MemberExpression)expression;
				target = this.GetValueFromStack(parameter.Type);
			}
			else if(expression is MethodCallExpression) // The method was called on a method (f.e. x.ToString().ToLower())
			{
				MethodCallExpression parameter = (MethodCallExpression)expression;
				target = this.GetValueFromStack(parameter.Type);
			}
			else if (expression is NewExpression) // The method was called on a constuctor (f.e. new Entity().DoSomething())
			{
				NewExpression parameter = (NewExpression)expression;
				target = this.GetValueFromStack(parameter.Type);
			}
			// If expression is null the call is static, so the target must and will be null.

			value = methodInfo.Invoke(target, parameterValues);

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
			object value = null;

			// Todo: What if some implemented the operators in a custom class?
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
				case ExpressionType.Equal:
					value = values.First() == values.Last();
					break;
				case ExpressionType.NotEqual:
					value = values.First() != values.Last();
					break;
				case ExpressionType.And:
					value = Convert.ToBoolean(values.First()) & Convert.ToBoolean(values.Last());
					break;
				case ExpressionType.AndAlso:
					value = Convert.ToBoolean(values.First()) && Convert.ToBoolean(values.Last());
					break;
				case ExpressionType.Or:
					value = Convert.ToBoolean(values.First()) | Convert.ToBoolean(values.Last());
					break;
				case ExpressionType.OrElse:
					value = Convert.ToBoolean(values.First()) || Convert.ToBoolean(values.Last());
					break;
				case ExpressionType.ExclusiveOr:
					value = Convert.ToBoolean(values.First()) ^ Convert.ToBoolean(values.Last());
					break;
				case ExpressionType.LessThan:
					value = Convert.ToDouble(values.First()) < Convert.ToDouble(values.Last());
					break;
				case ExpressionType.LessThanOrEqual:
					value = Convert.ToDouble(values.First()) <= Convert.ToDouble(values.Last());
					break;
				case ExpressionType.GreaterThan:
					value = Convert.ToDouble(values.First()) > Convert.ToDouble(values.Last());
					break;
				case ExpressionType.GreaterThanOrEqual:
					value = Convert.ToDouble(values.First()) >= Convert.ToDouble(values.Last());
					break;
				case ExpressionType.RightShift:
					value = Convert.ToInt64(values.First()) >> Convert.ToInt32(values.Last());
					break;
				case ExpressionType.LeftShift:
					value = Convert.ToInt64(values.First()) << Convert.ToInt32(values.Last());
					break;
				case ExpressionType.Coalesce:
					value = values.First() ?? values.Last();
					break;
				//case ExpressionType.AddChecked:
				//case ExpressionType.SubtractChecked:
				//case ExpressionType.MultiplyChecked:					
				case ExpressionType.ArrayIndex:
					object[] array = (object[])values.First();
					value = array[Convert.ToInt64(values.Last())];
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			this.evaluatedData.Push(value);
			
			return binaryExpression;
		}

		protected override Expression VisitUnary(UnaryExpression u)
		{
			Expression unaryExpression = base.VisitUnary(u);

			object[] values = this.GetValuesFromStack(1);
			object value;

			switch (u.NodeType)
			{
				case ExpressionType.Negate:
					value = -Convert.ToDouble(values.First());
					break;
				case ExpressionType.Not:
					if(values.First() is bool)
					{
						value = !Convert.ToBoolean(values.First());
					}
					else
					{
						value = ~Convert.ToInt64(values.First());
					}				
					break;
				//case ExpressionType.NegateChecked:
				//case ExpressionType.Quote:
				//case ExpressionType.Convert:
				//case ExpressionType.ConvertChecked:
				//case ExpressionType.ArrayLength:
				//case ExpressionType.TypeAs:
				default:
					throw new ArgumentOutOfRangeException();
			}

			this.evaluatedData.Push(value);

			return unaryExpression;
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
			arguments = arguments.Reverse();

			Expression[] expressions = arguments.ToArray();
			for (int i = 0; i < expressions.Count(); i++)
			{
				Expression expression = expressions[i];
				object parameterValue = this.GetValueFromStack(expression.Type);
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

		private object GetValueFromStack(Type conversionType)
		{
			object parameterValue = this.evaluatedData.Pop();
			parameterValue = Convert.ChangeType(parameterValue, conversionType, CultureInfo.InvariantCulture);
			return parameterValue;
		}
	}
}