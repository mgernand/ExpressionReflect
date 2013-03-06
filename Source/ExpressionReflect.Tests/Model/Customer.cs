namespace ExpressionReflect.Tests.Model
{
	public class Customer
	{
		public const int Age = 33;

		public Customer(string firstname, string lastname)
		{
			this.Firstname = firstname;
			this.Lastname = lastname;
		}

		public string Firstname { get; set; }
		public string Lastname { get; set; }

		public int CalculateAge()
		{
			return Age;
		}
	}
}
