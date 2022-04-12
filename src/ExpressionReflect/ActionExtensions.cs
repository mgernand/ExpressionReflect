namespace ExpressionReflect
{
	using System;
	using System.Linq.Expressions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="Expression{TDelegate}" /> type.
	/// </summary>
	[PublicAPI]
	public static class ActionExtensions
	{
		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action Reflect(this Expression<Action> target)
		{
			Action action = () => target.Execute();
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T> Reflect<T>(this Expression<Action<T>> target)
		{
			Action<T> action = a => target.Execute(a);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2> Reflect<T1, T2>(this Expression<Action<T1, T2>> target)
		{
			Action<T1, T2> action = (a, b) => target.Execute(a, b);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3> Reflect<T1, T2, T3>(this Expression<Action<T1, T2, T3>> target)
		{
			Action<T1, T2, T3> action = (a, b, c) => target.Execute(a, b, c);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4> Reflect<T1, T2, T3, T4>(this Expression<Action<T1, T2, T3, T4>> target)
		{
			Action<T1, T2, T3, T4> action = (a, b, c, d) => target.Execute(a, b, c, d);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5> Reflect<T1, T2, T3, T4, T5>(this Expression<Action<T1, T2, T3, T4, T5>> target)
		{
			Action<T1, T2, T3, T4, T5> action = (a, b, c, d, e) => target.Execute(a, b, c, d, e);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6> Reflect<T1, T2, T3, T4, T5, T6>(this Expression<Action<T1, T2, T3, T4, T5, T6>> target)
		{
			Action<T1, T2, T3, T4, T5, T6> action = (a, b, c, d, e, f) => target.Execute(a, b, c, d, e, f);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7> Reflect<T1, T2, T3, T4, T5, T6, T7>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7> action = (a, b, c, d, e, f, g) => target.Execute(a, b, c, d, e, f, g);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8> Reflect<T1, T2, T3, T4, T5, T6, T7, T8>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8> action = (a, b, c, d, e, f, g, h) => target.Execute(a, b, c, d, e, f, g, h);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <typeparam name="T9"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action = (a, b, c, d, e, f, g, h, i) => target.Execute(a, b, c, d, e, f, g, h, i);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <typeparam name="T9"></typeparam>
		/// <typeparam name="T10"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action = (a, b, c, d, e, f, g, h, i, j) => target.Execute(a, b, c, d, e, f, g, h, i, j);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <typeparam name="T9"></typeparam>
		/// <typeparam name="T10"></typeparam>
		/// <typeparam name="T11"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action = (a, b, c, d, e, f, g, h, i, j, k) => target.Execute(a, b, c, d, e, f, g, h, i, j, k);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <typeparam name="T9"></typeparam>
		/// <typeparam name="T10"></typeparam>
		/// <typeparam name="T11"></typeparam>
		/// <typeparam name="T12"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action = (a, b, c, d, e, f, g, h, i, j, k, l) => target.Execute(a, b, c, d, e, f, g, h, i, j, k, l);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <typeparam name="T9"></typeparam>
		/// <typeparam name="T10"></typeparam>
		/// <typeparam name="T11"></typeparam>
		/// <typeparam name="T12"></typeparam>
		/// <typeparam name="T13"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action = (a, b, c, d, e, f, g, h, i, j, k, l, m) => target.Execute(a, b, c, d, e, f, g, h, i, j, k, l, m);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <typeparam name="T9"></typeparam>
		/// <typeparam name="T10"></typeparam>
		/// <typeparam name="T11"></typeparam>
		/// <typeparam name="T12"></typeparam>
		/// <typeparam name="T13"></typeparam>
		/// <typeparam name="T14"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action = (a, b, c, d, e, f, g, h, i, j, k, l, m, n) => target.Execute(a, b, c, d, e, f, g, h, i, j, k, l, m, n);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <typeparam name="T9"></typeparam>
		/// <typeparam name="T10"></typeparam>
		/// <typeparam name="T11"></typeparam>
		/// <typeparam name="T12"></typeparam>
		/// <typeparam name="T13"></typeparam>
		/// <typeparam name="T14"></typeparam>
		/// <typeparam name="T15"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action = (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o) => target.Execute(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o);
			return action;
		}

		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <typeparam name="T3"></typeparam>
		/// <typeparam name="T4"></typeparam>
		/// <typeparam name="T5"></typeparam>
		/// <typeparam name="T6"></typeparam>
		/// <typeparam name="T7"></typeparam>
		/// <typeparam name="T8"></typeparam>
		/// <typeparam name="T9"></typeparam>
		/// <typeparam name="T10"></typeparam>
		/// <typeparam name="T11"></typeparam>
		/// <typeparam name="T12"></typeparam>
		/// <typeparam name="T13"></typeparam>
		/// <typeparam name="T14"></typeparam>
		/// <typeparam name="T15"></typeparam>
		/// <typeparam name="T16"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Reflect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>> target)
		{
			Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action = (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p) => target.Execute(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p);
			return action;
		}
	}
}
