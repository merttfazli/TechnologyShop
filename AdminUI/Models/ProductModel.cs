using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;

namespace AdminUI.Models
{
    public class ProductModel
    {
        public List<ProductDto> ProductDtos { get; set; }
        public Product Product { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }
        public List<Category> Categories { get; set; }
        public ResultModel Result { get; set; }

    }
}
