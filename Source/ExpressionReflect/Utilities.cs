namespace ExpressionReflect
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	internal static class Utilities
	{
		public static string ToConcatenatedString<T>(this IEnumerable<T> source, Func<T, string> selector, string separator)
		{
			StringBuilder sb = new StringBuilder();
			bool needSeparator = false;

			foreach(T item in source)
			{
				if(needSeparator)
				{
					sb.Append(separator);
				}

				sb.Append(selector(item));
				needSeparator = true;
			}

			return sb.ToString();
		}

		public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> source)
		{
			return new LinkedList<T>(source);
		}
	}
}