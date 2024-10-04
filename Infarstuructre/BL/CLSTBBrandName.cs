

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IIBrandName
    {
        List<TBBrandName> GetAll();
        TBBrandName GetById(int IdBrandName);
        bool saveData(TBBrandName savee);
        bool UpdateData(TBBrandName updatss);
        bool deleteData(int IdBrandName);
        List<TBBrandName> GetAllv(int IdBrandName);
        ////////////////////API/////////////////////////////
        ///
        Task<List<TBBrandName>> GetAllAsync();
        Task<TBBrandName> GetByIdAsync(int IdBrandName);
        Task<bool> AddDataAsync(TBBrandName savee);
        Task<bool> UpdateDataAsync(TBBrandName updatss);
        Task<bool> DeleteDataAsync(int IdBrandName);
        Task<List<TBBrandName>> GetAllvAsync(int IdBrandName);
    }
    public class CLSTBBrandName: IIBrandName
    {
        MasterDbcontext dbcontext;
        public CLSTBBrandName(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBBrandName> GetAll()
        {
            List<TBBrandName> MySlider = dbcontext.TBBrandNames.OrderByDescending(n => n.IdBrandName).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBBrandName GetById(int IdBrandName)
        {
            TBBrandName sslid = dbcontext.TBBrandNames.FirstOrDefault(a => a.IdBrandName == IdBrandName);
            return sslid;
        }
        public bool saveData(TBBrandName savee)
        {
            try
            {
                dbcontext.Add<TBBrandName>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBBrandName updatss)
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
        public bool deleteData(int IdBrandName)
        {
            try
            {
                var catr = GetById(IdBrandName);
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
        public List<TBBrandName> GetAllv(int IdBrandName)
        {
            List<TBBrandName> MySlider = dbcontext.TBBrandNames.OrderByDescending(n => n.IdBrandName == IdBrandName).Where(a => a.IdBrandName == IdBrandName).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        /// //////////////////////////////////////////API//////////////////////////////////////////////////

        public async Task<List<TBBrandName>> GetAllAsync()
        {
            List<TBBrandName> MySlider = await dbcontext.TBBrandNames.OrderByDescending(n => n.IdBrandName).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBBrandName> GetByIdAsync(int IdBrandName)
        {
            TBBrandName sslid = await dbcontext.TBBrandNames.FirstOrDefaultAsync(a => a.IdBrandName == IdBrandName);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBBrandName savee)
        {
            try
            {
                await dbcontext.AddAsync<TBBrandName>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBBrandName updatss)
        {
            try
            {
                dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDataAsync(int IdPurchaseDocumentation)
        {
            try
            {
                var catr = await GetByIdAsync(IdPurchaseDocumentation);
                catr.CurrentState = false;
                //TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdBrand == IdBrand).FirstOrDefault();
                //dbcontex.TbSubCateegoorys.Remove(dele);
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<TBBrandName>> GetAllvAsync(int IdBrandName)
        {
            List<TBBrandName> MySlider = await dbcontext.TBBrandNames.OrderByDescending(n => n.IdBrandName == IdBrandName).Where(a => a.IdBrandName == IdBrandName).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }
    }
}
