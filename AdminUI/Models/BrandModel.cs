using EntityLayer.Concrete;

namespace AdminUI.Models
{
    public class BrandModel
    {
        public Brand Brand { get; set; }
        public List<Brand> Brands { get; set; }
        public ResultModel Result { get; set; }
    }
}
