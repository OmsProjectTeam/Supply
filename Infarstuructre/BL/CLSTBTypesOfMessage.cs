

namespace Infarstuructre.BL
{
    public interface IITypesOfMessage
    {
        List<TBTypesOfMessage> GetAll();
        TBTypesOfMessage GetById(int IdTypesOfMessage);
        bool saveData(TBTypesOfMessage savee);
        bool UpdateData(TBTypesOfMessage updatss);
        bool deleteData(int IdTypesOfMessage);
        List<TBTypesOfMessage> GetAllv(int IdTypesOfMessage);
    }
    public class CLSTBTypesOfMessage: IITypesOfMessage
    {
        MasterDbcontext dbcontext;
        public CLSTBTypesOfMessage(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBTypesOfMessage> GetAll()
        {
            List<TBTypesOfMessage> MySlider = dbcontext.TBTypesOfMessages.OrderByDescending(n => n.IdTypesOfMessage).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTypesOfMessage GetById(int IdTypesOfMessage)
        {
            TBTypesOfMessage sslid = dbcontext.TBTypesOfMessages.FirstOrDefault(a => a.IdTypesOfMessage == IdTypesOfMessage);
            return sslid;
        }
        public bool saveData(TBTypesOfMessage savee)
        {
            try
            {
                dbcontext.Add<TBTypesOfMessage>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTypesOfMessage updatss)
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
        public bool deleteData(int IdTypesOfMessage)
        {
            try
            {
                var catr = GetById(IdTypesOfMessage);
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
        public List<TBTypesOfMessage> GetAllv(int IdTypesOfMessage)
        {
            List<TBTypesOfMessage> MySlider = dbcontext.TBTypesOfMessages.OrderByDescending(n => n.IdTypesOfMessage == IdTypesOfMessage).Where(a => a.IdTypesOfMessage == IdTypesOfMessage).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
    }
}
