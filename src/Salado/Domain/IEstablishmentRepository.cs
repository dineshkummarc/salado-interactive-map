using System.Collections.Generic;
using Domain;

namespace Core.Domain
{
	public interface IEstablishmentRepository
	{
		IEnumerable<Establishment> GetAll();
	}
}