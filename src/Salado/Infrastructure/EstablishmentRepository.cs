using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web;
using System.Windows;
using Core.Domain;
using Domain;

namespace Infrastructure
{
	public class EstablishmentRepository : IEstablishmentRepository
	{
		IEnumerable<Establishment> IEstablishmentRepository.GetAll()
		{
			using (var connection = new SQLiteConnection("Data Source=" + HttpContext.Current.Server.MapPath("~/Data/Salado.db3")))
			{
				connection.Open();
				var command = new SQLiteCommand("select * from Establishments", connection) { CommandType = CommandType.Text };
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					string name = reader.GetString(reader.GetOrdinal("Name"));
					var location = new Point
						{
							X = reader.GetDouble(reader.GetOrdinal("LocationX")),
							Y = reader.GetDouble(reader.GetOrdinal("LocationY"))
						};

					yield return new Establishment(name, location);
				}
				
				connection.Close();
			}
		}
	}
}
