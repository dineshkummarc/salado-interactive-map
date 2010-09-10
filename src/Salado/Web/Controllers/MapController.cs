using System.Web.Mvc;
using Core.Domain;
using Infrastructure;

namespace Web.Controllers
{
	public class MapController : Controller
	{
		public JsonResult Shops()
		{
			IEstablishmentRepository repository = new EstablishmentRepository();
			return JsonGet(repository.GetAll());
		}

		private JsonResult JsonGet(object data)
		{
			return Json(data, JsonRequestBehavior.AllowGet);
		}
	}
}
