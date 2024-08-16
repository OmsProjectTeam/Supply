
namespace Infarstuructre.BL
{
    public interface IIBondType
    {
        List<TBBondType> GetAll();
        TBBondType GetById(int IdBondType);
        bool saveData(TBBondType savee);
        bool UpdateData(TBBondType updatss);
        bool deleteData(int IdBondType);
        List<TBBondType> GetAllv(int IdBondType);
    }
    public class CLSTBBondType: IIBondType
    {
        MasterDbcontext dbcontext;
        public CLSTBBondType(MasterDbcontext dbcontex1)
        {
            dbcontext= dbcontex1;
        }

        public List<TBBondType> GetAll()
        {
            List<TBBondType> MySlider = dbcontext.TBBondTypes.OrderByDescending(n => n.IdBondType).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBBondType GetById(int IdBondType)
        {
            TBBondType sslid = dbcontext.TBBondTypes.FirstOrDefault(a => a.IdBondType == IdBondType);
            return sslid;
        }
        public bool saveData(TBBondType savee)
        {
            try
            {
                dbcontext.Add<TBBondType>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBBondType updatss)
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
        public bool deleteData(int IdBondType)
        {
            try
            {
                var catr = GetById(IdBondType);
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
        public List<TBBondType> GetAllv(int IdBondType)
        {
            List<TBBondType> MySlider = dbcontext.TBBondTypes.OrderByDescending(n => n.IdBondType == IdBondType).Where(a => a.IdBondType == IdBondType).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
