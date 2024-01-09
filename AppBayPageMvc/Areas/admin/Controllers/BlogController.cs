using AppBayPageMvc.DB;
using AppBayPageMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppBayPageMvc.Areas.admin.Controllers
{
    [Area("admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var item=_dbContext.blogs.ToList();
            return View(item);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            _dbContext.blogs.Add(blog);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var item = _dbContext.blogs.Find(id);
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Blog item = _dbContext.blogs.Find(id);
            return View(item);
        }
        [HttpPost]
        public IActionResult Update(Blog blog)
        {
            _dbContext.Update(blog);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
