namespace Domain
{
	public class Establishment
	{
		public string Name { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string Category { get; set; }

		public Establishment(string name) : this()
		{
			Name = name;
		}

		public Establishment() { }
	}
}
