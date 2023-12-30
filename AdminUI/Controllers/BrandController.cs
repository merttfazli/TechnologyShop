using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    [Authorize(Policy = "ElevatedRights", Roles = "Admin,User")]
    public class BrandController : Controller
    {
        private IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [Route("brands")]
        public IActionResult Index()
        {
            var brands = _brandService.GetAll();
            return View(new BrandModel
            {
                Brand = new Brand(),
                Brands = brands.Success ? brands.Data : new List<Brand>(),
                Result = new ResultModel(),
            });
        }

        [Route("brands")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Brand brand)
        {
            if (ModelState.IsValid)
            {
                if (brand.Id == 0)
                {
                    var data=_brandService.Add(brand);
                    var brands = _brandService.GetAll();
                    ModelState.Clear();
                    return View("Index", new BrandModel
                    {
                        Brand = new Brand(),
                        Brands = brands.Success ? brands.Data : new List<Brand>(),
                        Result = new ResultModel() { Error=data.Success,Message=data.Message},
                    });
                }
                else
                {

                    var data=_brandService.Update(brand);
                    var brands = _brandService.GetAll();
                    ModelState.Clear();
                    return View("Index", new BrandModel
                    {
                        Brand = new Brand(),
                        Brands = brands.Success ? brands.Data : new List<Brand>(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message },
                    });
                }
            }
            else
            {
                var brands = _brandService.GetAll();
                ModelState.Clear();
                return View("Index", new BrandModel
                {
                    Brand = new Brand(),
                    Brands = brands.Success ? brands.Data : new List<Brand>(),
                    Result = new ResultModel(),
                });

            }
        }

        [Route("brands/{id}")]
        public IActionResult Index(int id)
        {
            var brand = _brandService.GetById(id);
            var brands = _brandService.GetAll();
            if (brand == null)
            {
                return Redirect("/brands");
            }
            return View(new BrandModel
            {
                Brand = brand.Success ? brand.Data : new Brand(),
                Brands = brands.Success ? brands.Data : new List<Brand>(),
                Result = new ResultModel(),
            });
        }

        [Route("brands/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var brand = _brandService.GetById(id);

            if (brand.Success)
            {
                var data=_brandService.Delete(brand.Data);
                var brands = _brandService.GetAll();

                BrandModel model = new BrandModel
                {
                    Brand = new Brand(),
                    Brands = brands.Success ? brands.Data : new List<Brand>(),
                    Result = new ResultModel() { Error = data.Success, Message = data.Message },
                };
                return View("Index", model);
            }
            else
            {
                var brands = _brandService.GetAll();

                return View("Index", new BrandModel
                {
                    Brand = new Brand(),
                    Brands = brands.Success ? brands.Data : new List<Brand>(),
                    Result = new ResultModel() { Error = brand.Success, Message = brand.Message },
                });
            }
        }
    }
}
