namespace ExpressionReflect.Tests.Model
{
	public class NameIndex
	{
		private readonly string[] names;

		public NameIndex(params string[] names)
		{
			this.names = names;
		}

		public int this[string name]
		{
			get { return name.IndexOf(name); }
		}
	}
}