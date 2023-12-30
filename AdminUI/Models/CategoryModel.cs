using EntityLayer.Concrete;

namespace AdminUI.Models
{
    public class CategoryModel
    {
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
        public ResultModel Result { get; set; }
    }
}
