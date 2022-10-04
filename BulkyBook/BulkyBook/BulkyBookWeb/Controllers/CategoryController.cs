using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
    
        public IActionResult Index()
        {
            IEnumerable<Models.Category> CategoryList = _db.Categories.ToList(); 
            return View(CategoryList);
        }

        //get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            // this is a custom check for validation, suppose we do not allow identical name and displayorder values
            if(category.Name==category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and DisplayOrder cannot be identical!");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(category); _db.SaveChanges();
                TempData["CategoryMessage"] = "Category created successfully.";
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        //get
        [HttpGet]
        public IActionResult Edit(int? ID)
        {
            if(ID==null || ID==0) { return NotFound(); }
            Category category = _db.Categories.Find(ID);
            // throws exception if more than one records found
            //Category category = _db.Categories.SingleOrDefault(c=>c.ID==ID);
            // will not throw exception if multiple records exist; instead returns the first
            //Category category = _db.Categories.FirstOrDefault(c=>c.ID==ID);
            if(category==null) { return NotFound(); }
            return View(category);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            // this is a custom check for validation, suppose we do not allow identical name and displayorder values
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and DisplayOrder cannot be identical!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category); _db.SaveChanges();
                TempData["CategoryMessage"] = "Category edited successfully.";
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        //get
        [HttpGet]
        public IActionResult Delete(int? ID)
        {
            if (ID == null || ID == 0) { return NotFound(); }
            Category category = _db.Categories.Find(ID);
            if (category == null) { return NotFound(); }
            return View(category);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            _db.Categories.Remove(category); _db.SaveChanges();
            TempData["CategoryMessage"] = "Category deleted successfully.";
            return RedirectToAction("Index", "Category");

        }
    }
}
