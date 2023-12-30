using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    [Authorize(Policy = "ElevatedRights", Roles = "Admin,User")]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("categories")]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();

            return View(new CategoryModel
            {
                Categories = categories.Success ? categories.Data : new List<Category>(),
                Category = new Category(),
                Result = new ResultModel(),
            });
        }

        [Route("categories/add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    var data=_categoryService.Add(category);
                    var categories = _categoryService.GetAll();
                    ModelState.Clear();
                    return View("Index", new CategoryModel
                    {
                        Categories = categories.Success ? categories.Data : new List<Category>(),
                        Category = new Category(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message },
                    });
                }
                else
                {
                    var data =_categoryService.Update(category);
                    var categories = _categoryService.GetAll();
                    ModelState.Clear();
                    return View("Index", new CategoryModel
                    {
                        Categories = categories.Success ? categories.Data : new List<Category>(),
                        Category = new Category(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message },
                    });
                }
            }
            else
            {
                var categories = _categoryService.GetAll();
                ModelState.Clear();
                return View("Index", new CategoryModel
                {
                    Categories = categories.Success ? categories.Data : new List<Category>(),
                    Category = new Category(),
                    Result = new ResultModel(),
                });
            }
        }

        [Route("categories/{id}")]
        public IActionResult Index(int id)
        {
            var category = _categoryService.GetById(id);
            var categories = _categoryService.GetAll();
            if (category == null)
            {
                return Redirect("/categories");
            }
            return View(new CategoryModel
            {
                Categories = categories.Success ? categories.Data : new List<Category>(),
                Category = category.Success ? category.Data : new Category(),
                Result = new ResultModel(),
            });
        }

        [Route("categories/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);

            if (category.Success)
            {
                var data=_categoryService.Delete(category.Data);
                var categories = _categoryService.GetAll();

                CategoryModel model = new CategoryModel
                {
                    Categories = categories.Success ? categories.Data : new List<Category>(),
                    Category = new Category(),
                    Result = new ResultModel() { Error = data.Success, Message = data.Message },
                };
                return View("Index", model);
            }
            else
            {
                var categories = _categoryService.GetAll();

                return View("Index", new CategoryModel
                {
                    Categories = categories.Success ? categories.Data : new List<Category>(),
                    Category = new Category(),
                    Result = new ResultModel() { Error = category.Success, Message = category.Message },
                });
            }
        }
    }
}
