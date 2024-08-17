using Domin.Entity.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIMessageChat
    {
        TBMessageChat GetById(int id);
        List<TBViewChatMessage> GetBySenderId(string id);
        List<TBViewChatMessage> GetByReciverId(string id);
        List<TBViewChatMessage> GetBySenderIdAndReciverId(string senderId, string reciverId);
        TBViewChatMessage GetByReciverIdLast(string id);
        bool saveData(TBMessageChat savee);
        bool UpdateData(TBMessageChat updatss);
        bool deleteData(int id);
        // //////////////////////// APIs ///////////////////////////////////
        Task<TBMessageChat> GetByIdAsync(int id);
        Task<List<TBViewChatMessage>> GetBySenderIdAsync(string id);
        Task<List<TBViewChatMessage>> GetByReciverIdAsync(string id);
        Task<List<TBViewChatMessage>> GetBySenderIdAndReciverIdAsync(string senderId, string reciverId);
        Task<TBViewChatMessage> GetByReciverIdLastAsync(string id);
        Task<bool> saveDataAsync(TBMessageChat savee);
        Task<bool> UpdateDataAsync(TBMessageChat updatss);
        Task<bool> deleteDataAsync(int id);
    }

    public class CLSTBMessageChat : IIMessageChat
    {
        MasterDbcontext dbcontext;
        public CLSTBMessageChat(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public bool deleteData(int id)
        {
            try
            {
                var catr = GetById(id);
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

        public List<TBViewChatMessage> GetAll()
        {
            List<TBViewChatMessage> MySlider = dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        public TBMessageChat GetById(int id)
        {
            TBMessageChat MySlider = dbcontext.TBMessageChats.Where(a => a.CurrentState == true && a.IdMessageChat == id).OrderByDescending(n => n.MessageeTime).FirstOrDefault();
            return MySlider;
        }



        public List<TBViewChatMessage> GetByReciverId(string id)
        {
            List<TBViewChatMessage> MySlider = dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true)
                .Where(m => m.ReciverId == id)
                .ToList();
            return MySlider;
        }



        public TBViewChatMessage GetByReciverIdLast(string id)
        {
            TBViewChatMessage MySlider = dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true)
                .Where(m => m.ReciverId == id).FirstOrDefault();
            return MySlider;
        }



        public List<TBViewChatMessage> GetBySenderId(string id)
        {
            List<TBViewChatMessage> MySlider = dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true)
                .Where(m => m.SenderId == id)
                .ToList();
            return MySlider;
        }

        public List<TBViewChatMessage> GetBySenderIdAndReciverId(string senderId, string reciverId)
        {
            List<TBViewChatMessage> MySlider = dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true)
                .Where(m => m.ReciverId == reciverId && m.SenderId == senderId)
                .ToList();
            return MySlider;
        }



        public bool saveData(TBMessageChat savee)
        {
            try
            {
                dbcontext.Add<TBMessageChat>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateData(TBMessageChat updatss)
        {
            try
            {
                dbcontext.Entry(updatss).State = EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // /////////////////////////////////////// API /////////////////////////////////////////////////////////

        public async Task<TBMessageChat> GetByIdAsync(int id)
        {
            TBMessageChat MySlider = await dbcontext.TBMessageChats.Where(a => a.CurrentState == true && a.IdMessageChat == id).OrderByDescending(n => n.MessageeTime).FirstOrDefaultAsync();
            return MySlider;
        }

        public async Task<bool> deleteDataAsync(int id)
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

        public async Task<List<TBViewChatMessage>> GetBySenderIdAndReciverIdAsync(string senderId, string reciverId)
        {
            List<TBViewChatMessage> MySlider = await dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true)
                .Where(m => m.ReciverId == reciverId && m.SenderId == senderId)
                .ToListAsync();
            return MySlider;
        }

        public async Task<List<TBViewChatMessage>> GetBySenderIdAsync(string id)
        {
            List<TBViewChatMessage> MySlider = await dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true)
                .Where(m => m.SenderId == id)
                .ToListAsync();
            return MySlider;
        }
        public async Task<bool> saveDataAsync(TBMessageChat savee)
        {
            try
            {
                await dbcontext.AddAsync<TBMessageChat>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateDataAsync(TBMessageChat updatss)
        {
            try
            {
                dbcontext.Entry(updatss).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TBViewChatMessage> GetByReciverIdLastAsync(string id)
        {
            TBViewChatMessage MySlider = await dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true)
                .Where(m => m.ReciverId == id).FirstOrDefaultAsync();
            return MySlider;
        }

        public async Task<List<TBViewChatMessage>> GetByReciverIdAsync(string id)
        {
            List<TBViewChatMessage> MySlider = await dbcontext.ViewChatMessage.OrderByDescending(n => n.MessageeTime).Where(a => a.CurrentState == true)
                .Where(m => m.ReciverId == id)
                .ToListAsync();
            return MySlider;
        }
    }
}
