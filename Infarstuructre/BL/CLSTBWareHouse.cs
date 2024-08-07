namespace Infarstuructre.BL
{

    public interface IIWareHouse
    {
        List<TBViewWareHouse> GetAll();
        TBWareHouse GetById(int id);
        bool saveData(TBWareHouse savee);
        bool UpdateData(TBWareHouse updatss);
        bool deleteData(int id);
        List<TBViewWareHouse> GetAllv(int id);
        List<TBViewWareHouse> GetAllActive();
    }

    public class CLSTBWareHouse : IIWareHouse
    {
        MasterDbcontext dbcontext;
        public CLSTBWareHouse(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;

        }
        public List<TBViewWareHouse> GetAll()
        {
            List<TBViewWareHouse> MySlider = dbcontext.ViewWareHouse.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public List<TBViewWareHouse> GetAllActive()
        {
            List<TBViewWareHouse> MySlider = dbcontext.ViewWareHouse.OrderByDescending(n => n.IdBWareHouse).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
            return MySlider;
        }
        public TBWareHouse GetById(int IdFAQ)
        {
            TBWareHouse sslid = dbcontext.TBWareHouses.FirstOrDefault(a => a.IdBWareHouse == IdFAQ);
            return sslid;
        }
        public bool saveData(TBWareHouse savee)
        {
            try
            {
                dbcontext.Add<TBWareHouse>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBWareHouse updatss)
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
        public bool deleteData(int IdFAQ)
        {
            try
            {
                var catr = GetById(IdFAQ);
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
        public List<TBViewWareHouse> GetAllv(int id)
        {
            List<TBViewWareHouse> MySlider = dbcontext.ViewWareHouse.OrderByDescending(n => n.IdBWareHouse == id).Where(a => a.IdBWareHouse == id).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }


    }
}
