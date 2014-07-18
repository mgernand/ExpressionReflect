namespace ExpressionReflect.Tests.Model
{
	using System.Diagnostics;

	[DebuggerDisplay("Customer: {ToString()}")]
	public class Customer
	{
		public const int AgeConstant = 33;

	    public static string StaticField = "StaticField";

	    public static string StaticProperty
	    {
	        get
	        {
	            return "StaticProperty";
	        }
	    }

		private readonly int calculationValue;
		public string Field;

		public Customer()
		{

		}

		public Customer(int calculationValue)
		{
			this.calculationValue = calculationValue;
		}

		public Customer(string firstname, string lastname)
		{
			this.Firstname = firstname;
			this.Lastname = lastname;
		}

		public Customer(Customer toCopy)
		{
			this.calculationValue = toCopy.CalculationValue;
			this.Firstname = toCopy.Firstname;
			this.Lastname = toCopy.Lastname;
		}

		public Customer(string firstname, Customer customer, int amount, string lastname)
		{
			this.Firstname = firstname;
			this.Lastname = lastname;
			this.calculationValue = customer.CalculateAge() + amount;
		}

		public string Firstname { get; set; }
		public string Lastname { get; set; }

		public int Age 
		{
			get { return this.CalculateAge(); }
		}

		public int Value
		{
			get { return 10; }
		}

		public int CalculationValue
		{
			get { return this.calculationValue; }
		}

		public NameIndex NameIndex
		{
			get { return new NameIndex(this.Firstname, this.Lastname); }
		}

		public string[] Names
		{
			get { return new string[] { this.Firstname, this.Lastname };}
		}

		public bool IsPremium { get; set; }

		public object Object
		{
			get { return new object(); }
		}

		public int CalculateAge()
		{
			return AgeConstant;
		}

		public int CalculateLength(string str)
		{
			return str.Length;
		}

		public int Calculate(int increment)
		{
			return this.CalculateAge() + increment;
		}

		public int Calculate(Customer customer)
		{
			return this.CalculateAge() + customer.CalculationValue;
		}

		public override string ToString()
		{
			return this.Firstname + " " + this.Lastname;
		}

		public static int GetDefaultAge()
		{
			return AgeConstant;
		}

		public static int GetDefaultAge(int value)
		{
			return AgeConstant + value;
		}

		public static int GetDefaultAge(Customer customer)
		{
			return customer.Age + AgeConstant;
		}

		public int CalculateLength(string str, Customer customer, int value)
		{
			return str.Length + customer.CalculationValue + value;
		}

		public int this[int amount]
		{
			get { return this.Age + amount; }
		}

		public int this[int amount, int amount2]
		{
			get { return this[amount] + amount2; }
		}
	}
}
