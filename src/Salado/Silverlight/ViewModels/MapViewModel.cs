using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Domain;

namespace Silverlight.ViewModels
{
	public class MapViewModel
	{
		public IEnumerable<EstablishmentViewModel> Establishments { get; set; }

		public MapViewModel(IEnumerable<Establishment> establishments, Point zero, double scale)
		{
			Establishments = establishments.Select(establishment => new EstablishmentViewModel(establishment, zero, scale));
		}
	}

	public class EstablishmentViewModel
	{
		public string Name { get; private set; }
		public double X { get; private set; }
		public double Y { get; private set; }

		public EstablishmentViewModel(Establishment establishment, Point zero, double scale)
		{
			Name = establishment.Name;
			X = -(establishment.Location.X - zero.X) * scale + 400;
			Y = -(establishment.Location.Y - zero.Y) * scale + 300;
		}
	}
}