namespace ExpressionReflect
{
	using System;

	public class ExpressionEvaluationException : Exception
	{		
		public ExpressionEvaluationException()
		{
		}

		public ExpressionEvaluationException(string message) : base(message)
		{
		}

		public ExpressionEvaluationException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}