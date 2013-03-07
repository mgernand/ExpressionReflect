namespace ExpressionReflect.Tests.Model
{
	public static class CustomerExtensions
	{
		public static int GetDefaultAgeEx(this Customer customer, int value)
		{
			return Customer.AgeConstant + value;
		}

		public static int GetDefaultAgeEx(this Customer customer)
		{
			return customer.Age + Customer.AgeConstant;
		}
	}
}