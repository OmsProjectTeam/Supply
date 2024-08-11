
using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IICustomerMessages
    {
        List<TBViewCustomerMessages> GetAll();
        TBCustomerMessages GetById(int IdCustomerMessages);
        bool saveData(TBCustomerMessages savee);
        bool UpdateData(TBCustomerMessages update);
        bool deleteData(int IdCustomerMessages);
        List<TBViewCustomerMessages> GetAllv(int IdCustomerMessages);
        List<TBViewCustomerMessages> GetAllDataentry(string dataEntry);

        //////////////////////////APIs/////////////////////////////////////////////////////////////////
        Task<List<TBViewCustomerMessages>> GetAllAsync();
        Task<List<TBViewCustomerMessages>> GetAllvAsync(int IdCustomerMessages);
        Task<List<TBViewCustomerMessages>> GetAllDataentryAsync(string dataEntry);
        Task<TBCustomerMessages> GetByIdAsync(int IdCustomerMessages);
        Task<bool> AddDataAsync(TBCustomerMessages savee);
        Task<bool> DeleteDataAsync(int IdCustomerMessages);
        Task<bool> UpdateDataAsync(TBCustomerMessages update);
    }
    public class CLSTBCustomerMessages: IICustomerMessages
    {
        MasterDbcontext dbcontext;
        public CLSTBCustomerMessages(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewCustomerMessages> GetAll()
        {
            List<TBViewCustomerMessages> MySlider = dbcontext.ViewCustomerMessages.OrderByDescending(n => n.IdCustomerMessages).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBCustomerMessages GetById(int IdCustomerMessages)
        {
            TBCustomerMessages sslid = dbcontext.TBCustomerMessagess.FirstOrDefault(a => a.IdCustomerMessages == IdCustomerMessages);
            return sslid;
        }
        public bool saveData(TBCustomerMessages savee)
        {
            try
            {
                dbcontext.Add<TBCustomerMessages>(savee);
                dbcontext.SaveChanges();           
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBCustomerMessages update)
        {
            try
            {
                dbcontext.Entry(update).State = EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteData(int IdCustomerMessages)
        {
            try
            {
                var paid = GetById(IdCustomerMessages);
                paid.CurrentState = false;
                dbcontext.Entry(paid).State = EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<TBViewCustomerMessages> GetAllv(int IdCustomerMessages)
        {
            List<TBViewCustomerMessages> MySlider = dbcontext.ViewCustomerMessages.OrderByDescending(n => n.IdCustomerMessages).Where(a => a.IdCustomerMessages == IdCustomerMessages).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public List<TBViewCustomerMessages> GetAllDataentry(string dataEntry)
        {
            List<TBViewCustomerMessages> MySlider = dbcontext.ViewCustomerMessages.Where(a => a.DataEntry == dataEntry && a.CurrentState == true).ToList();
            return MySlider;
        }
     // //////////////////////////APIs/////////////////////////////////////////////////////////////////

        public async Task<List<TBViewCustomerMessages>> GetAllAsync()
        {
            var myDatd = await dbcontext.ViewCustomerMessages.OrderByDescending(n => n.IdCustomerMessages).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBViewCustomerMessages>> GetAllvAsync(int IdCustomerMessages)
        {
            var myDatd = await dbcontext.ViewCustomerMessages.OrderByDescending(n => n.IdCustomerMessages).Where(a => a.IdCustomerMessages == IdCustomerMessages).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBViewCustomerMessages>> GetAllDataentryAsync(string dataEntry)
        {
            var MySlider = await dbcontext.ViewCustomerMessages.Where(a => a.DataEntry == dataEntry && a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBCustomerMessages> GetByIdAsync(int IdCustomerMessages)
        {
            var sslid = await dbcontext.TBCustomerMessagess.FirstOrDefaultAsync(a => a.IdCustomerMessages == IdCustomerMessages);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBCustomerMessages savee)
        {
            try
            {
                await dbcontext.AddAsync<TBCustomerMessages>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdCustomerMessages)
        {
            try
            {
                var paid = await GetByIdAsync(IdCustomerMessages);
                paid.CurrentState = false;
                dbcontext.Entry(paid).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBCustomerMessages update)
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