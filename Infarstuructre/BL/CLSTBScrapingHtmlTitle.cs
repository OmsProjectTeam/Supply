

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IIScrapingHtmlTitle
    {
        List<TBScrapingHtmlTitle> GetAll();
        TBScrapingHtmlTitle GetById(int IdScrapingHtmlTitle);
        bool saveData(TBScrapingHtmlTitle savee);
        bool UpdateData(TBScrapingHtmlTitle updatss);
        bool deleteData(int IdScrapingHtmlTitle);
        List<TBScrapingHtmlTitle> GetAllv(int IdScrapingHtmlTitle);
        ////////////////////////////API////////////////////////////////
        ///
        Task<List<TBScrapingHtmlTitle>> GetAllAsync();
        Task<TBScrapingHtmlTitle> GetByIdAsync(int IdScrapingHtmlTitle);
        Task<bool> AddDataAsync(TBScrapingHtmlTitle savee);
        Task<bool> UpdateDataAsync(TBScrapingHtmlTitle updatss);
        Task<bool> DeleteDataAsync(int IdScrapingHtmlTitle);
        Task<List<TBScrapingHtmlTitle>> GetAllvAsync(int IdScrapingHtmlTitle);

    }
    public class CLSTBScrapingHtmlTitle: IIScrapingHtmlTitle
    {
        MasterDbcontext dbcontext;
        public CLSTBScrapingHtmlTitle(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBScrapingHtmlTitle> GetAll()
        {
            List<TBScrapingHtmlTitle> MySlider = dbcontext.TBScrapingHtmlTitles.OrderByDescending(n => n.IdScrapingHtmlTitle).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBScrapingHtmlTitle GetById(int IdScrapingHtmlTitle)
        {
            TBScrapingHtmlTitle sslid = dbcontext.TBScrapingHtmlTitles.FirstOrDefault(a => a.IdScrapingHtmlTitle == IdScrapingHtmlTitle);
            return sslid;
        }
        public bool saveData(TBScrapingHtmlTitle savee)
        {
            try
            {
                dbcontext.Add<TBScrapingHtmlTitle>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBScrapingHtmlTitle updatss)
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
        public bool deleteData(int IdScrapingHtmlTitle)
        {
            try
            {
                var catr = GetById(IdScrapingHtmlTitle);
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
        public List<TBScrapingHtmlTitle> GetAllv(int IdScrapingHtmlTitle)
        {
            List<TBScrapingHtmlTitle> MySlider = dbcontext.TBScrapingHtmlTitles.OrderByDescending(n => n.IdScrapingHtmlTitle == IdScrapingHtmlTitle).Where(a => a.IdScrapingHtmlTitle == IdScrapingHtmlTitle).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        /// //////////////////////////////////////////API//////////////////////////////////////////////////

        public async Task<List<TBScrapingHtmlTitle>> GetAllAsync()
        {
            List<TBScrapingHtmlTitle> MySlider = await dbcontext.TBScrapingHtmlTitles.OrderByDescending(n => n.IdScrapingHtmlTitle).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBScrapingHtmlTitle> GetByIdAsync(int IdScrapingHtmlTitle)
        {
            TBScrapingHtmlTitle sslid = await dbcontext.TBScrapingHtmlTitles.FirstOrDefaultAsync(a => a.IdScrapingHtmlTitle == IdScrapingHtmlTitle);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBScrapingHtmlTitle savee)
        {
            try
            {
                await dbcontext.AddAsync<TBScrapingHtmlTitle>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBScrapingHtmlTitle updatss)
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

        public async Task<bool> DeleteDataAsync(int IdScrapingHtmlTitle)
        {
            try
            {
                var catr = await GetByIdAsync(IdScrapingHtmlTitle);
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

        public async Task<List<TBScrapingHtmlTitle>> GetAllvAsync(int IdScrapingHtmlTitle)
        {
            List<TBScrapingHtmlTitle> MySlider = await dbcontext.TBScrapingHtmlTitles.OrderByDescending(n => n.IdScrapingHtmlTitle == IdScrapingHtmlTitle).Where(a => a.IdScrapingHtmlTitle == IdScrapingHtmlTitle).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }
    }

}

