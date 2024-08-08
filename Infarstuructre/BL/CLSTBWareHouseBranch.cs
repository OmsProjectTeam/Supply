using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{

    public interface IIWareHouseBranch
    {
        List<TBViewWareHouseBranch> GetAll();
        TBWareHouseBranch GetById(int idBWareHouseBranch);
        bool saveData(TBWareHouseBranch savee);
        bool UpdateData(TBWareHouseBranch updatss);
        bool deleteData(int idBWareHouseBranch);
        List<TBViewWareHouseBranch> GetAllv(int idBWareHouseBranch);
        List<TBViewWareHouseBranch> GetAllActive();
        //////////////////API//////////////////////////
        Task<List<TBViewWareHouseBranch>> GetAllAPI();
        Task<List<TBViewWareHouseBranch>> GetAllvAPI(int idBWareHouseBranch);
        Task<TBWareHouseBranch> GetByIdAPI(int idBWareHouseBranch);
        Task<TBWareHouseBranch> GetByNameAPI(string name);
        Task SaveDataAPI(TBWareHouseBranch savee);
        Task DeleteDataAPI(int idBWareHouseBranch);
        Task UpdateDataAPI(TBWareHouseBranch update);
    }

    public class CLSTBWareHouseBranch : IIWareHouseBranch
    {
        MasterDbcontext dbcontext;
        public CLSTBWareHouseBranch(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;

        }
        public List<TBViewWareHouseBranch> GetAll()
        {
            List<TBViewWareHouseBranch> MySlider = dbcontext.ViewWareHouseBranch.Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public List<TBViewWareHouseBranch> GetAllActive()
        {
            List<TBViewWareHouseBranch> MySlider = dbcontext.ViewWareHouseBranch.OrderByDescending(n => n.IdBWareHouse).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
            return MySlider;
        }
        public TBWareHouseBranch GetById(int idBWareHouseBranch)
        {
            TBWareHouseBranch sslid = dbcontext.TBWareHouseBranchs.FirstOrDefault(a => a.IdBWareHouseBranch == idBWareHouseBranch);
            return sslid;
        }
        public bool saveData(TBWareHouseBranch savee)
        {
            try
            {
                dbcontext.Add<TBWareHouseBranch>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBWareHouseBranch updatss)
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
        public bool deleteData(int idBWareHouseBranch)
        {
            try
            {
                var catr = GetById(idBWareHouseBranch);
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
        public List<TBViewWareHouseBranch> GetAllv(int idBWareHouseBranch)
        {
            List<TBViewWareHouseBranch> MySlider = dbcontext.ViewWareHouseBranch.OrderByDescending(n => n.IdBWareHouseBranch == idBWareHouseBranch).Where(a => a.IdBWareHouse == idBWareHouseBranch).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        //////////////////API//////////////////////////

        public async Task<List<TBViewWareHouseBranch>> GetAllAPI()
        {
            List<TBViewWareHouseBranch> Slider = await dbcontext.ViewWareHouseBranch.Where(a => a.CurrentState == true).ToListAsync();
            return Slider;
        }

        public async Task<List<TBViewWareHouseBranch>> GetAllvAPI(int idBWareHouseBranch)
        {
            List<TBViewWareHouseBranch> Slider = await dbcontext.ViewWareHouseBranch.OrderByDescending(n => n.IdBWareHouseBranch == idBWareHouseBranch).Where(a => a.IdBWareHouseBranch == idBWareHouseBranch).Where(a => a.CurrentState == true).ToListAsync();
            return Slider;
        }

        public async Task<TBWareHouseBranch> GetByIdAPI(int idBWareHouseBranch)
        {
            TBWareHouseBranch sslid = await dbcontext.TBWareHouseBranchs.FirstOrDefaultAsync(a => a.IdBWareHouse == idBWareHouseBranch);
            return sslid;
        }

        public async Task<TBWareHouseBranch> GetByNameAPI(string name)
        {
            TBWareHouseBranch sslid = await dbcontext.TBWareHouseBranchs.FirstOrDefaultAsync(a => a.Description == name);
            return sslid;
        }

        public async Task SaveDataAPI(TBWareHouseBranch savee)
        {
            await dbcontext.TBWareHouseBranchs.AddAsync(savee);
            await dbcontext.SaveChangesAsync();
        }

        public async Task DeleteDataAPI(int id)
        {
            var catr = GetById(id);
            catr.CurrentState = false;
            dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await dbcontext.SaveChangesAsync();
        }

        public async Task UpdateDataAPI(TBWareHouseBranch update)
        {
            dbcontext.Entry(update).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await dbcontext.SaveChangesAsync();
        }
    }
}
