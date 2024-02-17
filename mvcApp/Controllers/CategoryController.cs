using Microsoft.AspNetCore.Mvc;
using mvcApp.Database;
using mvcApp.Models;

namespace mvcApp.Controllers
{
    public class CategoryController : Controller
    {
        //using dependency injection there's no need to create object of AppDbContext
        private readonly AppDbContext _dbContext;
        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> Categorylist_Obj = _dbContext.Category;
            if(Categorylist_Obj.Count() > 0)
            {
                return View(Categorylist_Obj);

            }
            return View();
        }
        // Get add category page
        public IActionResult AddCategory()
        {
            //create a view to display form to add cat name ..etc
            return View();
        }
        // Post a new category
       [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Category cat)
        {
            //custom errors
            if(cat.Name == null)
            {
                ModelState.AddModelError("Name", "name is required");
            }
            if( cat.DisplayOrder != null && Convert.ToInt32(cat.DisplayOrder) >= 0)
            {
                ModelState.AddModelError("DisplayOrder", "display order must be a number ");
                
            }
            if (cat.Name == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "display order and name value must not match ");

            }
            if (!ModelState.IsValid)
            {
                return View(cat);
            }
            _dbContext.Category.Add(cat);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult EditCategory()
        {
            //create a view to display form to add cat name ..etc
            return View();
        }

        public IActionResult DeleteCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var cat = _dbContext.Category.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int? id)
        {

        
            var cat = _dbContext.Category.Find(id);
            if(cat == null)
            {

                return NotFound();

            }
            _dbContext.Category.Remove(cat);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
