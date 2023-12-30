using EntityLayer.Concrete.Dtos;
using EntityLayer.Concrete;

namespace UserUI.Models
{
    public class CategoryModel
    {
        public List<ProductDto> ProductDtos { get; set; }
        public List<FeatureRelationDto> FeatureRelationDtos { get; set; }
    }
}
