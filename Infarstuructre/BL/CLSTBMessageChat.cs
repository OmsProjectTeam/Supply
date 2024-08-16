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
    }
}
