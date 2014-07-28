namespace ExpressionReflect
{
	using System;
	using System.Linq.Expressions;
	using JetBrains.Annotations;

	public static class PredicateExtensions
	{
		[PublicAPI]
		public static Predicate<T> Reflect<T>(this Expression<Predicate<T>> target)
		{
			Predicate<T> predicate = x => (bool)target.Execute(x);
			return predicate;
		}
	}
}