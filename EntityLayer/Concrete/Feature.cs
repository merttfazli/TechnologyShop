using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Feature:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Choice { get; set; }
    }
}
