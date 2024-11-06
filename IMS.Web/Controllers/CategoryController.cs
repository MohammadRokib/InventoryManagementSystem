using IMS.Web.Data;
using IMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace IMS.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category>? objCategoryList = new List<Category>();

            try
            {
                objCategoryList = _db.Categories.ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occured during connecting to the database: {ex}");
            }
            
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Categories.Add(obj);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
					Console.WriteLine($"An error occured during connecting to the database: {ex}");
					return View(obj);
				}
                TempData["success"] = $"Category {obj.CategoryName} created successfully";
				return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryObj = new Category();
            try
            {
                categoryObj = _db.Categories.Find(id);
                // categoryObj = _db.Categories.FirstOrDefault(u => u.CategoryID == id);
            }
            catch(Exception ex)
            {
				Console.WriteLine($"An error occured during connecting to the database: {ex}");
			}

            if (categoryObj == null)
            {
                return NotFound();
            }
            return View(categoryObj);
        }

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_db.Categories.Update(obj);
					_db.SaveChanges();
				}
				catch (Exception ex)
				{
					Console.WriteLine($"An error occured during connecting to the database: {ex}");
					return View(obj);
				}
                TempData["success"] = $"Category {obj.CategoryName} updated successfully";
				return RedirectToAction("Index", "Category");
			}
			return View();
		}

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryObj = new Category();
            try
            {
                categoryObj = _db.Categories.Find(id);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occured during connecting to the database: ${ex}");
            }

            if (categoryObj == null)
            {
                return NotFound();
            }

            return View(categoryObj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryObj = new Category();
            try
            {
                categoryObj = _db.Categories.Find(id);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occured during conncting to the database: ${ex}");
            }

            try
            {
                _db.Categories.Remove(categoryObj);
				_db.SaveChanges();
			}
            catch(Exception ex)
            {
                Console.WriteLine($"Something went wrong while deleting the record: {ex}");
            }

            TempData["success"] = $"Category {categoryObj.CategoryName} deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
