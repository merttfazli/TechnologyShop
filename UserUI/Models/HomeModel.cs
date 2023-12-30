using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;

namespace UserUI.Models
{
    public class HomeModel
    {
        public List<ProductDto> ProductDtos { get; set; }
        public List<FeatureRelationDto> FeatureRelationDtos { get; set; }
        public List<Category> Categories { get; set; }
        public List<ProductDto> AmaountOfProduct { get; set; }
        public List<ProductDto> AmountOfProductInBrand { get; set; }
    }
}
