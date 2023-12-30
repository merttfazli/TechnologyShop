using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ModuleManager:IModuleService
    {
        private readonly IModuleDal _moduleDal;

        public ModuleManager(IModuleDal moduleDal)
        {
            _moduleDal = moduleDal;
        }

        public void Add(Module module)
        {
            _moduleDal.Add(module);
        }

        public void Delete(Module module)
        {
            _moduleDal.Delete(module);
        }

        public List<Module> GetAll()
        {
            return _moduleDal.GetAll();
        }

        public Module GetById(int id)
        {
            return _moduleDal.Get(x => x.Id == id);
        }

        public List<Module> GetAllByRoot(int root)
        {
            return _moduleDal.GetAll(x => x.Root == root);
        }

        public void Update(Module module)
        {
            _moduleDal.Update(module);
        }
    }
}
