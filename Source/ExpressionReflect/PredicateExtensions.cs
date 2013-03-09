namespace ExpressionReflect
{
	using System;
	using System.Linq.Expressions;

	public static class PredicateExtensions
	{
		public static Predicate<T> Reflect<T>(this Expression<Predicate<T>> target)
		{
			Predicate<T> predicate = x => (bool)ExpressionReflector.Execute(target, x);
			return predicate;
		}
	}
}