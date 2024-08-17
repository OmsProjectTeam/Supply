using Domin.Entity.SignalR;
using Infarstuructre.BL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIConnectAndDisconnect
    {
        List<TBConnectAndDisConnect> GetAll();

        bool addConnection(TBConnectAndDisConnect save);
        bool RemoveConnection(string ConnectId);
        TBConnectAndDisConnect GetById(string ConnectId);
        TBConnectAndDisConnect GetByName(string name);
        //////////////////////////APIs/////////////////////////////////////////////////////////////////
        ///
        Task<List<TBConnectAndDisConnect>> GetAllAsync();
        Task<TBConnectAndDisConnect> GetByIdAsync(int IdConnectAndDisConnect);
        Task<TBConnectAndDisConnect> GetByNameAsync(int IdConnectAndDisConnect);
        Task<bool> AddDataAsync(TBConnectAndDisConnect savee);
        Task<bool> DeleteDataAsync(int IdConnectAndDisConnect);

    }
}
    public class CLSTBConnectAndDisconnect : IIConnectAndDisconnect
    {
        MasterDbcontext dbcontext;
        public CLSTBConnectAndDisconnect(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }

        public List<TBConnectAndDisConnect> GetAll()
        {
            List<TBConnectAndDisConnect> MySlider = dbcontext.TBConnectAndDisConnects.ToList();
            return MySlider;

        }

        public TBConnectAndDisConnect GetById(string ConnectId)
        {
            TBConnectAndDisConnect sslid = dbcontext.TBConnectAndDisConnects.FirstOrDefault(a => a.ConnectId == ConnectId);
            return sslid;
        }

        public TBConnectAndDisConnect GetByName(string name)
        {
            TBConnectAndDisConnect sslid = dbcontext.TBConnectAndDisConnects.OrderBy(a => a.TimeConnection).Where(a => a.UserName == name).LastOrDefault();
            return sslid;
        }

        public bool addConnection(TBConnectAndDisConnect save)
        {
            try
            {
                dbcontext.Add<TBConnectAndDisConnect>(save);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveConnection(string ConnectId)
        {
            try
            {
                var catr = GetById(ConnectId);
                dbcontext.Remove<TBConnectAndDisConnect>(catr);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // //////////////////////////APIs/////////////////////////////////////////////////////////////////

        public async Task<List<TBConnectAndDisConnect>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBConnectAndDisConnects.OrderByDescending(n => n.IdConnectAndDisConnect).ToListAsync();
            return myDatd;
        }

        public async Task<TBConnectAndDisConnect> GetByIdAsync(int IdConnectAndDisConnect)
        {
            var sslid = await dbcontext.TBConnectAndDisConnects.FirstOrDefaultAsync(a => a.IdConnectAndDisConnect == IdConnectAndDisConnect);
            return sslid;
        }

        public async Task<TBConnectAndDisConnect> GetByNameAsync(int IdConnectAndDisConnect)
        {
            var sslid = await dbcontext.TBConnectAndDisConnects.FirstOrDefaultAsync(a => a.IdConnectAndDisConnect == IdConnectAndDisConnect);
            return sslid;
        }

    public async Task<bool> AddDataAsync(TBConnectAndDisConnect savee)
        {
            try
            {
                await dbcontext.AddAsync<TBConnectAndDisConnect>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdConnectAndDisConnect)
        {
            try
            {
                var conn = await GetByIdAsync(IdConnectAndDisConnect);
                dbcontext.Remove<TBConnectAndDisConnect>(conn);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }

