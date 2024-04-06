using ASP_MVC.Data;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        //GET
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {

            if(item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can not equal 100");
            }
           if(ModelState.IsValid)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
           else { return View(item); }
        }

        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound(); 
            }
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {

            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can not equal 100");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(item); }
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult DeleteItem(int? Id)
        {
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
        
                _db.Items.Remove(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
          
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(Item item)
        //{


        //    _db.Items.Remove(item);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");

        //}
    }
}
