using System.Web.Mvc;

namespace Web.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
