using ASP_MVC.Data;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            IEnumerable<Item> itemsList = _db.Items.Include(c => c.Category).ToList();
            return View(itemsList);
        }

        //GET
        public IActionResult New()
        {
            createSelectList();
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
                TempData["successData"] = "Item has been added new successfully";
                return RedirectToAction("Index");
            }
           else { return View(item); }
        }

        public void createSelectList(int selectId = 1)
        {
            //List<Category> categories = new List<Category>
            //{
            //    new Category() {Id = 0, Name = "Select Category"},
            //    new Category() {Id = 1, Name = "Computers"},
            //    new Category() {Id = 2, Name = "Mobiles"},
            //    new Category() {Id = 3, Name = "Electric machines"},
            //};

            List<Category> categories = _db.Categories.ToList();

            SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            ViewBag.CategoryList = listItems;
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
            createSelectList(item.CategoryId);
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
                TempData["successData"] = "Item has been Updated  successfully";
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
            createSelectList(item.CategoryId);
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
            TempData["successData"] = "Item has been delete  successfully";
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
