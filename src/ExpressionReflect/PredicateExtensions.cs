namespace ExpressionReflect
{
	using System;
	using System.Linq.Expressions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="Expression{TDelegate}" /> type.
	/// </summary>
	[PublicAPI]
	public static class PredicateExtensions
	{
		/// <summary>
		///     Reflects and executes the given expression.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Predicate<T> Reflect<T>(this Expression<Predicate<T>> target)
		{
			Predicate<T> predicate = x => (bool)target.Execute(x);
			return predicate;
		}
	}
}
