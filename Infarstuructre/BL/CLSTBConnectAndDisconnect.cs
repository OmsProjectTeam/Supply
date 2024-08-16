using Domin.Entity.SignalR;
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


    }
}
