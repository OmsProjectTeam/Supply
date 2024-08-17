
using Microsoft.EntityFrameworkCore;

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
        //////////////////////////APIs/////////////////////////////////////////////////////////////////
        Task<List<TBBondType>> GetAllAsync();
        Task<List<TBBondType>> GetAllvAsync(int IdCustomerMessages);
        Task<List<TBBondType>> GetAllDataentryAsync(string dataEntry);
        Task<TBBondType> GetByIdAsync(int IdCustomerMessages);
        Task<List<TBBondType>> GetAllActiveAsync();
        Task<bool> AddDataAsync(TBBondType savee);
        Task<bool> DeleteDataAsync(int TBBondType);
        Task<bool> UpdateDataAsync(TBBondType update);
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

        // //////////////////////////APIs/////////////////////////////////////////////////////////////////

        public async Task<List<TBBondType>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBBondTypes.OrderByDescending(n => n.IdBondType).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBBondType>> GetAllvAsync(int IdBondType)
        {
            var myDatd = await dbcontext.TBBondTypes.OrderByDescending(n => n.IdBondType).Where(a => a.IdBondType == IdBondType).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBBondType>> GetAllDataentryAsync(string dataEntry)
        {
            var MySlider = await dbcontext.TBBondTypes.Where(a => a.DataEntry == dataEntry && a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<List<TBBondType>> GetAllActiveAsync()
        {
            List<TBBondType> MySlider = await dbcontext.TBBondTypes.OrderByDescending(n => n.IdBondType).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBBondType> GetByIdAsync(int IdBondType)
        {
            var sslid = await dbcontext.TBBondTypes.FirstOrDefaultAsync(a => a.IdBondType == IdBondType);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBBondType savee)
        {
            try
            {
                await dbcontext.AddAsync<TBBondType>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdBondType)
        {
            try
            {
                var email = await GetByIdAsync(IdBondType);
                email.CurrentState = false;
                dbcontext.Entry(email).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBBondType update)
        {
            try
            {
                dbcontext.Entry(update).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
