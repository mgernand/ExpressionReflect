namespace ExpressionReflect
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	/// <summary>
	/// An expression visit that translates the expression tree into reflection calls.
	/// </summary>
	internal sealed class ReflectionOutputExpressionVisitor : ExpressionVisitor
	{
		private readonly IDictionary<string, object> args;
		private readonly Stack<object> data = new Stack<object>();
		private readonly Stack<string> localVariableNames = new Stack<string>(); 

		internal ReflectionOutputExpressionVisitor(IDictionary<string, object> args)
		{
			this.args = args;
		}

		internal object GetResult(Expression expression)
		{
			this.Visit(expression);

			if (this.data.Count > 1)
			{
				throw new ArgumentException("The result stack contained too much elements.");
			}
			if (this.data.Count < 1)
			{
				throw new ArgumentException("The result stack contained too few elements.");
			}

			object value = this.GetValueFromStack();
			return value;
		}

		protected override Expression VisitParameter(ParameterExpression p)
		{
			Expression expression = base.VisitParameter(p);

			object argument = this.args[p.Name];
			this.data.Push(argument);
			
			return expression;
		}

		protected override Expression VisitMemberAccess(MemberExpression m)
		{
			// Call base.VisitMemberAccess(m) late for local variables special case.
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

				object value = propertyInfo.GetValue(this.GetValueFromStack(), null);
				this.data.Push(value);
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

		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression methodCallExpression = base.VisitMethodCall(m);

			object target = null;
			object[] parameterValues = this.GetValuesFromStack(m.Arguments.Count);		
	
			Expression expression = m.Object;
			if(expression != null)
			{
				target = this.GetValueFromStack();
			}

			// If expression is null the call is static, so the target must and will be null.
			MethodInfo methodInfo = m.Method;
			object value = methodInfo.Invoke(target, parameterValues);

			this.data.Push(value);
			
			return methodCallExpression;
		}

		protected override Expression VisitInvocation(InvocationExpression iv)
		{
			Expression expression = base.VisitInvocation(iv);

			object value = null;

			if(iv.Expression is MemberExpression)
			{
				// Use the delegate on the stack. The constant expression visitor pushed it there.
				value = this.GetValueFromStack();

				if (value is Delegate)
				{
					Delegate del = (Delegate)value;
					ParameterInfo[] parameterInfos = del.Method.GetParameters();
					object[] parameterValues = GetValuesFromStack(parameterInfos.Length);
					value = del.DynamicInvoke(parameterValues);
				}
			}

			this.data.Push(value);

			return expression;
		}

		protected override NewExpression VisitNew(NewExpression nex)
		{
			NewExpression newExpression = base.VisitNew(nex);

			ConstructorInfo constructorInfo = nex.Constructor;
			object[] parameterValues = this.GetValuesFromStack(nex.Arguments.Count);

			object value = constructorInfo.Invoke(parameterValues.ToArray());
			this.data.Push(value);

			return newExpression;
		}

		protected override Expression VisitBinary(BinaryExpression b)
		{
			Expression binaryExpression = base.VisitBinary(b);

			object value;

			object[] values = this.GetValuesFromStack(2);	

			MethodInfo methodInfo = b.Method;
			if(methodInfo != null)
			{
				// If an operator method is available use it.
				value = methodInfo.Invoke(null, values);
			}
			else
			{
				switch (b.NodeType)
				{
					case ExpressionType.Add:
						value = Convert.ToDouble(values.First()) + Convert.ToDouble(values.Last());
						break;
					case ExpressionType.AddChecked:
						value = checked(Convert.ToDouble(values.First()) + Convert.ToDouble(values.Last()));
						break;
					case ExpressionType.Subtract:
						value = Convert.ToDouble(values.First()) - Convert.ToDouble(values.Last());
						break;
					case ExpressionType.SubtractChecked:
						value = checked(Convert.ToDouble(values.First()) - Convert.ToDouble(values.Last()));
						break;
					case ExpressionType.Multiply:
						value = Convert.ToDouble(values.First()) * Convert.ToDouble(values.Last());
						break;
					case ExpressionType.MultiplyChecked:
						value = checked(Convert.ToDouble(values.First()) * Convert.ToDouble(values.Last()));
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
					case ExpressionType.ArrayIndex:
						object[] array = (object[])values.First();
						value = array[Convert.ToInt64(values.Last())];
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			Type type = b.Type;
			value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
			this.data.Push(value);
			
			return binaryExpression;
		}

		protected override Expression VisitTypeIs(TypeBinaryExpression b)
		{
			Expression expression = base.VisitTypeIs(b);

			object target = this.GetValueFromStack();
			Type isType = b.TypeOperand;

			bool value = isType.IsInstanceOfType(target);
			this.data.Push(value);

			return expression;
		}

		protected override Expression VisitUnary(UnaryExpression u)
		{
			Expression unaryExpression = base.VisitUnary(u);

			object value;

			object[] values = this.GetValuesFromStack(1);
			MethodInfo methodInfo = u.Method;
			if(methodInfo != null)
			{
				// If an operator method is available use it.
				value = methodInfo.Invoke(null, values);
			}
			else
			{
				switch (u.NodeType)
				{
					case ExpressionType.Negate:
						value = -Convert.ToDouble(values.First());
						break;
					case ExpressionType.NegateChecked:
						value = checked(-Convert.ToDouble(values.First()));
						break;
					case ExpressionType.Not:
						if (values.First() is bool)
						{
							value = !Convert.ToBoolean(values.First());
						}
						else
						{
							value = ~Convert.ToInt64(values.First());
						}
						break;
					//case ExpressionType.Quote:
					//case ExpressionType.Convert:
					//case ExpressionType.ConvertChecked:
					//case ExpressionType.ArrayLength:
					//case ExpressionType.TypeAs:
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			Type type = u.Operand.Type;
			value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
			this.data.Push(value);

			return unaryExpression;
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			object value;

			bool isCompilerGenerated = c.Type.IsCompilerGenerated();
			if (isCompilerGenerated) // Special case for local variables
			{
				string memberName = this.localVariableNames.Pop();
				MemberInfo memberInfo = c.Type.GetMember(memberName).First();

				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				value = fieldInfo.GetValue(c.Value);
			}
			else
			{
				value = c.Value;
			}

			this.data.Push(value);

			return base.VisitConstant(c);
		}

		protected override Expression VisitNewArray(NewArrayExpression na)
		{
			Expression expression = base.VisitNewArray(na);

			Array arrayValues = this.GetValuesFromStack(na.Expressions.Count);
			Type type = na.Type.GetElementType();

			Array array = Array.CreateInstance(type, arrayValues.Length);
			Array.Copy(arrayValues, array, arrayValues.Length);
			
			this.data.Push(array);

			return expression;
		}

		protected override Expression VisitMemberInit(MemberInitExpression init)
		{
			Expression expression = base.VisitMemberInit(init);

			// Step 1: Get all values for the initialization
			object[] values = this.GetValuesFromStack(init.Bindings.Count);

			// Set 2: Get target from stack
			object target = this.GetValueFromStack();

			// Set 3: Initialize the properties.
			for(int index = 0; index < init.Bindings.Count; index++)
			{
				MemberBinding binding = init.Bindings[index];
				MemberInfo memberInfo = binding.Member;
				if(memberInfo is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
					object value = values[index];
					propertyInfo.SetValue(target, value, null);
				}
			}

			// Set 4 : Put initialized instance back on the stack.
			this.data.Push(target);

			return expression;
		}

		protected override Expression VisitListInit(ListInitExpression init)
		{
			Expression expression = base.VisitListInit(init);

			// Set 1: Get all values for initialization
			int initializerArgumentCount = init.Initializers.First().Arguments.Count;
			int initializerCount = init.Initializers.Count;
			object[] values = this.GetValuesFromStack(initializerCount * initializerArgumentCount);

			// Set 2: Get target from stack
			object target = this.GetValueFromStack();

			// Set 3: Add the values
			for (int i = 0; i < initializerCount; i++)
			{
				ElementInit initializer = init.Initializers[i];

				object[] argumentValues = new object[initializerArgumentCount];
				for(int j = 0; j < initializerArgumentCount; j++)
				{
					int index = (i * initializerArgumentCount) + j;
					object arg = values[index];
					argumentValues[j] = arg;
				}

				MethodInfo methodInfo = initializer.AddMethod;
				methodInfo.Invoke(target, argumentValues);
			}

			// Set 4: Put target back on the stack
			this.data.Push(target);

			return expression;
		}

		private object[] GetValuesFromStack(int count)
		{
			IList<object> parameterValues = new List<object>();

			for (int i = 0; i < count; i++)
			{
				object parameterValue = this.GetValueFromStack();
				parameterValues.Add(parameterValue);
			}

			return parameterValues.Reverse().ToArray();
		}

		private object GetValueFromStack()
		{
			object parameterValue = this.data.Pop();
			return parameterValue;
		}
	}
}