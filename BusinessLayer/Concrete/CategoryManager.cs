using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public IResult Add(Category category)
        {
            var getCategory = _categoryDal.Get(x => x.Name == category.Name);
            var categories = _categoryDal.GetAll();
            if (getCategory != null)
                return new ErrorResult(Messages.CategoryAlreadyExists);
            else
            {
                if (checkEquality(category))
                    return new ErrorResult(Messages.CategoryAlreadyExists);
                else
                {
                    _categoryDal.Add(category);
                    return new SuccessResult(Messages.CategoryAdded);
                }
            }
        }
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }
        public IDataResult<List<Category>> GetAll()
        {
            var categories = _categoryDal.GetAll();
            if (categories.Any())
                return new SuccessDataResult<List<Category>>(categories);
            else
                return new ErrorDataResult<List<Category>>(Messages.CategoryNotFound);
        }
      
        public IDataResult<Category> GetById(int id)
        {
            var category = _categoryDal.Get(x => x.Id == id);
            if (category != null)
                return new SuccessDataResult<Category>(category);
            else
                return new ErrorDataResult<Category>(Messages.CategoryNotFound);
        }

        public IResult Update(Category category)
        {
            var getCategory = _categoryDal.Get(x => x.Name == category.Name && x.Id != category.Id);
            if (getCategory != null)
                return new ErrorResult(Messages.CategoryAlreadyExists);
            else
            {
                _categoryDal.Update(category);
                return new SuccessResult(Messages.CategoryUpdated);
            }
        }
        private bool checkEquality(Category category)
        {
            var categories = _categoryDal.GetAll();
            foreach (var cat in categories)
            {
                if (cat.Name.Equals(category.Name))
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
