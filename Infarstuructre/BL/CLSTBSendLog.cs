using Domin.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IISendLog
    {
        List<TBViewSendLog> GetAll();
        TBSendLog GetById(int IdSendLog);
        bool saveData(TBSendLog savee);
        bool UpdateData(TBSendLog updatss);
        bool DeleteData(int IdSendLog);
        List<TBViewSendLog> GetAllv(int IdSendLog);
        // /////////////////////////API////////////////////////////////////
        Task<List<TBViewSendLog>> GetAllAsync();
        Task<List<TBViewSendLog>> GetAllvAsync(int id);
        Task<TBSendLog> GetByIdAsync(int id);
        Task<bool> AddDataAsync(TBSendLog savee);
        Task<bool> UpdateDataAsync(TBSendLog updatee);
        Task<bool> DeleteDataAsync(int id);
    }
    public class CLSTBSendLog : IISendLog
    {
        MasterDbcontext dbcontext;
        public CLSTBSendLog(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }

        public List<TBViewSendLog> GetAll()
        {
            List<TBViewSendLog> MySlider = dbcontext.ViewSendLog.OrderByDescending(n => n.IdSendLog).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBSendLog GetById(int IdSendLog)
        {
            TBSendLog sslid = dbcontext.TBSendLogs.FirstOrDefault(a => a.IdSendLog == IdSendLog);
            return sslid;
        }
        public bool saveData(TBSendLog savee)
        {
            try
            {
                dbcontext.Add<TBSendLog>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBSendLog updatss)
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
        public bool DeleteData(int IdSendLog)
        {
            try
            {
                var catr = GetById(IdSendLog);
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
        public List<TBViewSendLog> GetAllv(int IdSendLog)
        {
            List<TBViewSendLog> MySlider = dbcontext.ViewSendLog.OrderByDescending(n => n.IdSendLog == IdSendLog).Where(a => a.IdSendLog == IdSendLog).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        // /////////////////////////////////////////////////API//////////////////////////////////////////////////////////
        public async Task<List<TBViewSendLog>> GetAllAsync()
        {
            List<TBViewSendLog> MySlider = await dbcontext.ViewSendLog.OrderByDescending(n => n.IdSendLog).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<List<TBViewSendLog>> GetAllvAsync(int id)
        {
            List<TBViewSendLog> MySlider = await dbcontext.ViewSendLog.OrderByDescending(n => n.IdSendLog == id).Where(a => a.IdSendLog == id).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBSendLog> GetByIdAsync(int id)
        {
            TBSendLog sslid = await dbcontext.TBSendLogs.FirstOrDefaultAsync(a => a.IdSendLog == id);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBSendLog savee)
        {
            try
            {
                await dbcontext.AddAsync<TBSendLog>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBSendLog updatee)
        {
            try
            {
                dbcontext.Entry(updatee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDataAsync(int id)
        {
            try
            {
                var catr = await GetByIdAsync(id);
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
    }
}
