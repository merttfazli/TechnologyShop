using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IModuleService
    {
        List<Module> GetAllByRoot(int root);
        void Add(Module module);
        void Delete(Module module);
        List<Module> GetAll();
        Module GetById(int id);
        void Update(Module module);
    }
}
