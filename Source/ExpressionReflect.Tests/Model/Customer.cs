namespace ExpressionReflect.Tests.Model
{
	public class Customer
	{
		public const int AgeConstant = 33;

		private readonly int calculationValue;

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

		public override string ToString()
		{
			return this.Firstname + " " + this.Lastname;
		}
	}
}
