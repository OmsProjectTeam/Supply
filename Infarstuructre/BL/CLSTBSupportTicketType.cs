

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IISupportTicketType
    {
        List<TBSupportTicketType> GetAll();
        TBSupportTicketType GetById(int IdSupportTicketType);
        bool saveData(TBSupportTicketType savee);
        bool UpdateData(TBSupportTicketType updatss);
        bool deleteData(int IdSupportTicketType);
        List<TBSupportTicketType> GetAllv(int IdSupportTicketType);

        // /////////////APIs////////////////////////////////////////////////
        Task<List<TBSupportTicketType>> GetAllAsync();
        Task<TBSupportTicketType> GetByIdAsync(int IdSupportTicket);
        Task<bool> AddDataAsync(TBSupportTicketType savee);
        Task<bool> DeleteDataAsync(int IdSupportTicket);
        Task<bool> UpdateDataAsync(TBSupportTicketType update);
    }
    public class CLSTBSupportTicketType: IISupportTicketType
    {
        MasterDbcontext dbcontext;
        public CLSTBSupportTicketType(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBSupportTicketType> GetAll()
        {
            List<TBSupportTicketType> MySlider = dbcontext.TBSupportTicketTypes.OrderByDescending(n => n.IdSupportTicketType).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBSupportTicketType GetById(int IdSupportTicketType)
        {
            TBSupportTicketType sslid = dbcontext.TBSupportTicketTypes.FirstOrDefault(a => a.IdSupportTicketType == IdSupportTicketType);
            return sslid;
        }
        public bool saveData(TBSupportTicketType savee)
        {
            try
            {
                dbcontext.Add<TBSupportTicketType>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBSupportTicketType updatss)
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
        public bool deleteData(int IdSupportTicketType)
        {
            try
            {
                var catr = GetById(IdSupportTicketType);
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
        public List<TBSupportTicketType> GetAllv(int IdSupportTicketType)
        {
            List<TBSupportTicketType> MySlider = dbcontext.TBSupportTicketTypes.OrderByDescending(n => n.IdSupportTicketType == IdSupportTicketType).Where(a => a.IdSupportTicketType == IdSupportTicketType).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        //// ///////////////////APIs////////////////////////////////////////////////////////////////

        public async Task<List<TBSupportTicketType>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBSupportTicketTypes.OrderByDescending(n => n.IdSupportTicketType).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<TBSupportTicketType> GetByIdAsync(int IdSupportTicketType)
        {
            var sslid = await dbcontext.TBSupportTicketTypes.FirstOrDefaultAsync(a => a.IdSupportTicketType == IdSupportTicketType);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBSupportTicketType savee)
        {
            try
            {
                await dbcontext.AddAsync<TBSupportTicketType>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdSupportTicketType)
        {
            try
            {
                var SupportTicketType = await GetByIdAsync(IdSupportTicketType);
                SupportTicketType.CurrentState = false;
                dbcontext.Entry(SupportTicketType).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBSupportTicketType update)
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
