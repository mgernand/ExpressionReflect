namespace ExpressionReflect
{
	using System;
	using System.Linq.Expressions;

	public static class ActionExtensions
	{
		public static Action Reflect(this Expression<Action> target)
		{
			Action action = () => target.Execute();
			return action;
		}

		public static Action<T> Reflect<T>(this Expression<Action<T>> target)
		{
			Action<T> action = a => target.Execute(a);
			return action;
		}

		public static Action<T1, T2> Reflect<T1, T2>(this Expression<Action<T1, T2>> target)
		{
			Action<T1, T2> action = (a, b) => target.Execute(a, b);
			return action;
		}

		public static Action<T1, T2, T3> Reflect<T1, T2, T3>(this Expression<Action<T1, T2, T3>> target)
		{
			Action<T1, T2, T3> action = (a, b, c) => target.Execute(a, b, c);
			return action;
		}

		public static Action<T1, T2, T3, T4> Reflect<T1, T2, T3, T4>(this Expression<Action<T1, T2, T3, T4>> target)
		{
			Action<T1, T2, T3, T4> action = (a, b, c, d) => target.Execute(a, b, c, d);
			return action;
		}

		public static Action<T1, T2, T3, T4, T5> Reflect<T1, T2, T3, T4, T5>(this Expression<Action<T1, T2, T3, T4, T5>> target)
		{
			Action<T1, T2, T3, T4, T5> action = (a, b, c, d, e) => target.Execute(a, b, c, d, e);
			return action;
		}

		public static Action<T1, T2, T3, T4, T5, T6> Reflect<T1, T2, T3, T4, T5, T6>(this Expression<Action<T1, T2, T3, T4, T5, T6>> target)
		{
			Action<T1, T2, T3, T4, T5, T6> action = (a, b, c, d, e, f) => target.Execute(a, b, c, d, e, f);
			return action;
		}

		public static Action<T1, T2, T3, T4, T5, T6, T7> Reflect<T1, T2, T3, T4, T5, T6, T7>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7> action = (a, b, c, d, e, f, g) => target.Execute(a, b, c, d, e, f, g);
			return action;
		}

		public static Action<T1, T2, T3, T4, T5, T6, T7, T8> Reflect<T1, T2, T3, T4, T5, T6, T7, T8>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8> action = (a, b, c, d, e, f, g, h) => target.Execute(a, b, c, d, e, f, g, h);
			return action;
		}
	}
}