namespace Infarstuructre.BL
{

    public interface IIWareHouseType
    {
        List<TBViewWareHouseType> GetAll();
        TBWareHouseType GetById(int id);
        bool saveData(TBWareHouseType savee);
        bool UpdateData(TBWareHouseType updatss);
        bool deleteData(int id);
        List<TBViewWareHouseType> GetAllv(int id);
        List<TBViewWareHouseType> GetAllActive();
    }

    public class CLSTBWareHouseType : IIWareHouseType
    {
        MasterDbcontext dbcontext;
        public CLSTBWareHouseType(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;

        }
        public List<TBViewWareHouseType> GetAll()
        {
            List<TBViewWareHouseType> MySlider = dbcontext.ViewWareHouseType.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public List<TBViewWareHouseType> GetAllActive()
        {
            List<TBViewWareHouseType> MySlider = dbcontext.ViewWareHouseType.OrderByDescending(n => n.IdWareHouseType).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
            return MySlider;
        }
        public TBWareHouseType GetById(int IdFAQ)
        {
            TBWareHouseType sslid = dbcontext.TBWareHouseTypes.FirstOrDefault(a => a.IdWareHouseType == IdFAQ);
            return sslid;
        }
        public bool saveData(TBWareHouseType savee)
        {
            try
            {
                dbcontext.Add<TBWareHouseType>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBWareHouseType updatss)
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
        public List<TBViewWareHouseType> GetAllv(int id)
        {
            List<TBViewWareHouseType> MySlider = dbcontext.ViewWareHouseType.OrderByDescending(n => n.IdWareHouseType == id).Where(a => a.IdWareHouseType == id).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }


    }
}
