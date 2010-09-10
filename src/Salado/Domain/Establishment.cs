using System.Windows;

namespace Domain
{
	public class Establishment
	{
		public string Name { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public Point Location { get; set; }

		public Establishment(string name) : this()
		{
			Name = name;
		}

		public Establishment() { }
	}
}
