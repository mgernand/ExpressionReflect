namespace ExpressionReflect
{
	using System;
	using System.Linq.Expressions;

	public static class ActionExtensions
	{
		public static Action Reflect(this Expression<Action> target)
		{
			Action action = () => ExpressionReflector.Execute(target);
			return action;
		}

		public static Action<T> Reflect<T>(this Expression<Action<T>> target)
		{
			Action<T> action = a => ExpressionReflector.Execute(target, a);
			return action;
		}

		public static Action<T1, T2> Reflect<T1, T2>(this Expression<Action<T1, T2>> target)
		{
			Action<T1, T2> action = (a, b) => ExpressionReflector.Execute(target, a, b);
			return action;
		}

		public static Action<T1, T2, T3> Reflect<T1, T2, T3>(this Expression<Action<T1, T2, T3>> target)
		{
			Action<T1, T2, T3> action = (a, b, c) => ExpressionReflector.Execute(target, a, b, c);
			return action;
		}

		public static Action<T1, T2, T3, T4> Reflect<T1, T2, T3, T4>(this Expression<Action<T1, T2, T3, T4>> target)
		{
			Action<T1, T2, T3, T4> action = (a, b, c, d) => ExpressionReflector.Execute(target, a, b, c, d);
			return action;
		}
	}
}