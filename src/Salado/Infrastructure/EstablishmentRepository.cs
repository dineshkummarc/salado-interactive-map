using System.Collections.Generic;
using System.Windows;
using Core.Domain;
using Domain;

namespace Infrastructure
{
	public class EstablishmentRepository : IEstablishmentRepository
	{
		IEnumerable<Establishment> IEstablishmentRepository.GetAll()
		{
			yield return new Establishment("Salado Wine Seller") { Location = new Point(30.954196, -97.532920) };
			yield return new Establishment("Fletcher's Books") { Location = new Point(30.955571, -97.533714) };
			yield return new Establishment("Brookshire Brothers Grocery") { Location = new Point(30.956128, -97.532061) };
			yield return new Establishment("Gregory's") { Location = new Point(30.942554, -97.537104) };
			yield return new Establishment("TBC International, Inc.") { Location = new Point(30.95126, -97.55396) };
			yield return new Establishment("The Range") { Location = new Point(30.947681, -97.535430) };
		}
	}
}
