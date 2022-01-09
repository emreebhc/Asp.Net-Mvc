using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categorydal;

        public CategoryManager(ICategoryDal categorydal)
        {
            _categorydal = categorydal;
        }

        public void CategoryAdd(Category p)
        {
            _categorydal.Insert(p);
        }

        public void CategoryDelete(Category p)
        {
            _categorydal.Delete(p);
        }

        public void CategoryUpdate(Category p)
        {
            _categorydal.Update(p);
        }

        public Category GetByID(int id)
        {
            return _categorydal.Get(x=>x.CategoryID==id);
        }

        public List<Category> GetList()
        {
            return _categorydal.List();
        }


       




    }
}
