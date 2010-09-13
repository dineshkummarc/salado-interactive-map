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

		public MapViewModel(Point mapSize, IEnumerable<Establishment> establishments, Point minGps, Point maxGps, double scale)
		{
			MapSize = mapSize;
			Establishments = establishments.Select(establishment => new EstablishmentViewModel(mapSize, establishment, minGps, maxGps, scale));
		}
	}

	public class EstablishmentViewModel
	{
		public string Name { get; private set; }
		public double X { get; private set; }
		public double Y { get; private set; }
		public int Size { get { return 16; } }

		public EstablishmentViewModel(Point mapSize, Establishment establishment, Point minGps, Point maxGps, double scale)
		{
			Name = establishment.Name;

			X = ((establishment.Location.X - minGps.X) / (maxGps.X - minGps.X)) * mapSize.X - Size / 2;
			Y = ((establishment.Location.Y - minGps.Y) / (maxGps.Y - minGps.Y)) * mapSize.Y - Size / 2;
		}
	}
}
