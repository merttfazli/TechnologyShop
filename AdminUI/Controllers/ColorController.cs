using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    [Authorize(Policy = "ElevatedRights", Roles = "Admin,User")]
    public class ColorController : Controller
    {
        private IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [Route("colors")]
        public IActionResult Index()
        {
            var colors = _colorService.GetAll();
            return View(new ColorModel
            {
                Color = new Color(),
                Colors = colors.Success ? colors.Data : new List<Color>(),
                Result=new ResultModel(),
            });
        }

        [Route("colors")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Color color)
        {
            if (ModelState.IsValid)
            {
                if (color.Id == 0)
                {
                    var data=_colorService.Add(color);
                    var colors = _colorService.GetAll();
                    ModelState.Clear();
                    return View("Index", new ColorModel
                    {
                        Color = new Color(),
                        Colors = colors.Success ? colors.Data : new List<Color>(),
                        Result = new ResultModel() { Error=data.Success,Message=data.Message},
                    });
                }
                else
                {

                    var data = _colorService.Update(color);
                    var colors = _colorService.GetAll();
                    ModelState.Clear();
                    return View("Index", new ColorModel
                    {
                        Color = new Color(),
                        Colors = colors.Success ? colors.Data : new List<Color>(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message },
                    });
                }
            }
            else
            {
                var colors = _colorService.GetAll();
                ModelState.Clear();
                ViewBag.Message = "Model Hatalı";
                return View("Index", new ColorModel
                {
                    Color = new Color(),
                    Colors = colors.Success ? colors.Data : new List<Color>(),
                    Result = new ResultModel(),
                });

            }
        }

        [Route("colors/{id}")]
        public IActionResult Index(int id)
        {
            var color = _colorService.GetById(id);
            var colors = _colorService.GetAll();
            if (color == null)
            {
                return Redirect("/colors");
            }
            return View(new ColorModel
            {
                Color = color.Success ? color.Data : new Color(),
                Colors = colors.Success ? colors.Data : new List<Color>(),
                Result = new ResultModel(),
            });
        }

        [Route("colors/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var color = _colorService.GetById(id);

            if (color.Success)
            {
                var data = _colorService.Delete(color.Data);
                var colors = _colorService.GetAll();

                ColorModel model = new ColorModel
                {
                    Color = new Color(),
                    Colors = colors.Success ? colors.Data : new List<Color>(),
                    Result = new ResultModel() { Error = data.Success, Message = data.Message },
                };
                return View("Index", model);
            }
            else
            {
                var colors = _colorService.GetAll();

                return View("Index", new ColorModel
                {
                    Color = new Color(),
                    Colors = colors.Success ? colors.Data : new List<Color>(),
                    Result = new ResultModel()
                });
            }
        }
    }
}
