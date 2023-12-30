using EntityLayer.Concrete.Dtos;

namespace UserUI.Models
{
    public class SearchModel
    {
        public List<ProductDto> ProductDtos { get; set; }
        public List<FeatureRelationDto> FeatureRelationDtos { get; set; }
    }
}
