using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    public class FeatureController : Controller
    {
        private IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [Route("features")]
        public IActionResult Index()
        {
            var features = _featureService.GetAll();
            return View(new FeatureModel
            {
                Feature = new Feature(),
                Features = features.Success ? features.Data : new List<Feature>(),
                Result = new ResultModel()
            }); ;
        }
        [Route("features/add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Feature feature)
        {
            if (ModelState.IsValid)
            {
                if (feature.Id == 0)
                {
                    var data = _featureService.Add(feature);
                    var features = _featureService.GetAll();
                    ModelState.Clear();
                    return View("Index", new FeatureModel
                    {
                        Feature = new Feature(),
                        Features = features.Success ? features.Data : new List<Feature>(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message }
                    });
                }
                else
                {
                    var data = _featureService.Update(feature);
                    var features = _featureService.GetAll();
                    ModelState.Clear();
                    return View("Index", new FeatureModel
                    {
                        Feature = new Feature(),
                        Features = features.Success ? features.Data : new List<Feature>(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message }
                    });
                }
            }
            else
            {
                var features = _featureService.GetAll();
                ModelState.Clear();
                ViewBag.Message = "Model Hatalı";
                return View("Index", new FeatureModel
                {
                    Feature = new Feature(),
                    Features = features.Success ? features.Data : new List<Feature>(),
                    Result = new ResultModel()
                });
            }
        }
        [Route("features/id")]
        public IActionResult Index(int id)
        {
            var feature = _featureService.GetById(id);
            var features = _featureService.GetAll();
            if (feature == null)
                return Redirect("/features");
            return View(new FeatureModel
            {
                Feature = feature.Success ? feature.Data : new Feature(),
                Features = features.Success ? features.Data : new List<Feature>(),
                Result = new ResultModel(),
            });
        }
        [Route("features/delete/id")]
        public IActionResult Delete(int id)
        {
            var feature = _featureService.GetById(id);
            if (feature.Success)
            {
                var data = _featureService.Delete(feature.Data);
                var features = _featureService.GetAll();
                FeatureModel model = new FeatureModel
                {
                    Feature = new Feature(),
                    Features = features.Success ? features.Data : new List<Feature>(),
                    Result = new ResultModel() { Error = data.Success, Message = data.Message }
                };
                return View("Index",model);
            }
            else
            {
                var features = _featureService.GetAll();

                return View("Index", new FeatureModel
                {
                    Feature=new Feature(),
                    Features=features.Success ? features.Data : new List<Feature>(),
                    Result=new ResultModel()
                });
            }

        }
    }
}
