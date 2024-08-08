
namespace Infarstuructre.BL
{
    public interface IITypesProduct
    {
        List<TBTypesProduct> GetAll();
        TBTypesProduct GetById(int IdTypesProduct);
        bool saveData(TBTypesProduct savee);
        bool UpdateData(TBTypesProduct updatss);
        bool deleteData(int IdTypesProduct);
        List<TBTypesProduct> GetAllv(int IdTypesProduct);
    }
    public class CLSTBTypesProduct: IITypesProduct
    {
        MasterDbcontext dbcontext;
        public CLSTBTypesProduct(MasterDbcontext dbcontext1)
        {
            dbcontext= dbcontext1;
        }
        public List<TBTypesProduct> GetAll()
        {
            List<TBTypesProduct> MySlider = dbcontext.TBTypesProducts.OrderByDescending(n => n.IdTypesProduct).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTypesProduct GetById(int IdTypesProduct)
        {
            TBTypesProduct sslid = dbcontext.TBTypesProducts.FirstOrDefault(a => a.IdTypesProduct == IdTypesProduct);
            return sslid;
        }
        public bool saveData(TBTypesProduct savee)
        {
            try
            {
                dbcontext.Add<TBTypesProduct>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTypesProduct updatss)
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
        public bool deleteData(int IdTypesProduct)
        {
            try
            {
                var catr = GetById(IdTypesProduct);
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
        public List<TBTypesProduct> GetAllv(int IdTypesProduct)
        {
            List<TBTypesProduct> MySlider = dbcontext.TBTypesProducts.OrderByDescending(n => n.IdTypesProduct == IdTypesProduct).Where(a => a.IdTypesProduct == IdTypesProduct).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
