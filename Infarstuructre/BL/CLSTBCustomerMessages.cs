
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


    }
}