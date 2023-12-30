using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;

namespace AdminUI.Models
{
    public class FeatureRelationModel
    {
        public FeatureRelation FeatureRelation { get; set; }
        public List<FeatureRelationDto> FeatureRelationDtos { get; set; }
        public FeatureRelationDto FeatureRelationDto { get; set; }
        public List<Feature> Features { get; set; }
        public Feature Feature { get; set; }
        public Product Product { get; set; }
        public ResultModel Result { get; set; }
    }
}
