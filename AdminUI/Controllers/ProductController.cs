using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete.Dtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace AdminUI.Controllers
{
    [Authorize(Policy = "ElevatedRights", Roles = "Admin,User")]
    public class ProductController : Controller
    {
        IProductService _productService;
        IBrandService _brandService;
        IColorService _colorService;
        ICategoryService _categoryService;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ProductController(IProductService productService, IBrandService brandService, IColorService colorService, ICategoryService categoryService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _productService = productService;
            _brandService = brandService;
            _colorService = colorService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [Route("products")]
        public IActionResult Index()
        {
            var productDtos = _productService.GetAll();
            var brands = _brandService.GetAll();
            var colors = _colorService.GetAll();
            var categories = _categoryService.GetAll();
            return View(new ProductModel
            {
                Product = new Product(),
                Brands = brands.Success ? brands.Data : new List<Brand>(),
                Colors = colors.Success ? colors.Data : new List<Color>(),
                Categories = categories.Success ? categories.Data : new List<Category>(),
                ProductDtos = productDtos.Success ? productDtos.Data : new List<ProductDto>(),
                Result = new ResultModel()
            });
        }
        [Route("products/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Product product)
        {
            var files = Request.Form.Files;
            ModelState.Remove("product.Image");
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    if (files.Count() > 0)
                        product.Image = await ImageBaseClass.Upload(files[0], _environment.WebRootPath);

                    var data = _productService.Add(product);
                    var productDtos = _productService.GetAll();
                    var brands = _brandService.GetAll();
                    var colors = _colorService.GetAll();
                    var categories = _categoryService.GetAll();
                    ModelState.Clear();
                    return View("Index", new ProductModel
                    {
                        Product = new Product(),
                        Brands = brands.Success ? brands.Data : new List<Brand>(),
                        Colors = colors.Success ? colors.Data : new List<Color>(),
                        Categories = categories.Success ? categories.Data : new List<Category>(),
                        ProductDtos = productDtos.Success ? productDtos.Data : new List<ProductDto>(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message }
                    });
                }
                else
                {

                    if (files.Count() > 0)
                        product.Image = await ImageBaseClass.Upload(files[0], _environment.WebRootPath);
                   
                    var data = _productService.Update(product);
                    var productDtos = _productService.GetAll();
                    var brands = _brandService.GetAll();
                    var colors = _colorService.GetAll();
                    var categories = _categoryService.GetAll();
                    ModelState.Clear();
                    return View("Index", new ProductModel
                    {
                        Product = new Product(),
                        Brands = brands.Success ? brands.Data : new List<Brand>(),
                        Colors = colors.Success ? colors.Data : new List<Color>(),
                        Categories = categories.Success ? categories.Data : new List<Category>(),
                        ProductDtos = productDtos.Success ? productDtos.Data : new List<ProductDto>(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message }
                    });
                }
            }
            else
            {
                var productDtos = _productService.GetAll();
                var brands = _brandService.GetAll();
                var colors = _colorService.GetAll();
                var categories = _categoryService.GetAll();
                ModelState.Clear();
                ViewBag.Message = "Model Hatalı";
                return View("Index", new ProductModel
                {
                    Product = new Product(),
                    Brands = brands.Success ? brands.Data : new List<Brand>(),
                    Colors = colors.Success ? colors.Data : new List<Color>(),
                    Categories = categories.Success ? categories.Data : new List<Category>(),
                    ProductDtos = productDtos.Success ? productDtos.Data : new List<ProductDto>(),
                    Result = new ResultModel()
                });

            }
        }

        [Route("products/{id}")]
        public IActionResult Index(int id)
        {
            var product = _productService.GetById(id);
            var productDtos = _productService.GetAll();
            var brands = _brandService.GetAll();
            var colors = _colorService.GetAll();
            var categories = _categoryService.GetAll();
            if (product == null)
            {
                return Redirect("/products");
            }
            return View(new ProductModel
            {
                Product = product.Success ? product.Data : new Product(),
                Brands = brands.Success ? brands.Data : new List<Brand>(),
                Colors = colors.Success ? colors.Data : new List<Color>(),
                Categories = categories.Success ? categories.Data : new List<Category>(),
                ProductDtos = productDtos.Success ? productDtos.Data : new List<ProductDto>(),
                Result = new ResultModel()
            });
        }

        [Route("products/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);

            if (product.Success)
            {
                var data = _productService.Delete(product.Data);
                var productDtos = _productService.GetAll();
                var brands = _brandService.GetAll();
                var colors = _colorService.GetAll();
                var categories = _categoryService.GetAll();

                ProductModel model = new ProductModel
                {
                    Product = new Product(),
                    Brands = brands.Success ? brands.Data : new List<Brand>(),
                    Colors = colors.Success ? colors.Data : new List<Color>(),
                    Categories = categories.Success ? categories.Data : new List<Category>(),
                    ProductDtos = productDtos.Success ? productDtos.Data : new List<ProductDto>(),
                    Result = new ResultModel() { Error = data.Success, Message = data.Message }
                };
                return View("Index", model);
            }
            else
            {
                var productDtos = _productService.GetAll();
                var brands = _brandService.GetAll();
                var colors = _colorService.GetAll();
                var categories = _categoryService.GetAll();

                return View("Index", new ProductModel
                {
                    Product = new Product(),
                    Brands = brands.Success ? brands.Data : new List<Brand>(),
                    Colors = colors.Success ? colors.Data : new List<Color>(),
                    Categories = categories.Success ? categories.Data : new List<Category>(),
                    ProductDtos = productDtos.Success ? productDtos.Data : new List<ProductDto>(),
                    Result = new ResultModel() { Error = product.Success, Message = product.Message }
                });
            }
        }
    }
}
