//using ItemManagementSystem.Data;
//using ItemManagementSystem.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace ItemManagementSystem.Controllers
//{
//    public class ItemController : Controller
//    {
//        private readonly ApplicationDbContext _db;
//        private const int PageSize = 5;

//        public ItemController(ApplicationDbContext db)
//        {
//            _db = db;
//        }

//        // ITEM LIST
//        public IActionResult Index(int page = 1, string search = "", string sortOrder = "name_asc")
//        {
//            if (HttpContext.Session.GetInt32("UserId") == null)
//                return RedirectToAction("Login", "Account");

//            var items = _db.Items.AsQueryable();

//            if (!string.IsNullOrEmpty(search))
//                items = items.Where(i => i.Name.Contains(search));

//            items = sortOrder switch
//            {
//                "name_desc" => items.OrderByDescending(i => i.Name),
//                "price_asc" => items.OrderBy(i => i.Price),
//                "price_desc" => items.OrderByDescending(i => i.Price),
//                _ => items.OrderBy(i => i.Name)
//            };

//            int totalItems = items.Count();
//            int totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
//            var pagedItems = items.Skip((page - 1) * PageSize).Take(PageSize).ToList();

//            ViewBag.CurrentPage = page;
//            ViewBag.TotalPages = totalPages;
//            ViewBag.Search = search;
//            ViewBag.SortOrder = sortOrder;
//            ViewBag.Role = HttpContext.Session.GetString("Role");

//            return View(pagedItems);
//        }

//        // CREATE ITEM
//        public IActionResult Create()
//        {
//            if (HttpContext.Session.GetInt32("UserId") == null)
//                return RedirectToAction("Login", "Account");
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Item item)
//        {
//            if (HttpContext.Session.GetInt32("UserId") == null)
//                return RedirectToAction("Login", "Account");

//            if (!ModelState.IsValid) return View(item);

//            item.CreatedDate = DateTime.Now;
//            _db.Items.Add(item);
//            _db.SaveChanges();

//            TempData["Success"] = "Item added successfully!";
//            return RedirectToAction("Index"); // Redirect to Item List
//        }

//        // EDIT ITEM
//        public IActionResult Edit(int id)
//        {
//            if (HttpContext.Session.GetInt32("UserId") == null)
//                return RedirectToAction("Login", "Account");

//            var item = _db.Items.Find(id);
//            if (item == null) return NotFound();

//            return View(item);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(Item item)
//        {
//            if (HttpContext.Session.GetInt32("UserId") == null)
//                return RedirectToAction("Login", "Account");

//            if (!ModelState.IsValid) return View(item);

//            _db.Items.Update(item);
//            _db.SaveChanges();

//            TempData["Success"] = "Item updated successfully!";
//            return RedirectToAction("Index"); // Redirect to Item List
//        }

//        // ITEM DETAILS
//        public IActionResult Details(int id)
//        {
//            if (HttpContext.Session.GetInt32("UserId") == null)
//                return RedirectToAction("Login", "Account");

//            var item = _db.Items.Find(id);
//            if (item == null) return NotFound();

//            return View(item);
//        }

//        // DELETE ITEM
//        public IActionResult Delete(int id)
//        {
//            if (HttpContext.Session.GetInt32("UserId") == null)
//                return RedirectToAction("Login", "Account");

//            var item = _db.Items.Find(id);
//            if (item == null) return NotFound();

//            return View(item);
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            if (HttpContext.Session.GetInt32("UserId") == null)
//                return RedirectToAction("Login", "Account");

//            var item = _db.Items.Find(id);
//            if (item != null)
//            {
//                _db.Items.Remove(item);
//                _db.SaveChanges();
//                TempData["Success"] = "Item deleted successfully!";
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}



using ItemManagementSystem.Data;
using ItemManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemManagementSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const int PageSize = 5;

        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int page = 1, string search = "", string sortOrder = "name_asc")
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");

            var items = _db.Items.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                items = items.Where(i => i.Name.Contains(search));

            items = sortOrder switch
            {
                "name_desc" => items.OrderByDescending(i => i.Name),
                "price_asc" => items.OrderBy(i => i.Price),
                "price_desc" => items.OrderByDescending(i => i.Price),
                _ => items.OrderBy(i => i.Name)
            };

            int totalItems = items.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            var pagedItems = items.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;
            ViewBag.Role = HttpContext.Session.GetString("Role");

            return View(pagedItems);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");

            if (!ModelState.IsValid) return View(item);

            item.CreatedDate = DateTime.Now;
            _db.Items.Add(item);
            _db.SaveChanges();

            TempData["Success"] = "Item added successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");

            var item = _db.Items.Find(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");

            if (!ModelState.IsValid) return View(item);

            _db.Items.Update(item);
            _db.SaveChanges();

            TempData["Success"] = "Item updated successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");

            var item = _db.Items.Find(id);
            if (item == null) return NotFound();

            return View(item);
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");

            var item = _db.Items.Find(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "User");

            var item = _db.Items.Find(id);
            if (item == null) return NotFound();

            _db.Items.Remove(item);
            _db.SaveChanges();

            TempData["Success"] = "Item deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
