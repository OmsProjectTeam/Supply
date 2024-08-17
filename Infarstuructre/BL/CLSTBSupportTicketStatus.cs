

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IISupportTicketStatus
    {
        List<TBSupportTicketStatus> GetAll();
        TBSupportTicketStatus GetById(int IdSupportTicketStatus);
        bool saveData(TBSupportTicketStatus savee);
        bool UpdateData(TBSupportTicketStatus updatss);
        bool deleteData(int IdSupportTicketStatus);
        List<TBSupportTicketStatus> GetAllv(int IdSupportTicketStatus);

        // /////////////APIs////////////////////////////////////////////////
        Task<List<TBSupportTicketStatus>> GetAllAsync();
        Task<TBSupportTicketStatus> GetByIdAsync(int IdSupportTicket);
        Task<bool> AddDataAsync(TBSupportTicketStatus savee);
        Task<bool> DeleteDataAsync(int IdSupportTicket);
        Task<bool> UpdateDataAsync(TBSupportTicketStatus update);
    }
    public class CLSTBSupportTicketStatus: IISupportTicketStatus
    {
        MasterDbcontext dbcontext;
        public CLSTBSupportTicketStatus(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }

        public List<TBSupportTicketStatus> GetAll()
        {
            List<TBSupportTicketStatus> MySlider = dbcontext.TBSupportTicketStatuss.OrderByDescending(n => n.IdSupportTicketStatus).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBSupportTicketStatus GetById(int IdSupportTicketStatus)
        {
            TBSupportTicketStatus sslid = dbcontext.TBSupportTicketStatuss.FirstOrDefault(a => a.IdSupportTicketStatus == IdSupportTicketStatus);
            return sslid;
        }
        public bool saveData(TBSupportTicketStatus savee)
        {
            try
            {
                dbcontext.Add<TBSupportTicketStatus>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBSupportTicketStatus updatss)
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
        public bool deleteData(int IdSupportTicketStatus)
        {
            try
            {
                var catr = GetById(IdSupportTicketStatus);
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
        public List<TBSupportTicketStatus> GetAllv(int IdSupportTicketStatus)
        {
            List<TBSupportTicketStatus> MySlider = dbcontext.TBSupportTicketStatuss.OrderByDescending(n => n.IdSupportTicketStatus == IdSupportTicketStatus).Where(a => a.IdSupportTicketStatus == IdSupportTicketStatus).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        //// ///////////////////APIs////////////////////////////////////////////////////////////////

        public async Task<List<TBSupportTicketStatus>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBSupportTicketStatuss.OrderByDescending(n => n.IdSupportTicketStatus).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<TBSupportTicketStatus> GetByIdAsync(int IdSupportTicketStatus)
        {
            var sslid = await dbcontext.TBSupportTicketStatuss.FirstOrDefaultAsync(a => a.IdSupportTicketStatus == IdSupportTicketStatus);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBSupportTicketStatus savee)
        {
            try
            {
                await dbcontext.AddAsync<TBSupportTicketStatus>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdSupportTicketStatus)
        {
            try
            {
                var SupportTicketStatus = await GetByIdAsync(IdSupportTicketStatus);
                SupportTicketStatus.CurrentState = false;
                dbcontext.Entry(SupportTicketStatus).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBSupportTicketStatus update)
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
