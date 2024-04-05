using ASP_MVC.Data;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class ItemsController : Controller
    {

        private readonly AppDbContext _db;

        public ItemsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Item> itemsList = _db.Items.ToList();
            return View(itemsList);
        }
    }
}
