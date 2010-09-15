using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Domain;

namespace Silverlight.ViewModels
{
	public class MapViewModel
	{
		public IEnumerable<EstablishmentViewModel> Establishments { get; set; }
		public Point MapSize { get; private set; }

		public MapViewModel(Point mapSize, IEnumerable<Establishment> establishments, Point minGps, Point maxGps)
		{
			MapSize = mapSize;
			Establishments = establishments.Select(establishment => new EstablishmentViewModel(mapSize, establishment, minGps, maxGps));
		}
	}

	public class EstablishmentViewModel
	{
		public string Name { get; private set; }
		public double X { get; private set; }
		public double Y { get; private set; }
		public double Size { get { return 8; } }
		public string Category { get; private set; }

		public string Color { get { return GetColorForCategory(Category); } }

		public EstablishmentViewModel(Point mapSize, Establishment establishment, Point minGps, Point maxGps)
		{
			Name = establishment.Name;

			X = ((establishment.Latitude - minGps.X) / (maxGps.X - minGps.X)) * mapSize.X - Size / 2;
			Y = ((establishment.Longitude - minGps.Y) / (maxGps.Y - minGps.Y)) * mapSize.Y - Size / 2;

			Category = establishment.Category;
		}

		private static string GetColorForCategory(string category)
		{
			return "#ff00ff";

			IDictionary<string, string> categoryColors = new Dictionary<string, string>
				{
					{ "Restaurant", "#ff00ff" }
				};

			string color;
			bool success = categoryColors.TryGetValue(category, out color);
			if (!success) color = "#ffffff";

			return color;
		}
	}
}
