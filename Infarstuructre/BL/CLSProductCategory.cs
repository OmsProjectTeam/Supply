using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIProductCategory
    {
        List<TBViewProductCategory> GetAll();
        TBProductCategory GetById(int id);
        bool saveData(TBProductCategory savee);
        bool UpdateData(TBProductCategory updatss);
        bool deleteData(int id);
        List<TBViewProductCategory> GetAllv(int id);
        List<TBViewProductCategory> GetAllActive();
    }
    public class CLSProductCategory : IIProductCategory
    {
        MasterDbcontext dbcontext;
        public CLSProductCategory(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;

        }
        public bool deleteData(int id)
        {
            try
            {
                var catr = GetById(id);
                catr.CurrentState = false;
                //TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdBrand == IdBrand).FirstOrDefault();
                //dbcontex.TbSubCateegoorys.Remove(dele);
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<TBViewProductCategory> GetAll()
        {
            List<TBViewProductCategory> MySlider = dbcontext.ViewProductCategory.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public List<TBViewProductCategory> GetAllActive()
        {
            List<TBViewProductCategory> MySlider = dbcontext.ViewProductCategory.OrderByDescending(n => n.IdProductCategory).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
            return MySlider;
        }

        public List<TBViewProductCategory> GetAllv(int id)
        {
            List<TBViewProductCategory> MySlider = dbcontext.ViewProductCategory.OrderByDescending(n => n.IdProductCategory == id).Where(a => a.IdProductCategory == id).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public TBProductCategory GetById(int id)
        {
            TBProductCategory sslid = dbcontext.TBProductCategorys.FirstOrDefault(a => a.IdProductCategory == id);
            return sslid;
        }

        public bool saveData(TBProductCategory savee)
        {
            try
            {
                dbcontext.Add<TBProductCategory>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateData(TBProductCategory updatss)
        {
            try
            {
                dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
