using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{

    public interface IIWareHouseType
    {
        List<TBViewWareHouseType> GetAll();
        TBWareHouseType GetById(int id);
        bool saveData(TBWareHouseType savee);
        bool UpdateData(TBWareHouseType updatss);
        bool deleteData(int id);
        List<TBViewWareHouseType> GetAllv(int IdWareHouseType);
        List<TBViewWareHouseType> GetAllActive();

        //////////////////API//////////////////////////
        Task<List<TBViewWareHouseType>> GetAllAPI();
        Task<List<TBViewWareHouseType>> GetAllvAPI(int IdWareHouseType);
        Task<TBWareHouseType> GetByIdAPI(int IdWareHouseType);
        Task<TBWareHouseType> GetByNameAPI(string name);
        Task SaveDataAPI(TBWareHouseType savee);
        Task DeleteDataAPI(int IdWareHouseType);
        Task UpdateDataAPI(TBWareHouseType update);
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
        public TBWareHouseType GetById(int IdWareHouseType)
        {
            TBWareHouseType sslid = dbcontext.TBWareHouseTypes.FirstOrDefault(a => a.IdWareHouseType == IdWareHouseType);
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
        public bool deleteData(int Id)
        {
            try
            {
                var catr = GetById(Id);
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

        public List<TBViewWareHouseType> GetAllv(int IdWareHouseType)
        {
            List<TBViewWareHouseType> MySlider = dbcontext.ViewWareHouseType.OrderByDescending(n => n.IdWareHouseType == IdWareHouseType).Where(a => a.IdWareHouseType == IdWareHouseType).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        //////////////////API//////////////////////////

        public async Task<List<TBViewWareHouseType>> GetAllAPI()
        {
            List<TBViewWareHouseType> Slider = await dbcontext.ViewWareHouseType.Where(a => a.CurrentState == true).ToListAsync();
            return Slider;
        }

        public async Task<List<TBViewWareHouseType>> GetAllvAPI(int IdWareHouseType)
        {
            List<TBViewWareHouseType> Slider = await dbcontext.ViewWareHouseType.OrderByDescending(n => n.IdWareHouseType == IdWareHouseType).Where(a => a.IdWareHouseType == IdWareHouseType).Where(a => a.CurrentState == true).ToListAsync();
            return Slider;
        }

        public async Task<TBWareHouseType> GetByIdAPI(int IdWareHouseType)
        {
            TBWareHouseType sslid = await dbcontext.TBWareHouseTypes.FirstOrDefaultAsync(a => a.IdWareHouseType == IdWareHouseType);
            return sslid;
        }

        public async Task<TBWareHouseType> GetByNameAPI(string name)
        {
            TBWareHouseType sslid = await dbcontext.TBWareHouseTypes.FirstOrDefaultAsync(a => a.WareHouseType == name);
            return sslid;
        }

        public async Task SaveDataAPI(TBWareHouseType savee)
        {
            await dbcontext.TBWareHouseTypes.AddAsync(savee);
            await dbcontext.SaveChangesAsync();
        }

        public async Task DeleteDataAPI(int IdWareHouseType)
        {
            var catr = GetById(IdWareHouseType);
            catr.CurrentState = false;
            dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await dbcontext.SaveChangesAsync();
        }

        public async Task UpdateDataAPI(TBWareHouseType update)
        {
            dbcontext.Entry(update).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await dbcontext.SaveChangesAsync();
        }
    }
}
