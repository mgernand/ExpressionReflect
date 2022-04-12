namespace ExpressionReflect.Tests.Model
{
	public struct CustomStruct
	{
		private readonly int value;

		public CustomStruct(int value)
		{
			this.value = value;
		}

		public int Value
		{
			get { return this.value; }
		}
	}
}