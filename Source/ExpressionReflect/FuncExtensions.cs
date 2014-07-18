namespace ExpressionReflect
{
	using System;
	using System.Linq.Expressions;

	public static class FuncExtensions
	{
		public static Func<TResult> Reflect<TResult>(this Expression<Func<TResult>> target)
		{
			Func<TResult> func = () => (TResult)target.Execute();
			return func;
		}

		public static Func<T, TResult> Reflect<T, TResult>(this Expression<Func<T, TResult>> target)
		{
			Func<T, TResult> func = a => (TResult)target.Execute(a);
			return func;
		}

		public static Func<T1, T2, TResult> Reflect<T1, T2, TResult>(this Expression<Func<T1, T2, TResult>> target)
		{
			Func<T1, T2, TResult> func = (a, b) => (TResult)target.Execute(a, b);
			return func;
		}

		public static Func<T1, T2, T3, TResult> Reflect<T1, T2, T3, TResult>(this Expression<Func<T1, T2, T3, TResult>> target)
		{
			Func<T1, T2, T3, TResult> func = (a, b, c) => (TResult)target.Execute(a, b, c);
			return func;
		}

		public static Func<T1, T2, T3, T4, TResult> Reflect<T1, T2, T3, T4, TResult>(this Expression<Func<T1, T2, T3, T4, TResult>> target)
		{
			Func<T1, T2, T3, T4, TResult> func = (a, b, c, d) => (TResult)target.Execute(a, b, c, d);
			return func;
		}

		public static Func<T1, T2, T3, T4, T5, TResult> Reflect<T1, T2, T3, T4, T5, TResult>(this Expression<Func<T1, T2, T3, T4, T5, TResult>> target)
		{
			Func<T1, T2, T3, T4, T5, TResult> func = (a, b, c, d, e) => (TResult)target.Execute(a, b, c, d, e);
			return func;
		}

		public static Func<T1, T2, T3, T4, T5, T6, TResult> Reflect<T1, T2, T3, T4, T5, T6, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> target)
		{
			Func<T1, T2, T3, T4, T5, T6, TResult> func = (a, b, c, d, e, f) => (TResult)target.Execute(a, b, c, d, e, f);
			return func;
		}

		public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Reflect<T1, T2, T3, T4, T5, T6, T7, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> target)
		{
			Func<T1, T2, T3, T4, T5, T6, T7, TResult> func = (a, b, c, d, e, f, g) => (TResult)target.Execute(a, b, c, d, e, f, g);
			return func;
		}

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> target)
		{
			Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func = (a, b, c, d, e, f, g, h) => (TResult)target.Execute(a, b, c, d, e, f, g, h);
			return func;
		}
	}
}