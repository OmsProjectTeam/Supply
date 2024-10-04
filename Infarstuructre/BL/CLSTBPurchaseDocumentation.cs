

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IIPurchaseDocumentation
    {
        List<TBPurchaseDocumentation> GetAll();
        TBPurchaseDocumentation GetById(int IdPurchaseDocumentation);
        bool saveData(TBPurchaseDocumentation savee);
        bool UpdateData(TBPurchaseDocumentation updatss);
        bool deleteData(int IdPurchaseDocumentation);
        List<TBPurchaseDocumentation> GetAllv(int IdPurchaseDocumentation);
        //////////////////////////////////////API//////////////////////////////////////
        ///
        Task<List<TBPurchaseDocumentation>> GetAllAsync();
         Task<TBPurchaseDocumentation> GetByIdAsync(int IdPurchaseDocumentation);
         Task<bool> AddDataAsync(TBPurchaseDocumentation savee);
         Task<bool> UpdateDataAsync(TBPurchaseDocumentation updatss);
         Task<bool> DeleteDataAsync(int IdPurchaseDocumentation);
         Task<List<TBPurchaseDocumentation>> GetAllvAsync(int IdPurchaseDocumentation);

    }
    public class CLSTBPurchaseDocumentation: IIPurchaseDocumentation
    {
        MasterDbcontext dbcontext;
        public CLSTBPurchaseDocumentation(MasterDbcontext dbcontext1)
        {
            dbcontext= dbcontext1;
        }
        public List<TBPurchaseDocumentation> GetAll()
        {
            List<TBPurchaseDocumentation> MySlider = dbcontext.TBPurchaseDocumentations.OrderByDescending(n => n.IdPurchaseDocumentation).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBPurchaseDocumentation GetById(int IdPurchaseDocumentation)
        {
            TBPurchaseDocumentation sslid = dbcontext.TBPurchaseDocumentations.FirstOrDefault(a => a.IdPurchaseDocumentation == IdPurchaseDocumentation);
            return sslid;
        }
        public bool saveData(TBPurchaseDocumentation savee)
        {
            try
            {
                dbcontext.Add<TBPurchaseDocumentation>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBPurchaseDocumentation updatss)
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
        public bool deleteData(int IdPurchaseDocumentation)
        {
            try
            {
                var catr = GetById(IdPurchaseDocumentation);
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
        public List<TBPurchaseDocumentation> GetAllv(int IdPurchaseDocumentation)
        {
            List<TBPurchaseDocumentation> MySlider = dbcontext.TBPurchaseDocumentations.OrderByDescending(n => n.IdPurchaseDocumentation == IdPurchaseDocumentation).Where(a => a.IdPurchaseDocumentation == IdPurchaseDocumentation).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        /// //////////////////////////////////////////API//////////////////////////////////////////////////

        public async Task<List<TBPurchaseDocumentation>> GetAllAsync()
        {
            List<TBPurchaseDocumentation> MySlider = await dbcontext.TBPurchaseDocumentations.OrderByDescending(n => n.IdPurchaseDocumentation).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBPurchaseDocumentation> GetByIdAsync(int IdPurchaseDocumentation)
        {
            TBPurchaseDocumentation sslid = await dbcontext.TBPurchaseDocumentations.FirstOrDefaultAsync(a => a.IdPurchaseDocumentation == IdPurchaseDocumentation);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBPurchaseDocumentation savee)
        {
            try
            {
                await dbcontext.AddAsync<TBPurchaseDocumentation>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBPurchaseDocumentation updatss)
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

        public async Task<List<TBPurchaseDocumentation>> GetAllvAsync(int IdPurchaseDocumentation)
        {
            List<TBPurchaseDocumentation> MySlider = await dbcontext.TBPurchaseDocumentations.OrderByDescending(n => n.IdPurchaseDocumentation == IdPurchaseDocumentation).Where(a => a.IdPurchaseDocumentation == IdPurchaseDocumentation).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }
    }
}
